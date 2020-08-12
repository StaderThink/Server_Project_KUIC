using System;

namespace Infraestructura.Compartido.Notificaciones
{
    public interface IToastService
    {
        event Action<Toast> OnShow;

        void ShowMessage(string message);
        void ShowWarning(string message);
        void ShowError(string message);
        void ShowSuccess(string message);
    }
}
