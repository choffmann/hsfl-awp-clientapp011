using De.HsFlensburg.ClientApp011.Logic.Ui.MessageBusMessages;
using De.HsFlensburg.ClientApp011.Services.MessageBus;

namespace De.HsFlensburg.ClientApp011.Ui.Desktop.MessageBusLogic
{
    public class MessageListener
    {
        public bool BindableProperty => true;
        public MessageListener()
        {
            InitMessenger();
        }

        private void InitMessenger()
        {
            ServiceBus.Instance.Register<OpenNewTexBookCollectionWindowMessage>(this, OpenNewTexBookCollectionWindow);
            ServiceBus.Instance.Register<OpenNewErrorWindowMessage>(this, OpenNewErrorWindow);
            ServiceBus.Instance.Register<OpenPrintServiceWindowMessage>(this, OpenPrintServiceWindow);
            ServiceBus.Instance.Register<OpenBookSearchWindowMessage>(this, OpenBookSearchWindow);
            ServiceBus.Instance.Register<OpenNewBookWindowMessage>(this, OpenNewBookWindow);
        }
        private void OpenPrintServiceWindow()
        {
            PrintServiceWindow printServiceWindow = new PrintServiceWindow();
            printServiceWindow.ShowDialog();
        }

        private void OpenNewTexBookCollectionWindow()
        {
            TexBookCollectionWindow myWindow = new TexBookCollectionWindow();
            myWindow.ShowDialog();
        }

        private void OpenBookSearchWindow()
        {
            BookSearchWindow bookSearchWindow = new BookSearchWindow();
            bookSearchWindow.ShowDialog();
        }

        private void OpenNewErrorWindow()
        {
            ErrorWindow myWindow = new ErrorWindow();
            myWindow.ShowDialog();
        }
        private void OpenNewBookWindow()
        {
            NewBookEntryWindow newBookWindow = new NewBookEntryWindow();
            newBookWindow.ShowDialog();
        }
    }
}