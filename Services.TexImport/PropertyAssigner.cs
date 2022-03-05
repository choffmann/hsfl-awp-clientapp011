using De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De.HsFlensburg.ClientApp011.Services.TexImport
{
    class PropertyAssigner
    {
        public PropertyAssigner(string property, string propertyContent, Book book)
        {   
            switch (property)
            {
                case "TITLE": 
                    book.Title = propertyContent;
                    break;

                case "AUTHOR":
                    book.Author = propertyContent;
                    break;

                case "PRICE":
                    book.Price = propertyContent != "" ? 
                        Convert.ToInt32(propertyContent) : book.Price;
                    break;

                case "YEAR":
                    book.ReleaseDate = DateTime.Parse("01/" + propertyContent);
                    break;

                case "PUBLISHER":
                    book.Publisher = propertyContent;
                    break;

                case "DESCRIPTION":
                    book.Description = propertyContent;
                    break;

                case "PAGES":
                    book.Pages = propertyContent != "" ? 
                        Convert.ToInt32(propertyContent) : book.Pages;
                    break;

                case "WEIGHT":
                    book.Weight = propertyContent != "" ? 
                        Convert.ToInt32(propertyContent) : book.Weight;
                    break;

                    case "ISBN":
                    book.Isbn = propertyContent;
                    break;

                case "RATING":
                    book.Rating = propertyContent != "" ?
                        Convert.ToDouble(propertyContent) : book.Rating;
                    break;

                case "EDITION":
                    book.Edition = propertyContent != "" ?
                        Convert.ToInt32(propertyContent) : book.Edition;  
                    break;

                case "BESTSELLER":
                    book.Bestseller = bool.Parse(propertyContent);
                    break;

                case "EXTRACT":
                    book.Extract = propertyContent;
                    break;

                case "GENRE":
                    Enum.TryParse(propertyContent, out Genre currentGenre);
                    book.Genre = currentGenre;
                    break;

                case "FORMAT":
                    Enum.TryParse(propertyContent, out Format currentFormat);
                    book.Format = currentFormat;
                    break;

                case "LANGUAGE":
                    Enum.TryParse(propertyContent, out Language currentLanguage);
                    book.Language = currentLanguage;
                    break;


                default:
                    break;
            }

        }
    }
}
