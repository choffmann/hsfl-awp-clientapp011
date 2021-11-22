using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace De.HsFlensburg.ClientApp011.Services.PrintService
{
    public class PrintBookService
    {
        public FlowDocument flowDocument;

        public PrintBookService()
        {
            flowDocument = new FlowDocument();
            flowDocument.MinPageHeight = 680.0;
            flowDocument.MaxPageHeight = 480.0;
        }

        public FlowDocument BookDocument()
        {
            Paragraph paragraph = new Paragraph();

            Run header = new Run("Übersicht");
            header.FontFamily = new System.Windows.Media.FontFamily("Arial");
            header.FontSize = 24;
            paragraph.Inlines.Add(header);
            flowDocument.Blocks.Add(paragraph);

            return flowDocument;
        }

        public void Printing()
        {
            PrintDialog printDialog = new PrintDialog();
            IDocumentPaginatorSource idpSource = flowDocument;

            Nullable<Boolean> print = printDialog.ShowDialog();
            if(print == true)
            {
                printDialog.PrintDocument(idpSource.DocumentPaginator, "BookPrint");
            }
        }
    }
}
