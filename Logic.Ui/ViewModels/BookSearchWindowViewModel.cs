using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;
using System;
using System.Windows.Input;
using System.Windows.Controls;
using System.ComponentModel;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels
{
    /// <summary>
    /// ViewModel for BookSearcher Feature
    /// </summary>
    public class BookSearchWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Variables to store the combobox textes
        /// </summary>
        private string genreComboBoxText = "Genre";
        private string formatComboBoxText = "Buchart";
        private string ratingComboBoxText = "Bewertung";

        /// <summary>
        /// Variale to ensure that combobox filters only come from user
        /// </summary>
        private bool filterComboBoxes = true;

        /// <summary>
        /// BookCollection that contains all available BookViewModel
        /// </summary>
        public BookCollectionViewModel BookCollection { get; set; }

        /// <summary>
        /// BookCollectionViewModel that contains all BookViewModels from
        /// BookCollection which to the filters
        /// </summary>
        public BookCollectionViewModel FilteredBookCollection { get; set; }

        /// <summary>
        /// Variable to store OnPropertyChanged delegate
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Properties for binding the Combobox.Text Property
        /// </summary>
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

        /// <summary>
        /// ICommand Properties which are available for the view
        /// </summary>
        public ICommand ResetView { get; }
        public ICommand ShowBestseller { get; }
        public ICommand ShowNovelties { get; }
        public ICommand ShowSpecificGenre { get; }
        public ICommand ShowSpecificFormat { get; }
        public ICommand ShowSpecificRating { get; }
        public ICommand ShowTextFilteredBooks { get; }

        /// <summary>
        /// Class constructor instantiate the BookCollectionViewModels and ICommand properties
        /// </summary>
        /// <param name="bookCollectionViewModel"></param>
        public BookSearchWindowViewModel(BookCollectionViewModel bookCollectionViewModel)
        {
            BookCollection = bookCollectionViewModel;
            FilteredBookCollection = new BookCollectionViewModel();
            ResetView = new RelayCommand(param => ResetViewCommand(param));
            ShowBestseller = new RelayCommand(param => ShowBestsellerCommand());
            ShowNovelties = new RelayCommand(param => ShowNoveltiesCommand());
            ShowSpecificGenre = new RelayCommand(param => ShowSpecificGenreCommand(param));
            ShowSpecificFormat = new RelayCommand(param => ShowSpecificFormatCommand(param));
            ShowSpecificRating = new RelayCommand(param => ShowSpecificRatingCommand(param));
            ShowTextFilteredBooks = new RelayCommand(param => ShowTextFilteredBooksCommand(param));
        }

        /// <summary>
        /// Method fires PropertyChanged Event for properties of the viewmodel
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Method to resets view
        /// </summary>
        /// <param name="param"></param>
        private void ResetViewCommand(object param)
        {
            ResetFilteredBookCollection();
            ResetComboBoxes((WrapPanel)param);
        }

        /// <summary>
        /// Method to get the bestseller from the BookCollection
        /// </summary>
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

        /// <summary>
        /// Method to get the novelties from the BookCollection
        /// </summary>
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

        /// <summary>
        /// Method to get speficic genre from the BookCollection
        /// </summary>
        /// <param name="param"></param>
        private void ShowSpecificGenreCommand(object param)
        {
            if (filterComboBoxes)
            {
                FilteredBookCollection.Clear();
                ComboBox genreComboBox = (ComboBox)((SelectionChangedEventArgs)param).Source;
                string searchedGenre = genreComboBox.SelectedValue?.ToString() ?? "";
                foreach (BookViewModel book in BookCollection)
                {
                    if (book.Genre.ToString() == searchedGenre 
                        || genreComboBox.SelectedIndex == -1)
                    {
                        FilteredBookCollection.Add(book);
                    }
                }
            }
        }

        /// <summary>
        /// Methodd to get specific format from the BookCollection
        /// </summary>
        /// <param name="param"></param>
        private void ShowSpecificFormatCommand(object param)
        {
            if (filterComboBoxes)
            {
                FilteredBookCollection.Clear();
                ComboBox formatComboBox = (ComboBox)((SelectionChangedEventArgs)param).Source;
                string searchedFormat = formatComboBox.SelectedValue?.ToString() ?? "";
                foreach (BookViewModel book in BookCollection)
                {
                    if (book.Format.ToString() == searchedFormat 
                        || formatComboBox.SelectedIndex == -1)
                    {
                        FilteredBookCollection.Add(book);
                    }
                }
            }
        }

        /// <summary>
        /// Method to get specific rated books from the BookCollection
        /// </summary>
        /// <param name="param"></param>
        private void ShowSpecificRatingCommand(object param)
        {
            if (filterComboBoxes)
            {
                FilteredBookCollection.Clear();
                ComboBox ratingComboBox = (ComboBox)((SelectionChangedEventArgs)param).Source;
                ComboBoxItem selectedComboBoxItem = (ComboBoxItem)ratingComboBox.SelectedItem;
                foreach (BookViewModel book in BookCollection)
                {
                    if (book.Rating >= Convert.ToInt32(selectedComboBoxItem?.Tag)
                        || ratingComboBox.SelectedIndex == -1)
                    {
                        FilteredBookCollection.Add(book);
                    }
                }
            }
        }

        /// <summary>
        /// Method to get specific books which contain searched input from BookCollections
        /// </summary>
        /// <param name="param"></param>
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

        /// <summary>
        /// Method to reset the FilteredBookCollection and show all books from BookCollection
        /// </summary>
        private void ResetFilteredBookCollection()
        {
            FilteredBookCollection.Clear();
            foreach (BookViewModel book in BookCollection)
            {
                FilteredBookCollection.Add(book);
            }
        }

        /// <summary>
        /// Method to reset the comboboxes in the view
        /// </summary>
        /// <param name="comboBoxWrapPanel"></param>
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