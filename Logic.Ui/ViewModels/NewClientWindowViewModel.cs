using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels
{
    public class NewClientWindowViewModel
    {
        public int Identifier { get; set; }
        public string Name { get; set; }
        private BookCollectionViewModel bookCollectionViewModel;
        public ICommand AddClient { get; }
        public NewClientWindowViewModel(BookCollectionViewModel viewModelCollection)
        {
            AddClient = new RelayCommand(AddClientMethod);
            bookCollectionViewModel = viewModelCollection;
        }
        private void AddClientMethod()
        {
            BookViewModel bvm = new BookViewModel();
            bvm.Author = Identifier.ToString(); /* Nur für die Abgabe, 
                                                 * da das später ohnehin überarbeitet wird. */
            bvm.Title = Name;
            bookCollectionViewModel.Add(bvm);
        }
    }
}
