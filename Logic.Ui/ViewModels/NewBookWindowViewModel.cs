using Microsoft.Win32;
using System.Windows;
using De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper;
using System.Windows.Controls;
using System;
using System.Text.RegularExpressions;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels
{
    public class NewBookWindowViewModel
    {
        public BookViewModel BookVM { get; set; }
        /*
            public String Author { get; set; }
            private Decimal Price { get; set; }
            private String Description { get; set; }
            private DateTime ReleaseDate { get; set; }
            private String Publisher { get; set; }
            private int Pages { get; set; }
            private int Weight { get; set; }
            private String Isbn { get; set; }
            private int Edition { get; set; }
            private Boolean Bestseller { get; set; }
            private Image Cover { get; set; }
            private String Extract { get; set; }
            private Genre Genre { get; set; }
            private Format Format { get; set; }
            private Language Language { get; set; }
            private Dimension Dimension { get; set; }
        */

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
            //Console.WriteLine("container is of type: " + container.GetType());
            // Check ISBN Format
            //Regex checkIsbn = new Regex(@"^(?:ISBN)? ?\d{3}[- ]\d[- ]\d{3}[- ]\d{5}[- ]\d$|^(?:ISBN)? ?\d[- ]\d{3}[- ]\d{5}[- ]\d$");
            //Match match = Regex.Match(BookVM.Isbn, @"^(?:ISBN)? ?\d{3}[- ]\d[- ]\d{3}[- ]\d{5}[- ]\d$|^(?:ISBN)? ?\d[- ]\d{3}[- ]\d{5}[- ]\d$");
            if (Regex.Match(BookVM.Isbn, @"^(?:ISBN)? ?\d{3}[- ]\d[- ]\d{3}[- ]\d{5}[- ]\d$|^(?:ISBN)? ?\d[- ]\d{3}[- ]\d{5}[- ]\d$").Success)
            {
                MyBookCollectionVM.Add(BookVM);
            }
            else
            {

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
    }
}
