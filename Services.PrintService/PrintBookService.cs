using De.HsFlensburg.ClientApp011.Business.Model.BusinessObjects;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace De.HsFlensburg.ClientApp011.Services.PrintService
{
    public class PrintBookService
    {
        public FlowDocument flowDocument;

        public PrintBookService(BookCollection bookCollection)
        {
            flowDocument = new FlowDocument();
            flowDocument.PagePadding = new Thickness(15);
            flowDocument.ColumnGap = 0;
            BookDocument(bookCollection);
        }

        public FlowDocument BookDocument(BookCollection bookCollection)
        {
            // Define Fonts
            FontFamily arial = new FontFamily("Arial");
            FontFamily consolas = new FontFamily("Consolas");

            // Create header Title of List
            Run header = new Run("Print BookList of " + bookCollection.Count + " Books" );
            header.FontFamily = arial;
            header.FontSize = 24;
            flowDocument.Blocks.Add(new Paragraph(header));

            foreach (var book in bookCollection)
            {
                // Create 4x4 Table to show Book content
                Table contentTable = new Table();
                contentTable.Columns.Add(new TableColumn());
                contentTable.Columns.Add(new TableColumn());
                contentTable.Columns.Add(new TableColumn());
                contentTable.Columns.Add(new TableColumn());
                contentTable.Columns.Add(new TableColumn());
                contentTable.RowGroups.Add(new TableRowGroup());
                contentTable.RowGroups[0].Rows.Add(new TableRow());
                contentTable.RowGroups[0].Rows.Add(new TableRow());
                contentTable.RowGroups[0].Rows.Add(new TableRow());
                contentTable.RowGroups[0].Rows.Add(new TableRow());

                // First Column fill with Headers
                TableRow currentRow = contentTable.RowGroups[0].Rows[0];
                currentRow.FontFamily = arial;
                currentRow.FontWeight = FontWeights.Bold;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Title:"))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Author:"))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Genre:"))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Preis:"))));

                // Second Column fill with Content
                currentRow = contentTable.RowGroups[0].Rows[1];
                currentRow.FontFamily = consolas;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(book.Title))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(book.Author))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(book.Genre.ToString()))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(book.Price.ToString()))));

                // Third Column fill with new Header
                currentRow = contentTable.RowGroups[0].Rows[2];
                currentRow.FontFamily = arial;
                currentRow.FontWeight = FontWeights.Bold;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Kurzbeschreibung:"))));
                currentRow.Cells[0].ColumnSpan = 4;

                // Second Column fill with new Content
                currentRow = contentTable.RowGroups[0].Rows[3];
                currentRow.FontFamily = consolas;
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At"))));
                currentRow.Cells[0].ColumnSpan = 4;

                // Create new 1x2 Table to display Image and content table
                Table mainTable = new Table();
                TableColumn imageColumn = new TableColumn();
                mainTable.Columns.Add(imageColumn);
                mainTable.Columns.Add(new TableColumn());
                mainTable.RowGroups.Add(new TableRowGroup());
                mainTable.RowGroups[0].Rows.Add(new TableRow());

                // Insert Image in main table first row, first column 
                // Insert content table in first row, second column
                currentRow = mainTable.RowGroups[0].Rows[0];
                Image cover = new Image();
                cover.Source = ConvertModelToBitMapImage(book.Cover);
                cover.Width = 150;
                cover.Height = 150;
                currentRow.Cells.Add(new TableCell(new BlockUIContainer(cover)));
                currentRow.Cells.Add(new TableCell(contentTable));

                // Change Table Space / Width
                mainTable.Columns[0].Width = new GridLength(175);

                // Add border to main table
                mainTable.Padding = new Thickness(15);
                mainTable.BorderBrush = Brushes.Gray;
                mainTable.BorderThickness = new Thickness(2);

                flowDocument.Blocks.Add(mainTable);
            }
            return flowDocument;
        }

        // Convert Image to BitmapImage to print on FlowDocument
        private BitmapImage ConvertModelToBitMapImage(System.Drawing.Image cover)
        {
            MemoryStream localMemoryStream = new MemoryStream();
            cover.Save(localMemoryStream, System.Drawing.Imaging.ImageFormat.Png);
            localMemoryStream.Position = 0;
            BitmapImage localBitmapImage = new BitmapImage();
            localBitmapImage.BeginInit();
            localBitmapImage.StreamSource = localMemoryStream;
            localBitmapImage.EndInit();
            return localBitmapImage;
        }

        // Execute Printing
        public void Printing()
        {
            PrintDialog printDialog = new PrintDialog();
            IDocumentPaginatorSource idpSource = flowDocument;
            Nullable<Boolean> print = printDialog.ShowDialog();
            if (print == true)
            {
                // Set FlowDocument Width to Page Settings
                flowDocument.ColumnWidth = printDialog.PrintableAreaWidth;
                flowDocument.PageWidth = printDialog.PrintableAreaWidth;
                printDialog.PrintDocument(idpSource.DocumentPaginator, "BookPrint");
            }
        }
    }
}