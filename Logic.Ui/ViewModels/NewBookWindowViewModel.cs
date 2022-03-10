using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Text.RegularExpressions;
using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels
{
    public class NewBookWindowViewModel
    {
        public BookViewModel BookVM { get; set; }
        public RelayCommand AddBook { get; }
        public RelayCommand OpenFileDialog { get; }
        public RelayCommand CloseWindow { get; }
        public BookCollectionViewModel MyBookCollectionVM { get; set; }

        public NewBookWindowViewModel(BookCollectionViewModel bookCollectionViewModel)
        {
            AddBook = new RelayCommand(grid => AddBookMethod(grid));
            OpenFileDialog = new RelayCommand(OpenFileDialogMethod);
            CloseWindow = new RelayCommand(param => CloseWindowMethod(param));
            MyBookCollectionVM = bookCollectionViewModel;
            BookVM = new BookViewModel();
        }

        private void AddBookMethod(object container)
        {
            // Check ISBN Format
            //Regex checkIsbn = new Regex(@"^(?:ISBN)? ?\d{3}[- ]\d[- ]\d{3}[- ]\d{5}[- ]\d$|^(?:ISBN)? ?\d[- ]\d{3}[- ]\d{5}[- ]\d$");
            Match match = Regex.Match(BookVM.Isbn, @"^(?:ISBN)? ?\d{3}[- ]\d*[- ]\d*[- ]\d*[- ]\d$");
            if (!match.Success)
            {
                MessageBox.Show("Die ISBN hat nicht das richtige Format. \n" +
                    "Bitte korrigieren Sie den Eintrag und versuchen Sie es erneut."
                    , "Fehler im Eintrag", MessageBoxButton.OK);
            }
            else if (BookVM.Rating < 0 || BookVM.Rating > 0)
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
            }

            //ClearUIFields((Grid)container);
        }
        private void OpenFileDialogMethod()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            BookVM.Cover = System.Drawing.Image.FromFile(ofd.FileName);
        }
        private void CloseWindowMethod(object window)
        {
            Window locWindow = (Window)window;
            locWindow.Close();
        }
        private void ClearUIFields(Grid container)
        {
            foreach (Control ctl in container.Children)
            {
                if (ctl.GetType() == typeof(CheckBox))
                {
                    ((CheckBox)ctl).IsChecked = false;
                }
                else if (ctl.GetType() == typeof(TextBox))
                {
                    ((TextBox)ctl).Text = String.Empty;
                }
                /*else if (ctl.GetType() == typeof(Grid))
                {
                    ClearUIFields((Grid)ctl);
                }*/
            }
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
