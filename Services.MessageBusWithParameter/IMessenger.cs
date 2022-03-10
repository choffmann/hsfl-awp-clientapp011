using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De.HsFlensburg.ClientApp011.Services.MessageBusWithParameter
{
    public interface IMessenger
    {
        void Register<TNotification>(object recipient, Action<TNotification> action);
        void Register<TNotification>(object recipient, string identCode, Action<TNotification> action);
        void Send<TNotification>(TNotification notification);
        void Send<TNotification>(TNotification notification, string identCode);
        void Unregister<TNotification>(object recipient);
    }

}
