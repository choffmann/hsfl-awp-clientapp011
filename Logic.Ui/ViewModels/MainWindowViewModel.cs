using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;
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
        public ICommand FillBookList { get; set; }
        public BookCollectionViewModel BookCollection { get; set; }
        public MainWindowViewModel(BookCollectionViewModel bookCollectionViewModel)
        {
            BookCollection = bookCollectionViewModel;
            FillBookList = new RelayCommand(FillBookListCommand);
        }

        private void FillBookListCommand()
        {
            // DEMO BOOKS
            Console.WriteLine("Create Demo Books...");
            for (int i = 1; i <= 10; i++)
            {
                BookViewModel book = new BookViewModel();
                book.Title = "Book #" + i;
                book.Author = "Author #" + i;
                book.Publisher = "Publisher #" + i;
                book.Description = "Lorem ipsum dolor sit amet, ...";
                book.Extract = "Lorem ipsum dolor sit amet, ...";
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

                // Root Directory of Repository => \img\
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\img\\" + i + ".png";
                Image image = Image.FromFile(path);
                book.Cover = image;
                BookCollection.Add(book);
            }
        }
    }
}
