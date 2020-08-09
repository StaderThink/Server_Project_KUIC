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

        private async Task<AuthenticationState> GetAuthenticationState()
        {
            var status = new AuthenticationState(new ClaimsPrincipal());
            var token = await localStorage.GetItemAsync<string>("token");

            if (token is string)
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                var respuesta = await http.GetAsync("/api/sesion");

                if (respuesta.IsSuccessStatusCode)
                {
                    var usuario = await respuesta.Content.ReadFromJsonAsync<Usuario>();
                    var servicio = new ServicioSesion();

                    var identidad = servicio.GenerarIdentidad(usuario);
                    status = new AuthenticationState(identidad);
                }
            }

            return status;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                return await GetAuthenticationState();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new AuthenticationState(new ClaimsPrincipal());
            }
        }

        public async Task SignIn(string documento, string clave)
        {
            var credentials = new FormularioIniciarSesion(documento, clave);
            var response = await http.PostAsJsonAsync("/api/sesion", credentials);

            if (response.IsSuccessStatusCode)
            {
                string token = await response.Content.ReadAsStringAsync();
                await localStorage.SetItemAsync("token", token);

                NotifyAuthenticationStateChanged(GetAuthenticationState());
                navigation.NavigateTo("/");
            }

            else
            {
                throw new ArgumentException("Invalid credentials");
            }
        }

        public async Task SignOut()
        {
            await localStorage.ClearAsync();

            NotifyAuthenticationStateChanged(GetAuthenticationState());
            navigation.NavigateTo("/");
        }

        #nullable enable
        public async Task<Usuario?> GetUserAsync()
        {
            try
            {
                return await http.GetFromJsonAsync<Usuario>("/api/sesion");
            }

            catch
            {
                return null;
            }
        }
        #nullable disable
    }
}
