using System;
using System.Linq;
using Microsoft.AspNetCore.Components.Forms;

namespace Infraestructura.Vistas.Componentes.Formularios
{
    public class FieldBase<TValue>: InputBase<TValue>
    {
        protected string ControlCssClass()
        {
            bool isInvalid = EditContext.GetValidationMessages(FieldIdentifier).Any();
            return isInvalid ? "is-danger" : "";
        }

        protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
        {
            try
            {
                if (typeof(TValue) == typeof(int))
                {
                    result = (TValue) (object) int.Parse(value);
                }

                else if (typeof(TValue) == typeof(DateTime))
                {
                    result = (TValue) (object) DateTime.Parse(value);
                }

                else
                {
                    result = (TValue) (object) value;
                }

                validationErrorMessage = null;
                return true;
            }

            catch
            {
                result = default;
                validationErrorMessage = "No se logro traducir el valor";
                return false;
            }
        }
    }
}
