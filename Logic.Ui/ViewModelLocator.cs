﻿using De.HsFlensburg.ClientApp011.Logic.Ui.ViewModels;
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
        public BookCollectionViewModel TheBookCollectionViewModel { get; set; }
        public MainWindowViewModel TheMainWindowViewModel { get; set; }
        public NewClientWindowViewModel TheNewClientWindowViewModel { get; set; }

        public ViewModelLocator()
        {
            TheBookCollectionViewModel = new BookCollectionViewModel();
            TheMainWindowViewModel = new MainWindowViewModel(TheBookCollectionViewModel);
            TheNewClientWindowViewModel = new NewClientWindowViewModel(TheBookCollectionViewModel);
        }
    }
}
