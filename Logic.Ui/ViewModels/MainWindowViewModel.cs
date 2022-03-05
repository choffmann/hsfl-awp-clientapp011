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
        public BookCollectionViewModel MyList { get; set; }

        private void OpenNewBookWindowMethod()
        {
            ServiceBus.Instance.Send(new OpenNewBookWindowMessage());
        }

        public MainWindowViewModel(BookCollectionViewModel viewModelCollection)
        {
            modelFileHandler = new ModelFileHandler();
            pathForSerialization = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                + "\\BookManagerSerialization\\BooksGroup011.bmf";
            RenameValueInModelCommand = new RelayCommand(RenameValueInModel);
            SaveCommand = new RelayCommand(SaveModel);
            LoadCommand = new RelayCommand(LoadModel);
            OpenNewBookWindowCommand = new RelayCommand(OpenNewBookWindowMethod);
            FillBookListCommand = new RelayCommand(FillBookListMethod);

            MyList = viewModelCollection;
            if (viewModelCollection.Count == 0)
            {
                try
                {
                    LoadModel();
                }
                catch
                {
                    FillBookListMethod();
                }
            }
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
                MyList.Add(book);
            }
        }
        private void RenameValueInModel()
        {
            var first = MyList.FirstOrDefault();
            first.Model.Title = "Rename in model";
        }
        private void SaveModel()
        {
            modelFileHandler.WriteModelToFile(pathForSerialization, MyList.Model);
        }

        private void LoadModel()
        {
            MyList.Model = modelFileHandler.ReadModelFromFile(pathForSerialization);
        }
    }
}
