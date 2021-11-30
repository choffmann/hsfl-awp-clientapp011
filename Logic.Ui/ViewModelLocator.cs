using De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels;
using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De.HsFlensburg.ClientApp011.Logic.Ui
{
    public class ViewModelLocator
    {
        public ClientCollectionViewModel TheClientCollectionViewModel { get; set; }
        public MainWindowViewModel TheMainWindowViewModel { get; set; }
        public NewClientWindowViewModel TheNewClientWindowViewModel { get; set; }
        public BookCollectionViewModel TheBookCollectionViewModel { get; set; }
        public NewBookViewModel TheNewBookViewModel { get; set; }

        public ViewModelLocator()
        {
            TheClientCollectionViewModel = new ClientCollectionViewModel();
            TheMainWindowViewModel = new MainWindowViewModel(TheClientCollectionViewModel);
            TheNewClientWindowViewModel = new NewClientWindowViewModel(TheClientCollectionViewModel);
            
            TheBookCollectionViewModel = new BookCollectionViewModel();
            TheNewBookViewModel = new NewBookViewModel(TheBookCollectionViewModel);
        }
    }
}
