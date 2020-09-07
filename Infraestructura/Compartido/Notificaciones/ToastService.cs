using System;

namespace Infraestructura.Compartido.Notificaciones
{
    public class ToastService : IToastService
    {
        public event Action<Toast> OnShow;

        public void ShowWarning(string message)
        {
            Show(message, ToastType.Warning);
        }

        public void ShowError(string message)
        {
            Show(message, ToastType.Danger);
        }

        public void ShowMessage(string message)
        {
            Show(message, ToastType.Normal);
        }

        public void ShowSuccess(string message)
        {
            Show(message, ToastType.Success);
        }

        protected void Show(string message, ToastType type)
        {
            var toast = new Toast
            {
                Id = Guid.NewGuid(),
                Body = message,
                Type = type
            };

            OnShow?.Invoke(toast);
        }
    }
}
