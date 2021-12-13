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
        public MainWindowViewModel TheMainWindowViewModel { get; set; }
        public BookCollectionViewModel TheBookCollectionViewModel { get; set; }

        public ViewModelLocator()
        {
            TheBookCollectionViewModel = new BookCollectionViewModel();
            TheMainWindowViewModel = new MainWindowViewModel(TheBookCollectionViewModel);
        }
    }
}
