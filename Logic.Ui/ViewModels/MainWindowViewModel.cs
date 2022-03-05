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
        public BookCollectionViewModel BookCollection { get; set; } //public BookCollectionViewModel MyList { get; set; }
        public MainWindowViewModel(BookCollectionViewModel bookCollectionViewModel)
        {
            BookCollection = bookCollectionViewModel;
            /*if (BookCollection.Count == 0)
            {
                try
                {
                    LoadFromFileCommand();
                }
                catch
                {
                    FillBookListMethod();
                }
            }*/
            LoadFromFile = new RelayCommand(LoadFromFileCommand); //LoadCommand = new RelayCommand(LoadModel);
            SaveCommand = new RelayCommand(SaveModel);
            modelFileHandler = new ModelFileHandler();
            pathForSerialization = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                + "\\BookManagerSerialization\\BooksGroup011.bmf";

            // Open PrintService Window
            OpenPrintServiceWindow = new RelayCommand(OpenPrintServiceWindowCommand);

            // Open BibInport Window
            OpenNewTexBookCollectionWindow = new RelayCommand(OpenNewTexBookCollectionWindowMethod);

            // Open BookSearch Window
            OpenBookSearchWindow = new RelayCommand(OpenBookSearchWindowCommand);
            OpenNewBookWindowCommand = new RelayCommand(OpenNewBookWindowMethod);
            FillBookListCommand = new RelayCommand(FillBookListMethod);
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


        private void FillBookListMethod()
        {
            // DEMO BOOKS
            Console.WriteLine("Create Demo Books...");
            for (int i = 1; i <= 10; i++)
            {
                BookViewModel book = new BookViewModel();
                book.Title = "Book #" + i;
                book.Author = "Author #" + i;
                book.Publisher = "Publisher #" + i;
                book.Description = "Da steht <<Lorem ipsum dolor sit amet, ...>> drin";
                book.Extract = "Lorem ipsum dolor sit amet, ...";
                book.Genre = Business.Model.BusinessObjects.ENUM.Genre.Horror;
                book.Language = Business.Model.BusinessObjects.ENUM.Language.Deutsch;
                book.Format = Business.Model.BusinessObjects.ENUM.Format.Taschenbuch;
                book.Price = 2.3M;
                book.Pages = 120;
                book.Weight = 10;
                book.Isbn = i + "-" + i + 7645 + "-" + i + 2641 + "-" + i + 1;
                book.Rating = 2.4;
                book.Edition = i;
                book.Bestseller = i % 2 == 0;
                book.ReleaseDate = new DateTime();

                Business.Model.BusinessObjects.Dimension dimension = new Business.Model.BusinessObjects.Dimension();
                dimension.Depth = 1 * (i + 1);
                dimension.Height = 3 * (i + 1);
                dimension.Width = 2 * (i + 1);
                book.Dimension = dimension;

                // Root Directory of Repository => \img\
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\img\\" + i + ".png";
                Image image = Image.FromFile(path);
                book.Cover = image;
                BookCollection.Add(book);
            }
        }
        private void RenameValueInModel()
        {
            var first = BookCollection.FirstOrDefault();
            first.Model.Title = "Rename in model";
        }
    }
}