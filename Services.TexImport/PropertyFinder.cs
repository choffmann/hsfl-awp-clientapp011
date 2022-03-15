using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De.HsFlensburg.ClientApp011.Services.TexImport
{
    class PropertyFinder
    {
        private string currentLine;
        private string property;
        private string[] cathegories = {"TITLE", "AUTHOR", "PRICE", "YEAR", "PUBLISHER",
            "DESCRIPTION","PAGES", "WEIGHT", "ISBN", "isbn", "RATING", "EDITION", "BESTSELLER",
            "EXTRACT", "GENRE", "FORMAT", "LANGUAGE"};

        public string Property
        {
            get
            {
                for (int i = 0; i < cathegories.Length && property == null; i++)
                {
                    if (currentLine.Contains(cathegories[i]))
                    {
                        return cathegories[i].ToUpper();
                    }
                }
                return null;
            }
        }

        public string PropertyContent
        {
            get
            {
                if (Property != null)
                {
                    string[] splittPropertyContent = new string[8]
                    {$"{property}={{", $"{property} = {{", $"{property} =\"",
                    $"{property} = \"", $"{property} = ", "},", "\",", "}"};
                    string[] propertyContentArray = currentLine.Split(splittPropertyContent, StringSplitOptions.None);
                    return propertyContentArray[1];
                }
                return null;
            }
        }

        public PropertyFinder(string line)
        {
            currentLine = line;
        }
    }
}
