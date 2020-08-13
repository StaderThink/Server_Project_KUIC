using System;
using System.Linq;
using Microsoft.AspNetCore.Components.Forms;

namespace Infraestructura.Compartido.Formularios
{
    public class FieldBase<TValue>: InputBase<TValue>
    {
        protected string ControlCssClass()
        {
            string cssClass = "";

            bool isInvalid = EditContext.GetValidationMessages(FieldIdentifier).Any();

            if (isInvalid)
                cssClass += "is-danger ";

            return cssClass;
        }

        protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
        {
            try
            {
                if (typeof(TValue) == typeof(int))
                {
                    result = (TValue) (object) int.Parse(value);
                }

                else if (typeof(TValue) == typeof(double))
                {
                    result = (TValue) (object) double.Parse(value);
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
