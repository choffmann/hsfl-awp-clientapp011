using De.HsFlensburg.ClientApp011.Logic.Ui.MessageBusMessages;
using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;
using De.HsFlensburg.ClientApp011.Services.MessageBus;
using De.HsFlensburg.ClientApp011.Services.SerializationService;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels
{
    public class MainWindowViewModel
    {
        private ModelFileHandler modelFileHandler;
        private string pathForSerialization;

        public ICommand RenameValueInModelCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand OpenNewBookWindowCommand { get; }
        public ICommand FillBookListCommand { get; }        

        //public ICommand FillBookList { get; set; }
        public ICommand OpenPrintServiceWindow { get; }
        public ICommand OpenNewTexBookCollectionWindow { get; }
        public ICommand OpenBookSearchWindow { get; }
        public ICommand LoadFromFile { get; }
        public BookCollectionViewModel BookCollection { get; set; }
        public MainWindowViewModel(BookCollectionViewModel bookCollectionViewModel)
        {
            BookCollection = bookCollectionViewModel;
            LoadFromFile = new RelayCommand(LoadFromFileCommand);
            SaveCommand = new RelayCommand(SaveModel);
            modelFileHandler = new ModelFileHandler();
            pathForSerialization = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                + "\\BookManagerSerialization\\BooksGroup017.bmf";

            // Open PrintService Window
            OpenPrintServiceWindow = new RelayCommand(OpenPrintServiceWindowCommand);
            // Open BibInport Window
            OpenNewTexBookCollectionWindow = 
                new RelayCommand(OpenNewTexBookCollectionWindowMethod);
            // Open BookSearch Window
            OpenBookSearchWindow = new RelayCommand(OpenBookSearchWindowCommand);
            OpenNewBookWindowCommand = new RelayCommand(OpenNewBookWindowMethod);
        }
        private void OpenNewBookWindowMethod()
        {
            ServiceBus.Instance.Send(new OpenNewBookWindowMessage());
        }
        private void LoadFromFileCommand()
        {
            BookCollection.Model = modelFileHandler.ReadModelFromFile(pathForSerialization);
        }
        private void SaveModel()
        {
            modelFileHandler.WriteModelToFile(pathForSerialization, BookCollection.Model);
        }
        private void OpenPrintServiceWindowCommand()
        {
            ServiceBus.Instance.Send(new OpenPrintServiceWindowMessage());
        }
        private void OpenNewTexBookCollectionWindowMethod()
        {
            ServiceBus.Instance.Send(new OpenNewTexBookCollectionWindowMessage());
        }
        private void OpenBookSearchWindowCommand()
        {
            ServiceBus.Instance.Send(new OpenBookSearchWindowMessage());
        }
    }
}