using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels
{
    public class NewBookViewModel
    {
        public BookViewModel BookVM { get; set; }
        //public String Author { get; set; }
        //private Decimal Price { get; set; }
        //private String Description { get; set; }
        //private DateTime ReleaseDate { get; set; }
        //private String Publisher { get; set; }
        //private int Pages { get; set; }
        //private int Weight { get; set; }
        //private String Isbn { get; set; }
        //private int Edition { get; set; }
        //private Boolean Bestseller { get; set; }
        //private Image Cover { get; set; }
        //private String Extract { get; set; }
        //private Genre Genre { get; set; }
        //private Format Format { get; set; }
        //private Language Language { get; set; }
        //private Dimension Dimension { get; set; }

        public ICommand AddBook { get; }
        public BookCollectionViewModel MyBookCollection { get; set; }

        public NewBookViewModel(BookCollectionViewModel bookCollectionViewModel)
        {
            AddBook = new RelayCommand(AddBookMethod);
            MyBookCollection = bookCollectionViewModel;
        }

        private void AddBookMethod()
        {

            MyBookCollection.Add(BookVM);
        }
    }
}
