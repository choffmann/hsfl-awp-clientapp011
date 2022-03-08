using De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp011.Logic.Ui.Base;
using System.ComponentModel;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper
{
    public class BookCollectionViewModel : ViewModelSyncCollection
        <BookViewModel, Book, BookCollection>
    {
        public override void NewModelAssigned()
        {
            foreach (var bvm in this)
            {
                var modelPropChanged = bvm.Model as INotifyPropertyChanged;
                if (modelPropChanged != null)
                {
                    modelPropChanged.PropertyChanged += bvm.OnPropertyChangedInModel;
                }
            }
        }
    }
}
