using De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace De.HsFlensburg.ClientApp011.Services.PrintService
{
    public class PrintBookService
    {
        public FlowDocument flowDocument;

        public PrintBookService(BookCollection bookCollection)
        {
            flowDocument = new FlowDocument();
            flowDocument.MinPageHeight = 680.0;
            flowDocument.MaxPageHeight = 480.0;
            BookDocument(bookCollection);
        }

        public FlowDocument BookDocument(BookCollection bookCollection)
        {
            Paragraph paragraph = new Paragraph();

            FontFamily arial = new FontFamily("Arial");
            FontFamily consolas = new FontFamily("Consolas");

            Run header = new Run("Übersicht");
            header.FontFamily = new FontFamily("Arial");
            header.FontSize = 24;
            paragraph.Inlines.Add(header);
            flowDocument.Blocks.Add(paragraph);

            foreach (var book in bookCollection)
            {
                paragraph = new Paragraph();
                flowDocument.PagePadding = new System.Windows.Thickness(40);

                Run bookHeader = new Run("Book:");
                bookHeader.FontFamily = new FontFamily("Times");
                bookHeader.FontSize = 16;
                bookHeader.FontWeight = FontWeights.Bold;
                paragraph.Inlines.Add(bookHeader);


                paragraph.Inlines.Add(AddLine("Name", book.Title, consolas, 10));
                paragraph.Inlines.Add(AddLine("Author", book.Author, consolas, 10));

                paragraph.Padding = new Thickness(15);
                paragraph.BorderBrush = Brushes.Red;
                paragraph.BorderThickness = new Thickness(6);

                flowDocument.Blocks.Add(paragraph);
            }

            return flowDocument;
        }

        private Run AddLine(String title, object value, FontFamily font, int size)
        {
            Run line = new Run("\n" + title + ": " + value);
            line.FontFamily = font;
            line.FontSize = size;
            return line;
        }

        public void Printing()
        {
            PrintDialog printDialog = new PrintDialog();
            IDocumentPaginatorSource idpSource = flowDocument;

            Nullable<Boolean> print = printDialog.ShowDialog();
            if (print == true)
            {
                printDialog.PrintDocument(idpSource.DocumentPaginator, "BookPrint");
            }
        }
    }
}
