using De.HsFlensburg.ClientApp011.Logic.Ui.MessageBusMessages;
using De.HsFlensburg.ClientApp011.Services.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De.HsFlensburg.ClientApp011.Ui.Desktop.MessageBusLogic
{
    class MessageListener
    {
        public bool BindableProperty => true;
        public MessageListener()
        {
            InitMessenger();
        }

        private void InitMessenger()
        {
            ServiceBus.Instance.Register<OpenNewClientWindowMessage>(this, OpenNewClientWindow);
            ServiceBus.Instance.Register<OpenNewTexBookCollectionWindowMessage>(this, OpenNewTexBookCollectionWindow);
            ServiceBus.Instance.Register<OpenNewErrorWindowMessage>(this, OpenNewErrorWindow);
        }
        private void OpenNewClientWindow()
        {
            NewClientWindow myWindow = new NewClientWindow();
            myWindow.ShowDialog();
        }

        private void OpenNewTexBookCollectionWindow()
        {
            TexBookCollectionWindow myWindow = new TexBookCollectionWindow();
            myWindow.ShowDialog();
        }

        private void OpenNewErrorWindow()
        {
            ErrorWindow myWindow = new ErrorWindow();
            myWindow.ShowDialog();
        }
    }
}
