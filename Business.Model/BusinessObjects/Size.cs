using De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects
{
    public class Size
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }

        public int GetHeight(Unit unit)
        {
            // TODO: convert in unit
            return 0;
        }
        public int GetWidth(Unit unit)
        {
            // TODO: convert in unit
            return 0;
        }
        public int GetDepth(Unit unit)
        {
            // TODO: convert in unit
            return 0;
        }
    }
}
