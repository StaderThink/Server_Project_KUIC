using Centaurus.Modelo;
using Centaurus.Repositorio;
using Corvus.Modelo.Sesiones;
using Corvus.Servicio.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Norma.Extensiones {
	public enum Permiso {
		Basico, Pedidos, Usuarios, Logistica, Clientes, Solicitar
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class AutenticadoAttribute: Attribute, IAsyncActionFilter {
		private readonly Permiso permiso;

		#region Permiso
		public AutenticadoAttribute() {
			permiso = Permiso.Basico;
		}

		public AutenticadoAttribute(Permiso permiso) {
			this.permiso = permiso;
		}

		private bool ValidarPermisos(Usuario usuario) {
			var repo = new RepoCargo();

			if (repo.PorId(usuario.Cargo) is Cargo cargo) {
				var tienePermiso = permiso switch
				{
					Permiso.Pedidos => cargo.Pedidos,
					Permiso.Usuarios => cargo.Usuarios,
					Permiso.Logistica => cargo.Logistica,
					Permiso.Clientes => cargo.Clientes,
					Permiso.Solicitar => cargo.Solicitar,
					_ => true, // no necesita permiso especifico
				};

				return tienePermiso;
			}

			return false;
		}
		#endregion

		private string ExtraerToken(string cabecera) {
			if (!string.IsNullOrWhiteSpace(cabecera)) {
				var regex = new Regex("^[bB]earer (.*)$");
				var resultado = regex.Match(cabecera);

				if (resultado.Success && resultado.Groups.Count > 1) {
					return resultado.Groups[1].Value;
				}
			}

			return null;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
			var cookies = context.HttpContext.Request.Headers;
			cookies.TryGetValue("Authorization", out StringValues cabecera);

			if (ExtraerToken(cabecera) is string token) {
				var servicio = new ServicioSesion();

				if (servicio.Traducir(token) is Sesion sesion) {
					var repo = new RepoUsuario();
					var usuario = repo.PorDocumento(sesion.Credencial.Documento);

					if (usuario is Usuario) {
						if (ValidarPermisos(usuario)) {
							context.HttpContext.Items["usuario"] = usuario;
							await next();
						}
					}

					context.Result = new BadRequestResult();
				}
			}

			context.Result = new UnauthorizedResult();
		}
	}
}
