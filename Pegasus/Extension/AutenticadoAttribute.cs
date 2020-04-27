using Centaurus.Modelo;
using Centaurus.Repositorio;
using Centaurus.Seguridad;
using Corvus.Caso.Crud;
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

		public AutenticadoAttribute() {}

		public AutenticadoAttribute(Permiso permiso) {
			this.permiso = permiso;
		}

		private async Task BorrarCookie(ActionExecutingContext contexto, string mensaje) {
			var respuesta = contexto.HttpContext.Response;

			respuesta.StatusCode = 401;
			respuesta.Cookies.Delete("token");

			await respuesta.WriteAsync(mensaje);
		}

		#region Procesos
		private Sesion ObtenerSesion(string token) {
			if (!string.IsNullOrEmpty(token)) {
				var criptografo = new ProveedorToken();
				var carga = criptografo.Traduccir<Sesion>(token);

				// validar sesion

				if (carga is Sesion sesion) {
					var proceso = new ProcesoSesion();

					if (proceso.Traducir(sesion) is Credencial) {
						return sesion;
					}
				}
			}

			return null;
		}
		private bool TienePermiso(Credencial credencial) {
			var crudUsuario = new CrudUsuario();
			var usuario = crudUsuario.PorDocumento(credencial.Documento);

			if (usuario is Usuario) {
				var repoCargo = new RepoCargo();

				if (repoCargo.PorId(usuario.Cargo) is Cargo cargo) {
					return permiso switch {
						Permiso.Pedidos => cargo.Pedidos,
						Permiso.Usuarios => cargo.Usuarios,
						Permiso.Logistica => cargo.Logistica,
						Permiso.Clientes => cargo.Clientes,
						Permiso.Solicitar => cargo.Solicitar,
						_ => false,
					};
				}
			}

			return false;
		}

		#endregion

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
			var cookies = context.HttpContext.Request.Cookies;

			if (cookies.TryGetValue("token", out string token)) {
				if (ObtenerSesion(token) is Sesion sesion) {
					var proceso = new ProcesoSesion();

					if (proceso.Traducir(sesion) is Credencial credencial) {
						// validar permisos

						if (TienePermiso(credencial)) {
							context.HttpContext.Items["sesion"] = sesion;

							await next();
							return;
						}

						else {
							context.Result = new UnauthorizedObjectResult("no tienes permisos");
							return;
						}
					}
				}

				await BorrarCookie(context, "traduccion fallida, token invalido");
				return;
			}

			await BorrarCookie(context, "sesión invalida");
		}
	}
}
