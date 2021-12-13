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

namespace De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels
{
    public class PrintServiceWindowViewModel
    {
        public BookCollectionViewModel BookList { get; set; }
        public BookCollectionViewModel CheckedBooks { get; set; }
        private BookViewModel selectedBook;
        public BookViewModel SelectedBook
        {
            get
            {
                return selectedBook;
            }
            set
            {
                if(value != null)
                {
                    Console.WriteLine("Add Book to Collection: " + value.Title);
                    selectedBook = value;
                    CheckedBooks.Add(selectedBook);
                }
            }
        }
        public ICommand PrintBooks { get; }
        public ICommand CloseWindow { get; }
        public ICommand Selected { get; }

        public PrintServiceWindowViewModel(BookCollectionViewModel bookCollectionViewModel)
        {
            PrintBooks = new RelayCommand(PrintBooksCommand);
            CloseWindow = new RelayCommand(param => CloseWindowCommand(param));
            Selected = new RelayCommand(param => SelectedCommand(param));
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
        private void SelectedCommand(object param)
        {
            Console.WriteLine(param);
        }
    }
}
