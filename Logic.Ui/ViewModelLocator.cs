using De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels;
using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;

namespace De.HsFlensburg.ClientApp011.Logic.Ui
{
    public class ViewModelLocator
    {
        public MainWindowViewModel TheMainWindowViewModel { get; set; }
        public BookCollectionViewModel TheBookCollectionViewModel { get; set; }
        public PrintServiceWindowViewModel ThePrintServiceWindowViewModel { get; }
        public TexBookCollectionWindowViewModel TheTexBookCollectionWindowViewModel { get; set; }
        public BookSearchWindowViewModel TheBookSearchWindowViewModel { get; }
        public NewBookWindowViewModel TheNewBookViewModel { get; set; }

        public ViewModelLocator()
        {
            TheBookCollectionViewModel = new BookCollectionViewModel();
            TheMainWindowViewModel = new MainWindowViewModel(TheBookCollectionViewModel);
            ThePrintServiceWindowViewModel = new PrintServiceWindowViewModel(TheBookCollectionViewModel);
            TheTexBookCollectionWindowViewModel = new TexBookCollectionWindowViewModel(TheBookCollectionViewModel);
            TheBookSearchWindowViewModel = new BookSearchWindowViewModel(TheBookCollectionViewModel);
            TheNewBookViewModel = new NewBookWindowViewModel(TheBookCollectionViewModel);
        }
    }
}