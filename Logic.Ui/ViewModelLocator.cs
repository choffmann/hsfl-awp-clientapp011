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
        public BookCollectionViewModel TheBookCollectionViewModel { get; set; }
        public MainWindowViewModel TheMainWindowViewModel { get; set; }
        public NewClientWindowViewModel TheNewClientWindowViewModel { get; set; }
        public PrintServiceWindowViewModel ThePrintServiceWindowViewModle { get; set; }

        public ViewModelLocator()
        {
            TheClientCollectionViewModel = new ClientCollectionViewModel();
            TheBookCollectionViewModel = new BookCollectionViewModel();

            TheMainWindowViewModel = new MainWindowViewModel(TheClientCollectionViewModel);
            TheNewClientWindowViewModel = new NewClientWindowViewModel(TheClientCollectionViewModel);
            ThePrintServiceWindowViewModle = new PrintServiceWindowViewModel(TheBookCollectionViewModel);
        }
    }
}
