using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects.ENUM;

namespace De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects
{
    [Serializable]
    public class Book: INotifyPropertyChanged
    {
        private String title;
        private String author;
        private Decimal price;
        private String description;
        private DateTime releaseDate;
        private String publisher;
        private int pages;
        private int weight;
        private String isbn;
        private double rating;
        private int edition;
        private Boolean bestseller;
        private Image cover;
        private String extract;
        private Genre genre;
        private Format format;
        private Language language;
        private Dimension dimension;

        public String Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public String Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }
        public Decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }
        public DateTime ReleaseDate
        {
            get
            {
                return releaseDate;
            }
            set
            {
                releaseDate = value;
                OnPropertyChanged("ReleaseDate");
            }
        }
        public String Publisher
        {
            get
            {
                return publisher;
            }
            set
            {
                publisher = value;
                OnPropertyChanged("Publisher");
            }
        }
        public String Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public int Pages
        {
            get
            {
                return pages;
            }
            set
            {
                pages = value;
                OnPropertyChanged("Pages");
            }
        }
        public int Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
                OnPropertyChanged("Weight");
            }
        }
        public String Isbn
        {
            get
            {
                return isbn;
            }
            set
            {
                isbn = value;
                OnPropertyChanged("Isbn");
            }
        }
        public double Rating
        {
            get
            {
                return rating;
            }
            set
            {
                rating = value;
                OnPropertyChanged("Rating");
            }
        }
        public int Edition
        {
            get
            {
                return edition;
            }
            set
            {
                edition = value;
                OnPropertyChanged("Edition");
            }
        }
        public bool Bestseller
        {
            get
            {
                return bestseller;
            }
            set
            {
                bestseller = value;
                OnPropertyChanged("Bestseller");
            }
        }
        public String Extract
        {
            get
            {
                return extract;
            }
            set
            {
                extract = value;
                OnPropertyChanged("Extract");
            }
        }
        public Image Cover
        {
            get
            {
                return cover;
            }
            set
            {
                cover = value;
                OnPropertyChanged("Cover");
            }
        }
        public Genre Genre
        {
            get
            {
                return genre;
            }
            set
            {
                genre = value;
                OnPropertyChanged("Genre");
            }
        }
        public Format Format
        {
            get
            {
                return format;
            }
            set
            {
                format = value;
                OnPropertyChanged("Format");
            }
        }
        public Language Language
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
                OnPropertyChanged("Language");
            }
        }
        public Dimension Dimension
        {
            get
            {
                return dimension;
            }
            set
            {
                dimension = value;
                OnPropertyChanged("Dimension");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
    }
}