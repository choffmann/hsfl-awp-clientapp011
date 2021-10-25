using De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp011.Logic.Ui.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper
{
    public class ClientViewModel: ViewModelBase<Client>
    {
        public int Id
        {
            get
            {
                return Model.Id;
            }
            set
            {
                Model.Id = value;
            }
        }
        public String Name
        {
            get
            {
                return Model.Name;
            }
            set
            {
                Model.Name = value;
            }
        }

        public override void NewModelAssigned()
        {
            throw new NotImplementedException();
        }
    }
}
