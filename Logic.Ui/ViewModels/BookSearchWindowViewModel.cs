using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;
using System;
using System.Windows.Input;
using System.Windows.Controls;
using System.ComponentModel;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels
{
    public class BookSearchWindowViewModel : INotifyPropertyChanged
    {
        private string genreComboBoxText = "Genre";
        private string formatComboBoxText = "Buchart";
        private string ratingComboBoxText = "Bewertung";
        private bool filterComboBoxes = true;
        public BookCollectionViewModel BookCollection { get; set; }
        public BookCollectionViewModel FilteredBookCollection { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public string GenreComboBoxText
        {
            get
            {
                return genreComboBoxText;             
            }
            set
            {
                genreComboBoxText = value;
                OnPropertyChanged("GenreComboBoxText");
            }
        }
        public string FormatComboBoxText
        {
            get
            {
                return formatComboBoxText;
            }
            set
            {
                formatComboBoxText = value;
                OnPropertyChanged("FormatComboBoxText");
            }
        }
        public string RatingComboBoxText
        {
            get
            {
                return ratingComboBoxText;
            }
            set
            {
                ratingComboBoxText = value;
                OnPropertyChanged("RatingComboBoxText");
            }
        }
        public ICommand WindowLoaded { get; }
        public ICommand ResetFilter { get; }
        public ICommand ShowBestseller { get; }
        public ICommand ShowNovelties { get; }
        public ICommand ShowSpecificGenre { get; }
        public ICommand ShowSpecificFormat { get; }
        public ICommand ShowSpecificRating { get; }
        public ICommand ShowTextFilteredBooks { get; }

        public BookSearchWindowViewModel(BookCollectionViewModel bookCollectionViewModel)
        {
            BookCollection = bookCollectionViewModel;
            FilteredBookCollection = new BookCollectionViewModel();
            WindowLoaded = new RelayCommand(param => WindowLoadedCommand(param));
            ShowBestseller = new RelayCommand(param => ShowBestsellerCommand());
            ShowNovelties = new RelayCommand(param => ShowNoveltiesCommand());
            ShowSpecificGenre = new RelayCommand(param => ShowSpecificGenreCommand(param));
            ShowSpecificFormat = new RelayCommand(param => ShowSpecificFormatCommand(param));
            ShowSpecificRating = new RelayCommand(param => ShowSpecificRatingCommand(param));
            ShowTextFilteredBooks = new RelayCommand(param => ShowTextFilteredBooksCommand(param));
            ResetFilter = new RelayCommand(param => ResetFilterCommand(param));
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void WindowLoadedCommand(object param)
        {
            ResetFilteredBookCollection();
            ResetComboBoxes((WrapPanel)param);
        }

        private void ShowBestsellerCommand()
        {
            FilteredBookCollection.Clear();
            foreach (BookViewModel book in BookCollection)
            {
                if (book.Bestseller)
                {
                    FilteredBookCollection.Add(book);
                }
            }
        }
        private void ShowNoveltiesCommand()
        {
            FilteredBookCollection.Clear();
            foreach (BookViewModel book in BookCollection)
            {
                if (book.ReleaseDate > DateTime.Today.AddMonths(-3))
                {
                    FilteredBookCollection.Add(book);
                }
            }
        }
        private void ShowSpecificGenreCommand(object param)
        {
            if (filterComboBoxes)
            {
                FilteredBookCollection.Clear();
                ComboBox genreComboBox = (ComboBox)((SelectionChangedEventArgs)param).Source;
                string searchedGenre = genreComboBox.SelectedValue?.ToString() ?? "";
                foreach (BookViewModel book in BookCollection)
                {
                    if (book.Genre.ToString() == searchedGenre || 
                        genreComboBox.SelectedIndex == -1)
                    {
                        FilteredBookCollection.Add(book);
                    }
                }
            }
        }
        private void ShowSpecificFormatCommand(object param)
        {
            if (filterComboBoxes)
            {
                FilteredBookCollection.Clear();
                ComboBox formatComboBox = (ComboBox)((SelectionChangedEventArgs)param).Source;
                string searchedFormat = formatComboBox.SelectedValue?.ToString() ?? "";
                foreach (BookViewModel book in BookCollection)
                {
                    if (book.Format.ToString() == searchedFormat || 
                        formatComboBox.SelectedIndex == -1)
                    {
                        FilteredBookCollection.Add(book);
                    }
                }
            }
        }
        private void ShowSpecificRatingCommand(object param)
        {
            if (filterComboBoxes)
            {
                FilteredBookCollection.Clear();
                ComboBox ratingComboBox = (ComboBox)((SelectionChangedEventArgs)param).Source;
                ComboBoxItem selectedComboBoxItem = (ComboBoxItem)ratingComboBox.SelectedItem;
                foreach (BookViewModel book in BookCollection)
                {
                    if (book.Rating >= Convert.ToInt32(selectedComboBoxItem?.Tag) || 
                        ratingComboBox.SelectedIndex == -1)
                    {
                        FilteredBookCollection.Add(book);
                    }
                }
            }
        }
        private void ShowTextFilteredBooksCommand(object param)
        {
            FilteredBookCollection.Clear();
            string searchInput = ((TextBox)((TextChangedEventArgs)param).OriginalSource).Text;
            if(searchInput == "")
            {
                ResetFilteredBookCollection();
            } else {
                foreach (BookViewModel book in BookCollection)
                {
                    string textFields = book.Author + book.Genre.ToString() + book.Title
                        + book.Isbn + book.Publisher + book.Description + book.Language.ToString()
                        + book.Extract;
                    if (textFields.Contains(searchInput))
                    {
                        FilteredBookCollection.Add(book);
                    }
                }
            }
        }
        private void ResetFilterCommand(object param)
        {
            ResetFilteredBookCollection();
            ResetComboBoxes((WrapPanel)param);
        }
        private void ResetFilteredBookCollection()
        {
            FilteredBookCollection.Clear();
            foreach (BookViewModel book in BookCollection)
            {
                FilteredBookCollection.Add(book);
            }
        }
        private void ResetComboBoxes(WrapPanel comboBoxWrapPanel)
        {
            filterComboBoxes = false;
            foreach (Control control in comboBoxWrapPanel.Children)
            {
                if (control is ComboBox box)
                {
                    box.SelectedIndex = -1;
                }
            }
            filterComboBoxes = true;
            GenreComboBoxText = "Genre";
            FormatComboBoxText = "Buchart";
            RatingComboBoxText = "Bewertung";
        }
    }
}