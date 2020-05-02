using Centaurus.Modelo;
using Centaurus.Repositorio;
using Centaurus.Seguridad;
using Corvus.Caso.Proceso;
using Corvus.Modelo.Sesiones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Pegasus.Extension {
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public sealed class AutenticadoAttribute: Attribute, IAsyncActionFilter {
		private readonly Permiso permiso;

		public AutenticadoAttribute() {
			this.permiso = Permiso.Basico;
		}

		public AutenticadoAttribute(Permiso permiso) {
			this.permiso = permiso;
		}

		private async Task BorrarCookie(ActionExecutingContext contexto, string mensaje) {
			var respuesta = contexto.HttpContext.Response;

			respuesta.StatusCode = 403;
			respuesta.Cookies.Delete("token");

			await respuesta.WriteAsync(mensaje);
		}

		#region Procesos
		private Sesion ObtenerSesion(string token) {
			if (string.IsNullOrEmpty(token)) return null;

			var criptografo = new ProveedorToken();
			var carga = criptografo.Traduccir<Sesion>(token);

			// validar sesion

			if (carga is Sesion sesion) {
				var proceso = new ProcesoSesion();

				if (proceso.Traducir(sesion) is Credencial) {
					return sesion;
				}
			}

			return null;
		}

		private Usuario ObtenerUsuario(Credencial credencial) {
			var repoUsuario = new RepoUsuario();
			var usuario = repoUsuario.PorDocumento(credencial.Documento);

			if (usuario is Usuario) {
				var repoCargo = new RepoCargo();

				if (repoCargo.PorId(usuario.Cargo) is Cargo cargo) {
					var tienePermiso = permiso switch {
						Permiso.Pedidos => cargo.Pedidos,
						Permiso.Usuarios => cargo.Usuarios,
						Permiso.Logistica => cargo.Logistica,
						Permiso.Clientes => cargo.Clientes,
						Permiso.Solicitar => cargo.Solicitar,
						_ => true, // no necesita permiso especifico
					};

					return tienePermiso ? usuario : null;
				}
			}

			return null;
		}
		#endregion

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
			var cookies = context.HttpContext.Request.Cookies;
			cookies.TryGetValue("token", out string token);

			if (ObtenerSesion(token) is Sesion sesion) {
				var proceso = new ProcesoSesion();

				if (proceso.Traducir(sesion) is Credencial credencial) {
					var usuario = ObtenerUsuario(credencial);

					if (usuario is Usuario) {
						// establecer usuario

						context.HttpContext.Items["usuario"] = usuario;
						await next();
					}

					else {
						context.Result = new UnauthorizedObjectResult("no se logro asociar el usuario con la sesión");
					}
				}
			}

			else {
				await BorrarCookie(context, "sesión invalida, intenta iniciar sesión");
			}
		}
	}
}
