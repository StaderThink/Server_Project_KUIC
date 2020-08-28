using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Components.Forms;

namespace Infraestructura.Compartido.Formularios
{
    public class FieldBase<TValue>: InputBase<TValue>
    {
        protected new string CssClass
        {
            get
            {
                string classes = "input";
                var messages = EditContext.GetValidationMessages(FieldIdentifier);

                if (messages.Any())
                {
                    classes += " is-danger";
                }

                return classes;
            }
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
                    var date = (TValue) (object) DateTime.Parse(value);
                    result = date;
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
