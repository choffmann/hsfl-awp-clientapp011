using De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De.HsFlensburg.ClientApp011.Services.TexImport
{
    public class SplittPath

    {
        private string textToRender;

        public SplittPath(string fileText)
        {
            textToRender = fileText;
        }

        public void AddItemsToCollection(BookCollection collection)
        {
            string[] items = SplittByItems(textToRender);
            foreach (var item in items)
            {
                if (item.Contains("BOOK{"))
                {
                    Book newBook = new Book();
                    SplittTheBook(item, newBook);
                    collection.Add(newBook);
                }
            }
        }
        
        private string[] SplittByItems(string path)
        {
            string[] splittItem = new string[1] { "@" };     
            var devidedItems = path.Split(splittItem, StringSplitOptions.None);
            return devidedItems;
        }

        private void SplittTheBook(string stringItem, Book currentBook)
        {
            string[] splittLine = new string[3] { "\n\t", "\n", "" };
            var devidedLines = stringItem.Split(splittLine, StringSplitOptions.None);
            devidedLines[0] = "";
            foreach (var line in devidedLines)
            {
                if (line != "" && line != "}" && line != " ")
                {
                    PropertyFinder finder = new PropertyFinder(line);
                    string currentProperty = finder.Property;
                    string content = finder.PropertyContent;
                    PropertyAssigner assigner = new PropertyAssigner(currentProperty, content, currentBook);
                }
            }
        }
    }
}
