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
            BookList = bookCollectionViewModel;
            CheckedBooks = new BookCollectionViewModel();
            PrintBooks = new RelayCommand(PrintBooksCommand);
            CloseWindow = new RelayCommand(param => CloseWindowCommand(param));
            AddSelectedBookToCollection = new RelayCommand(param => AddSelectedBookToCollectionCommand(param));
            RemoveSelectedBookToCollection = new RelayCommand(param => RemoveSelectedBookToCollectionCommand(param));
        }

        // Command to add selected book to BookCollection "CheckedBooks"
        private void AddSelectedBookToCollectionCommand(object param)
        {
            // Save System.Windows.Controls.SelectedItemCollection to IList and Cast to List<BookViewModel>
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

        // Command to remove selected book of BookCollection "CheckedBooks"
        private void RemoveSelectedBookToCollectionCommand(object param)
        {
            // Save System.Windows.Controls.SelectedItemCollection to IList and Cast to List<BookViewModel>
            // https://stackoverflow.com/questions/1877949/how-to-cast-a-system-windows-controls-selecteditemcollection
            System.Collections.IList items = (System.Collections.IList)param;
            var collection = items.Cast<BookViewModel>();

            foreach (BookViewModel book in collection)
            {
                CheckedBooks.Remove(book);
            }
        }

        // Command to open PrintService
        private void PrintBooksCommand()
        {
            if(CheckedBooks.Count == 0)
            {
                // Show MessageBox if no book is selected
                string caption = "No Book Selected!";
                string msg = "No Book selected. You have to select at least one book!";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxIcon = MessageBoxImage.Information;
                MessageBox.Show(msg, caption, messageBoxButton, messageBoxIcon);
            } else
            {
                PrintBookService printer = new PrintBookService(CheckedBooks.Model);
                printer.Printing();
            }
        }

        // Command to Close Window
        private void CloseWindowCommand(object param)
        {
            CheckedBooks.Clear();
            Window window = (Window)param;
            window.Close();
        }

        // Helper Function to check if book is present in collection
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
