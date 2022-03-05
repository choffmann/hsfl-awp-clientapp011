using De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects.ENUM;
using De.HsFlensburg.ClientApp011.Logic.Ui.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace De.HsFlensburg.ClientApp011.Logic.Ui.Wrapper
{
    public class BookViewModel : ViewModelBase<Book>
    {
        public String Title
        {
            get
            {
                return Model.Title;
            }
            set
            {
                Model.Title = value;
            }
        }
        public String Author
        {
            get
            {
                return Model.Author;
            }
            set
            {
                Model.Author = value;
            }
        }

        public Decimal Price
        {
            get
            {
                return Model.Price;
            }
            set
            {
                Model.Price = value;
            }
        }

        public DateTime ReleaseDate
        {
            get
            {
                return Model.ReleaseDate;
            }
            set
            {
                Model.ReleaseDate = value;
            }
        }

        public String Publisher
        {
            get
            {
                return Model.Publisher;
            }
            set
            {
                Model.Publisher = value;
            }
        }

        public String Description
        {
            get
            {
                return Model.Description;
            }
            set
            {
                Model.Description = value;
            }
        }

        public int Pages
        {
            get
            {
                return Model.Pages;
            }
            set
            {
                Model.Pages = value;
            }
        }

        public int Weight
        {
            get
            {
                return Model.Weight;
            }
            set
            {
                Model.Weight = value;
            }
        }

        public String Isbn
        {
            get
            {
                return Model.Isbn;
            }
            set
            {
                Model.Isbn = value;
            }
        }

        public double Rating
        {
            get
            {
                return Model.Rating;
            }
            set
            {
                Model.Rating = value;
            }
        }

        public int Edition
        {
            get
            {
                return Model.Edition;
            }
            set
            {
                Model.Edition = value;
            }
        }

        public bool Bestseller
        {
            get
            {
                return Model.Bestseller;
            }
            set
            {
                Model.Bestseller = value;
            }
        }

        public String Extract
        {
            get
            {
                return Model.Extract;
            }
            set
            {
                Model.Extract = value;
            }
        }

        public BitmapImage BindableCoverImage
        {
            get
            {
                if (Model.Cover != null)
                {
                    MemoryStream localMemoryStream = new MemoryStream();
                    Model.Cover.Save(localMemoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    localMemoryStream.Position = 0;
                    BitmapImage localBitmapImage = new BitmapImage();
                    localBitmapImage.BeginInit();
                    localBitmapImage.StreamSource = localMemoryStream;
                    localBitmapImage.EndInit();
                    return localBitmapImage;
                }
                else
                {
                    return null;
                }

            }
            private set { }
        }
        public Image Cover
        {
            get
            {
                return Model.Cover;
            }
            set
            {
                Model.Cover = value;
                OnPropertyChanged("Cover");
                OnPropertyChanged("BindableCoverImage");
            }
        }
        public Genre Genre
        {
            get
            {
                return Model.Genre;
            }
            set
            {
                Model.Genre = value;
            }
        }
        public String[] GenreValues
        {
            get
            {
                List<String> value = new List<String>();
                foreach (var e in Enum.GetValues(typeof(Genre)))
                {
                    value.Add(e.ToString());
                }
                return value.ToArray<String>();
            }
        }

        public Format Format
        {
            get
            {
                return Model.Format;
            }
            set
            {
                Model.Format = value;
            }
        }
        public String[] FormatValues
        {
            get
            {
                List<String> value = new List<String>();
                foreach (var e in Enum.GetValues(typeof(Format)))
                {
                    value.Add(e.ToString());
                }
                return value.ToArray<String>();
            }
        }

        public Language Language
        {
            get
            {
                return Model.Language;
            }
            set
            {
                Model.Language = value;
            }
        }
        public String[] LanguageValues
        {
            get
            {
                List<String> value = new List<String>();
                foreach (var e in Enum.GetValues(typeof(Language)))
                {
                    value.Add(e.ToString());
                }
                return value.ToArray<String>();
            }
        }

        public Dimension Dimension
        {
            get
            {
                return Model.Dimension;
            }
            set
            {
                Model.Dimension = value;
            }
        }

        public override void NewModelAssigned()
        {
            throw new NotImplementedException();
        }
    }
}
