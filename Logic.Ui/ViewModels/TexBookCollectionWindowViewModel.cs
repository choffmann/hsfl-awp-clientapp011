using De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp011.Logic.Ui.MessageBusMessages;
using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;
using De.HsFlensburg.ClientApp011.Services.MessageBus;
using De.HsFlensburg.ClientApp011.Services.TexImport;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels
{
    public class TexBookCollectionWindowViewModel
    {
        public BookCollectionViewModel TempBooks { get; set; }
        
        private BookCollectionViewModel bookCollectionViewModel;
        public ICommand OpenFile { get; }
        public ICommand ImportFile { get; set; }

        public TexBookCollectionWindowViewModel(BookCollectionViewModel viewModelCollection)
        {
            OpenFile = new RelayCommand(OpenFileMethod);
            ImportFile = new RelayCommand(ImportFileMethod);
            bookCollectionViewModel = viewModelCollection; 
            TempBooks = new BookCollectionViewModel();
        }

        private void OpenFileMethod()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            var path = openFileDialog.FileName;
            string readText = File.ReadAllText(path);
            SplittPath mySplittPath = new SplittPath(readText);
            mySplittPath.AddItemsToCollection(TempBooks.Model);
            Debug.WriteLine(readText);
        }

        public void ImportFileMethod()
        {
            if (!CheckImportedBooks())
            {
                foreach (var book in TempBooks)
                {
                    this.bookCollectionViewModel.Add(book);
                }
            }
            else
            {
                ServiceBus.Instance.Send(new OpenNewErrorWindowMessage());
            }
            TempBooks.Clear();
        }

        private bool CheckImportedBooks()
        {
            bool foundSameBook = false;
            for (int i = 0; i < bookCollectionViewModel.Count; i++)
            {
                foreach (var book in TempBooks)
                {
                    if (bookCollectionViewModel.ElementAt(i).Title == book.Title)
                    {
                        foundSameBook = true;
                        return foundSameBook;
                    }
                }
            }
            return foundSameBook;
        }
    }
        
}
