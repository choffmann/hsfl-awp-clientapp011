using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Text.RegularExpressions;
using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;
using System.Reflection;
using System.ComponentModel;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels
{
    public class NewBookWindowViewModel : INotifyPropertyChanged
    {
        private BookViewModel bookvm;
        public BookViewModel BookVM
        {
            get
            {
                return bookvm;
            }
            set
            {
                bookvm = value;
                OnPropertyChanged("BookVM");
            }
        }
        public RelayCommand AddBook { get; }
        public RelayCommand OpenFileDialog { get; }
        public RelayCommand CloseWindow { get; }
        public BookCollectionViewModel MyBookCollectionVM { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public NewBookWindowViewModel(BookCollectionViewModel bookCollectionViewModel)
        {
            AddBook = new RelayCommand(grid => AddBookMethod(grid));
            OpenFileDialog = new RelayCommand(OpenFileDialogMethod);
            CloseWindow = new RelayCommand(param => CloseWindowMethod(param));
            MyBookCollectionVM = bookCollectionViewModel;
            BookVM = new BookViewModel();
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void AddBookMethod(object container)
        {
            // Check ISBN Format
            //Regex checkIsbn = new Regex(@"^(?:ISBN)? ?\d{3}[- ]\d[- ]\d{3}[- ]\d{5}[- ]\d$|^(?:ISBN)? ?\d[- ]\d{3}[- ]\d{5}[- ]\d$");
            Match match = Regex.Match(BookVM?.Isbn + "", @"^(?:ISBN)? ?\d{3}[- ]\d*[- ]\d*[- ]\d*[- ]\d$");
            if (!match.Success)
            {
                MessageBox.Show("Die ISBN hat nicht das richtige Format. \n" +
                    "Bitte korrigieren Sie den Eintrag und versuchen Sie es erneut."
                    , "Fehler im Eintrag", MessageBoxButton.OK);
            }
            else if (BookVM.Rating < 0 || BookVM.Rating > 5)
            {
                MessageBox.Show("Das Rating ist außerhalb des gültigen Bereichs von 0 bis 5. \n" +
                    "Bitte korrigieren Sie den Eintrag und versuchen Sie es erneut."
                    , "Fehler im Eintrag", MessageBoxButton.OK);
            }
            else if (CheckDuplicateTitle(BookVM.Title))
            {
                MessageBox.Show("Der Buchtitle ist bereits im Book Manager vorhanden."
                    , "Fehler im Eintrag", MessageBoxButton.OK);
            }
            else
            {
                MyBookCollectionVM.Add(BookVM);
                // Workaround, damit Buch nicht laufend beim Anlegen überschrieben wird.
                BookVM = new BookViewModel();
            }
        }
        private void OpenFileDialogMethod()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            BookVM.Cover = System.Drawing.Image.FromFile(ofd.FileName);
        }
        private void CloseWindowMethod(object window)
        {
            BookVM = new BookViewModel();
            Window locWindow = (Window)window;
            locWindow.Close();
        }
        private bool CheckDuplicateTitle(String currentTitle)
        {
            bool result = false;
            for (int i = 0; i < MyBookCollectionVM.Count && !result; i++)
            {
                result = MyBookCollectionVM[i].Title.Equals(currentTitle);
            }
            return result;
        }
    }
}
