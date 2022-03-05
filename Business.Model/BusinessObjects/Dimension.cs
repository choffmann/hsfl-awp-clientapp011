using De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects.ENUM;
using System;

namespace De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects
{
    [Serializable]
    public class Dimension
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }

        public double GetHeight(Unit unit)
        {
            return ConvertInto(this.Height, unit);
        }
        public double GetWidth(Unit unit)
        {
            return ConvertInto(this.Width, unit);
        }
        public double GetDepth(Unit unit)
        {
            return ConvertInto(this.Depth, unit);
        }
        private double ConvertInto(double size, Unit unit)
        {
            // Values are stored in mm
            switch (unit.ToString())
            {
                case "m": return size / 1000;
                case "cm": return size / 10;
                case "inch": return size * 0.03937008;
                case "mm": return size;
                default: throw new Exception("unit is not recognized");
            }
        }
    }
}
