using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;
using De.HsFlensburg.ClientApp011.Services.PrintService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels
{
    public class PrintServiceWindowViewModel
    {
        public BookCollectionViewModel BookList { get; set; }
        public BookCollectionViewModel CheckedBooks { get; set; }
        public ICommand PrintBooks { get; }
        public ICommand CloseWindow { get; }
        public ICommand AddSelectedBookToCollection { get; }
        public ICommand RemoveSelectedBookToCollection { get; }

        public PrintServiceWindowViewModel(BookCollectionViewModel bookCollectionViewModel)
        {
            PrintBooks = new RelayCommand(PrintBooksCommand);
            CloseWindow = new RelayCommand(param => CloseWindowCommand(param));
            AddSelectedBookToCollection = new RelayCommand(param => AddSelectedBookToCollectionCommand(param));
            RemoveSelectedBookToCollection = new RelayCommand(param => RemoveSelectedBookToCollectionCommand(param));
            BookList = bookCollectionViewModel;
            CheckedBooks = new BookCollectionViewModel();
        }

        private void PrintBooksCommand()
        {
            PrintBookService printer = new PrintBookService(CheckedBooks.Model);
            printer.Printing();
        }

        private void CloseWindowCommand(object param)
        {
            Window window = (Window)param;
            window.Close();
        }
        private void AddSelectedBookToCollectionCommand(object param)
        {
            // Save System.Windows.Controls.SelectedItemCollection to IList to Cast to List<BookViewModel>
            // https://stackoverflow.com/questions/1877949/how-to-cast-a-system-windows-controls-selecteditemcollection
            System.Collections.IList items = (System.Collections.IList)param;
            var collection = items.Cast<BookViewModel>();

            foreach (BookViewModel book in collection)
            {
                if(!CheckItemIsInCheckedBooks(book))
                {
                    CheckedBooks.Add(book);
                }
            }
        }
        private void RemoveSelectedBookToCollectionCommand(object param)
        {
            // Save System.Windows.Controls.SelectedItemCollection to IList to Cast to List<BookViewModel>
            // https://stackoverflow.com/questions/1877949/how-to-cast-a-system-windows-controls-selecteditemcollection
            System.Collections.IList items = (System.Collections.IList)param;
            var collection = items.Cast<BookViewModel>();

            foreach (BookViewModel book in collection)
            {
                CheckedBooks.Remove(book);
            }
        }

        private bool CheckItemIsInCheckedBooks(BookViewModel currentBook)
        {
            foreach(BookViewModel book in CheckedBooks)
            {
                if(book.Equals(currentBook))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
