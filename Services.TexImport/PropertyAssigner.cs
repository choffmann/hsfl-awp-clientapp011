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
                    // string[] splittTitle = new string[5]
                    //{"TITLE={", "TITLE = {", "TITLE =\"", "TITLE = \"", "},"};
                    // string[] title = line.Split(splittTitle, StringSplitOptions.None); 
                    book.Title = propertyContent;
                    break;

                case "AUTHOR":
                    //string[] splittAuthor = new string[5]
                    //{"AUTHOR={", "AUTHOR = {", "AUTHOR =\"", "AUTHOR = \"", "},"}; // Fall: '",' und 'AUTHOR ="'
                    //string[] author = line.Split(splittAuthor, StringSplitOptions.None);
                    book.Author = propertyContent;
                    break;

                case "PRICE":
                    // string[] splittPrice = new string[4]
                    //{"PRICE={", "PRICE = {", "PRICE =", "},"};
                    // string[] price = line.Split(splittPrice, StringSplitOptions.None);
                    book.Price = Convert.ToInt32(propertyContent);
                    break;

                case "YEAR":
                    // string[] splittYear = new string[5]
                    //{"YEAR={", "YEAR = {", "YEAR = ", "}", ","};
                    // string[] year = line.Split(splittYear, StringSplitOptions.None);
                    book.ReleaseDate = DateTime.Parse("01/" + propertyContent);
                    break;

                case "PUBLISHER":
                    book.Publisher = propertyContent;
                    break;

                case "DESCRIPTION":
                    // string[] splittDesc = new string[4]
                    //{"DESCRIPTION={", "DESCRIPTION = {", "DESCRIPTION =", "},"};
                    // string[] description = line.Split(splittDesc, StringSplitOptions.None);
                    book.Description = propertyContent;
                    break;

                case "PAGES":
                    // string[] splittPages = new string[4]
                    //{"PAGES={", "PAGES = {", "PAGES =", "},"};
                    // string[] pages = line.Split(splittPages, StringSplitOptions.None);
                    book.Pages = Convert.ToInt32(propertyContent);
                    break;

                case "WEIGHT":
                    // string[] splittWeight = new string[4]
                    //{"WEIGHT={", "WEIGHT = {", "WEIGHT =", "},"};
                    // string[] weight = line.Split(splittWeight, StringSplitOptions.None);
                    book.Weight = Convert.ToInt32(propertyContent);
                    break;

                    case "ISBN":
                    //    string[] splittIsbn = new string[7]
                    //   {"ISBN={", "ISBN = {", "ISBN =", "isbn={", "isbn = {", "isbn =", "},"};
                    //    string[] isbn = line.Split(splittIsbn, StringSplitOptions.None);
                    book.Isbn = propertyContent;
                    break;

                case "RATING":
                    // string[] splittRating = new string[4]
                    //{"RATING={", "RATING = {", "RATING =", "},"};
                    // string[] rating = line.Split(splittRating, StringSplitOptions.None);
                    book.Rating = Convert.ToDouble(propertyContent);
                    break;

                case "EDITION":
                    // string[] splittEdition = new string[4]
                    //{"EDITION={", "EDITION = {", "EDITION =", "},"};
                    // string[] edition = line.Split(splittEdition, StringSplitOptions.None);
                    book.Edition = propertyContent != "" ?
                        Convert.ToInt32(propertyContent) : book.Edition;  // Null?
                    break;

                case "BESTSELLER":
                    // string[] splittSeller = new string[4]
                    //{"BESTSELLER={", "BESTSELLER = {", "BESTSELLER =", "},"};
                    // string[] seller = line.Split(splittSeller, StringSplitOptions.None);
                    book.Bestseller = bool.Parse(propertyContent);
                    break;

                case "EXTRACT":
                    // string[] splittExtract = new string[4]
                    //{"EXTRACT={", "EXTRACT = {", "EXTRACT =", "},"};
                    // string[] extract = line.Split(splittExtract, StringSplitOptions.None);
                    book.Extract = propertyContent;
                    break;

                case "GENRE":
                    //Type genres = typeof(Genre);
                    //book.Genre = Enum.Format(genres, Enum.Parse(genres, propertyContent), propertyContent);
                    //book.Genre = Genre.Parse(Genre, propertyContent);
                    //book.Genre = (Genre)2;
                    //book.Genre = propertyContent;
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
