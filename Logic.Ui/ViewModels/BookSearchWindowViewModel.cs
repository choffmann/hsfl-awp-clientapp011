using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels
{
    public class BookSearchWindowViewModel
    {
        public BookCollectionViewModel BookCollection { get; set; }

        public BookCollectionViewModel FilteredBookCollection { get; set; }

        public ICommand CloseWindow { get; }

        public ICommand ActivateFilter { get; }

        public ICommand ResetFilter { get; }

        public ICommand ShowBestseller { get; }
        public ICommand ShowNovelties { get; }

        public BookSearchWindowViewModel(BookCollectionViewModel bookCollectionViewModel)
        {
            BookCollection = bookCollectionViewModel;
            FilteredBookCollection = BookCollection;
            CloseWindow = new RelayCommand(param => CloseWindowCommand(param));
            ShowBestseller = new RelayCommand(param => ShowBestsellerCommand());
            ShowNovelties = new RelayCommand(param => ShowNoveltiesCommand());
            ResetFilter = new RelayCommand(param => ResetFilteredBookCollectionCommand());
        }
        private void CloseWindowCommand(object param)
        {
            FilteredBookCollection = BookCollection;
            Window window = (Window)param;
            window.Close();
        }
        private void ShowBestsellerCommand()
        {
            FilteredBookCollection = new BookCollectionViewModel();
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
            FilteredBookCollection = new BookCollectionViewModel();
            foreach(BookViewModel book in BookCollection)
            {
                if (book.ReleaseDate > DateTime.Today.AddMonths(-3))
                {
                    FilteredBookCollection.Add(book);
                }
            }
        }
        private void ResetFilteredBookCollectionCommand()
        {
            FilteredBookCollection = BookCollection;
        }
    }
}