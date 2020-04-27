﻿using Centaurus.Modelo;
using Corvus.Caso.Crud;
using Corvus.Modelo.Sesiones;
using System;
using System.Linq;

namespace Corvus.Caso.Proceso {
	public sealed class ProcesoSesion: ITraductor<Credencial, Sesion> {
		private bool ValidarCredencial(Credencial credencial) {
			var crud = new CrudUsuario();


			var consulta = crud.Listar()
				.Where(usuario => usuario.Documento == credencial.Documento)
				.First();

			if (consulta is Usuario usuario) {
				if (usuario.Activo && usuario.Clave == credencial.Clave) {
					return true;
				}
			}

			return false;
		}

		public Sesion Generar(Credencial datos) {
			if (ValidarCredencial(datos)) {
				return new Sesion {
					Credencial = datos,
					Fecha = DateTime.Now
				};
			}

			return null;
		}

		public Credencial Traducir(Sesion datos) {
			if (ValidarCredencial(datos.Credencial)) {
				var diasUso = (DateTime.Now - datos.Fecha).Days;
				
				if (diasUso < 3) {
					return datos.Credencial;
				}
			}

			return null;
		}
	}
}
