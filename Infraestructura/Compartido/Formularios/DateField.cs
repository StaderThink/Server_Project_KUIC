using System;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Infraestructura.Compartido.Formularios
{
    public class DateField : FieldBase<DateTime>
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "control");

            #region Input
            builder.OpenElement(2, "input");
            builder.AddMultipleAttributes(3, AdditionalAttributes);
            builder.AddAttribute(4, "class", CssClass);
            builder.AddAttribute(5, "type", "date");
            builder.AddAttribute(6, "value", CurrentValueAsString);
            builder.AddAttribute(7, "onchange", EventCallback.Factory.CreateBinder(this, (value) => CurrentValueAsString = value, CurrentValueAsString));
            builder.CloseElement();
            #endregion

            builder.CloseElement();
        }

        protected override string FormatValueAsString(DateTime value)
        {
            return BindConverter.FormatValue(value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}
