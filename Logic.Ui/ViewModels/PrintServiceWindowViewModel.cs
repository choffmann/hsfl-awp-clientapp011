using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels
{
    public class PrintServiceWindowViewModel
    {
        private int selectedBookIndex = 0;
        public int SelectedBookIndex 
        { 
            get
            {
                return selectedBookIndex;
            }
            set
            {
                selectedBookIndex = value;
                Console.WriteLine(BookList[selectedBookIndex].Title);
                CurrentBook = BookList[selectedBookIndex];
            }
        }

        public BookCollectionViewModel BookList { get; set; }
        public BookViewModel CurrentBook { get; set; }
        public ICommand PrintBooks { get; }

        public PrintServiceWindowViewModel(BookCollectionViewModel bookCollectionViewModel)
        {
            PrintBooks = new RelayCommand(PrintBooksCommand);
            BookList = bookCollectionViewModel;
            CreateDemoBooks();
        }

        private void CreateDemoBooks()
        {
            Console.WriteLine("Create Demo Books...");

            // DEMO BOOKS
            for (int i = 1; i <= 10; i++)
            {
                BookViewModel book = new BookViewModel();
                book.Title = "Book #" + i;
                BookList.Add(book);
            }
        }

        private void PrintBooksCommand()
        {
            Console.WriteLine("print...");
        }
    }
}
