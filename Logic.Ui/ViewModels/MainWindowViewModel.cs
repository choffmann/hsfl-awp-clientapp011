using De.HsFlensburg.ClientApp011.Logic.Ui.MessageBusMessages;
using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;
using De.HsFlensburg.ClientApp011.Services.MessageBus;
using De.HsFlensburg.ClientApp011.Services.SerializationService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels
{
    public class MainWindowViewModel
    {
        public ICommand FillBookList { get; set; }
        public ICommand OpenPrintServiceWindow { get; }
        public ICommand OpenNewTexBookCollectionWindow { get; }
        private ModelFileHandler modelFileHandler;
        private string pathForSerialization;
        public ICommand LoadFromFile { get; }
        public BookCollectionViewModel BookCollection { get; set; }
        public MainWindowViewModel(BookCollectionViewModel bookCollectionViewModel)
        {
            BookCollection = bookCollectionViewModel;
            LoadFromFile = new RelayCommand(LoadFromFileCommand);
            modelFileHandler = new ModelFileHandler();
            pathForSerialization = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\BookManagerSerialization\\BooksGroup017.bmf";

            // Open PrintService Window
            OpenPrintServiceWindow = new RelayCommand(OpenPrintServiceWindowCommand);

            // Open BibInport Window
            OpenNewTexBookCollectionWindow = new RelayCommand(OpenNewTexBookCollectionWindowMethod);
        }

        private void LoadFromFileCommand()
        {
            BookCollection.Model = modelFileHandler.ReadModelFromFile(pathForSerialization);
        }

        private void OpenPrintServiceWindowCommand()
        {
            ServiceBus.Instance.Send(new OpenPrintServiceWindowMessage());
        }

        private void OpenNewTexBookCollectionWindowMethod()
        {
            ServiceBus.Instance.Send(new OpenNewTexBookCollectionWindowMessage());
        }
    }
}
