using De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp011.Logic.Ui.Base;
using System;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper
{
    public class DimensionViewModel : ViewModelBase<Dimension>
    {
        public DimensionViewModel() : base()
        {

        }
        public int Height
        {
            get
            {
                return Model.Height;
            }
            set
            {
                Model.Height = value;
            }
        }
        public int Width
        {
            get
            {
                return Model.Width;
            }
            set
            {
                Model.Width = value;
            }
        }
        public int Depth
        {
            get
            {
                return Model.Depth;
            }
            set
            {
                Model.Depth = value;
            }
        }
        public override void NewModelAssigned()
        {
            throw new NotImplementedException();
        }
    }
}
