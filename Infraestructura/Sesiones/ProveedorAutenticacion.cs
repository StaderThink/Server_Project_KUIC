using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Aplicacion.Sesiones;
using Aplicacion.Sesiones.Formularios;
using Blazored.LocalStorage;
using Dominio.Usuarios;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Infraestructura.Sesiones
{
    public sealed class ProveedorAutenticacion : AuthenticationStateProvider
    {
        private readonly HttpClient http;
        private readonly ILocalStorageService localStorage;
        private readonly NavigationManager navigation;

        public ProveedorAutenticacion(HttpClient http, ILocalStorageService localStorage, NavigationManager navigation)
        {
            this.http = http;
            this.localStorage = localStorage;
            this.navigation = navigation;
        }

        private async Task<AuthenticationState> ObtenerIdentidad()
        {
            AuthenticationState estado = new AuthenticationState(new ClaimsPrincipal());

            string token = await localStorage.GetItemAsync<string>("token");

            if (token is string)
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                HttpResponseMessage respuesta = await http.GetAsync("/api/sesion");

                if (respuesta.IsSuccessStatusCode)
                {
                    Usuario usuario = await respuesta.Content.ReadFromJsonAsync<Usuario>();
                    ServicioSesion servicio = new ServicioSesion();

                    ClaimsPrincipal identidad = servicio.GenerarIdentidad(usuario);
                    estado = new AuthenticationState(identidad);
                }
            }

            return estado;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                return await ObtenerIdentidad();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new AuthenticationState(new ClaimsPrincipal());
            }
        }

        public async Task IniciarSesion(string documento, string clave)
        {
            FormularioIniciarSesion credencial = new FormularioIniciarSesion(documento, clave);
            HttpResponseMessage respuesta = await http.PostAsJsonAsync("/api/sesion", credencial);

            if (respuesta.IsSuccessStatusCode)
            {
                string token = await respuesta.Content.ReadAsStringAsync();
                await localStorage.SetItemAsync("token", token);

                NotifyAuthenticationStateChanged(ObtenerIdentidad());
                navigation.NavigateTo("/inicio");
            }

            else
            {
                throw new ArgumentException("Credenciales invalidas");
            }
        }

        public async Task CerrarSesion()
        {
            await localStorage.ClearAsync();
            NotifyAuthenticationStateChanged(ObtenerIdentidad());
            navigation.NavigateTo("/");
        }
    }
}
