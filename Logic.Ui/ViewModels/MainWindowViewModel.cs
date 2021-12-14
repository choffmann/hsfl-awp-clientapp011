using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;
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
        }

        private void LoadFromFileCommand()
        {
            BookCollection.Model = modelFileHandler.ReadModelFromFile(pathForSerialization);
        }
    }
}
