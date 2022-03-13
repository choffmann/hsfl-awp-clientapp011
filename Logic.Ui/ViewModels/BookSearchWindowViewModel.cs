using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels
{
    public class BookSearchWindowViewModel
    {
        public BookCollectionViewModel BookCollection { get; set; }

        public BookCollectionViewModel FilteredBookCollection { get; set; }

        public ICommand CloseWindow { get; }
        public ICommand WindowLoaded { get; }

        public ICommand ActivateFilter { get; }

        public ICommand ResetFilter { get; }

        public ICommand ShowBestseller { get; }
        public ICommand ShowNovelties { get; }
        public ICommand ShowSpecificGenre { get; }
        public ICommand ShowSpecificFormat { get; }

        public ICommand ShowTextFilteredBooks { get; set; }


        public BookSearchWindowViewModel(BookCollectionViewModel bookCollectionViewModel)
        {
            BookCollection = bookCollectionViewModel;
            FilteredBookCollection = new BookCollectionViewModel();
            WindowLoaded = new RelayCommand(param => WindowLoadedCommand());
            CloseWindow = new RelayCommand(param => CloseWindowCommand(param));
            ShowBestseller = new RelayCommand(param => ShowBestsellerCommand());
            ShowNovelties = new RelayCommand(param => ShowNoveltiesCommand());
            ShowSpecificGenre = new RelayCommand(param => ShowSpecificGenreCommand(param));
            ShowSpecificFormat = new RelayCommand(param => ShowSpecificFormatCommand(param));
            ShowTextFilteredBooks = new RelayCommand(param => ShowTextFilteredBooksCommand(param));
            ResetFilter = new RelayCommand(param => ResetFilteredBookCollectionCommand());
        }
        private void WindowLoadedCommand()
        {
            resetFilteredBookCollection();
        }
        private void CloseWindowCommand(object param)
        {
            Window window = (Window)param;
            window.Close();
        }
        private void ShowBestsellerCommand()
        {            
            FilteredBookCollection.Clear();
            foreach(BookViewModel book in BookCollection)
            {
                if (book.Bestseller)
                {
                    FilteredBookCollection.Add(book);
                }
            }
        }
        private void ShowNoveltiesCommand()
        {
            FilteredBookCollection.Clear();
            foreach (BookViewModel book in BookCollection)
            {
                if (book.ReleaseDate > DateTime.Today.AddMonths(-3))
                {
                    FilteredBookCollection.Add(book);
                }
            }
        }
        private void ShowSpecificGenreCommand(object param)
        {
            FilteredBookCollection.Clear();
            foreach (BookViewModel book in BookCollection)
            {
                //Wieso funktioniert AddedItems.firstOrDefault nicht?
                if (book.Genre.ToString() == ((System.Windows.Controls.SelectionChangedEventArgs)param).AddedItems[0].ToString())
                {
                    FilteredBookCollection.Add(book);
                }
            }
        }
        private void ShowSpecificFormatCommand(object param)
        {
            FilteredBookCollection.Clear();
            foreach (BookViewModel book in BookCollection)
            {
                if (book.Format.ToString() == ((System.Windows.Controls.SelectionChangedEventArgs)param).AddedItems[0].ToString())
                {
                    FilteredBookCollection.Add(book);
                }
            }
        }
        private void ShowTextFilteredBooksCommand(object param)
        {
            FilteredBookCollection.Clear();
            String searchInput = ((System.Windows.Controls.TextBox)((System.Windows.Controls.TextChangedEventArgs)param).OriginalSource).Text;           
            if(searchInput == "")
            {
                resetFilteredBookCollection();
            } else {
                foreach (BookViewModel book in BookCollection)
                {
                    String textFields = book.Author + book.Genre.ToString() + book.Title
                        + book.Isbn + book.Publisher + book.Description;
                    if (textFields.Contains(searchInput))
                    {
                        FilteredBookCollection.Add(book);
                    }
                }
            }
        }
        private void ResetFilteredBookCollectionCommand()
        {
            resetFilteredBookCollection();
        }
        private void resetFilteredBookCollection()
        {
            FilteredBookCollection.Clear();
            foreach (BookViewModel book in BookCollection)
            {
                FilteredBookCollection.Add(book);
            }
        }
    }
}