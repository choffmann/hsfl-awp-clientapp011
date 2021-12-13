﻿using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;
using De.HsFlensburg.ClientApp011.Services.PrintService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels
{
    public class PrintServiceWindowViewModel: INotifyPropertyChanged
    {
        public BookCollectionViewModel BookList { get; set; }
        public BookCollectionViewModel CheckedBooks { get; set; }
        public ICommand PrintBooks { get; }

        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if(isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public PrintServiceWindowViewModel(BookCollectionViewModel bookCollectionViewModel)
        {
            PrintBooks = new RelayCommand(PrintBooksCommand);
            BookList = bookCollectionViewModel;
            CheckedBooks = new BookCollectionViewModel();
            CreateDemoBooks();
        }

        private void CreateDemoBooks()
        {
            // DEMO BOOKS
            Console.WriteLine("Create Demo Books...");
            for (int i = 1; i <= 10; i++)
            {
                BookViewModel book = new BookViewModel();
                book.Title = "Book #" + i;
                book.Author = "Author #" + i;
                book.Publisher = "Publisher #" + i;
                book.Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut";
                book.Extract = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";
                book.Genre = Business.Model.BusinessObjects.ENUM.Genre.Horror;
                book.Language = Business.Model.BusinessObjects.ENUM.Language.Deutsch;
                book.Format = Business.Model.BusinessObjects.ENUM.Format.Taschenbuch;
                book.Price = 2.3M;
                book.Pages = 120;
                book.Weight = 10;
                book.Isbn = "ISBN " + i + "-" + i + 7645 + "-" + i + 2641 + "-" + i + 1;
                book.Rating = 2.4;
                book.Edition = i;
                book.Bestseller = i % 2 == 0;
                book.ReleaseDate = new DateTime();

                Business.Model.BusinessObjects.Dimension dimension = new Business.Model.BusinessObjects.Dimension();
                dimension.Depth = 1 * (i + 1);
                dimension.Height = 2 * (i + 1);
                dimension.Width = 3 * (i + 1);
                book.Dimension = dimension;

                Image image = Image.FromFile(@"C:\Users\hoffmann\Documents\FH Flensburg\AWP\image\" + i + ".png");
                book.Cover = image;
                BookList.Add(book);
            }
        }

        private void PrintBooksCommand()
        {
            PrintBookService printer = new PrintBookService(CheckedBooks.Model);
            printer.Printing();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
