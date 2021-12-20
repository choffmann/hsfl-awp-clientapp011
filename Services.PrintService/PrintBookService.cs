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

            // Add Header to Document
            flowDocument.Blocks.Add(CreateHeader(bookCollection.Count));
            foreach (var book in bookCollection)
            {
                Table mainTable = CreateTable(1, 2);
                Table contentTable = CreateTable(4, 4);

                // First Column fill with Headers
                String[] headers = { "Title:", "Author:", "Genre:", "Preis:" };
                SetRowContent(contentTable.RowGroups[0].Rows[0], headers, arial, FontWeights.Bold);
                // Second Column fill with Content
                String[] content = { book.Title, book.Author, book.Genre.ToString(), book.Price.ToString() };
                SetRowContent(contentTable.RowGroups[0].Rows[1], content, consolas, FontWeights.Normal);
                // Third Column fill with new Header
                String[] descriptionHeader = { "Kurzbeschreibung"};
                TableRow currentRow = contentTable.RowGroups[0].Rows[2];
                SetRowContent(currentRow, descriptionHeader, arial, FontWeights.Bold);
                currentRow.Cells[0].ColumnSpan = 4;
                // Second Column fill with new Content
                String[] description = { book.Description };
                currentRow = contentTable.RowGroups[0].Rows[3];
                SetRowContent(currentRow, description, consolas, FontWeights.Normal);
                currentRow.Cells[0].ColumnSpan = 4;
                // Insert Image in main table first row, first column 
                SetRowImage(mainTable.RowGroups[0].Rows[0], book.Cover);
                // Insert content table in first row, second column
                SetRowToContentTable(mainTable.RowGroups[0].Rows[0], contentTable);
                // Change Table Space / Width
                mainTable.Columns[0].Width = new GridLength(175);
                // Add border to main table
                AddBorder(mainTable);

                flowDocument.Blocks.Add(mainTable);
            }
            return flowDocument;
        }

        private Paragraph CreateHeader(int bookCollectionSize)
        {
            Run header = new Run("Print BookList of " + bookCollectionSize + " Books");
            header.FontFamily = new FontFamily("Arial");
            header.FontSize = 24;
            return new Paragraph(header);
        }

        private Table CreateTable(int rows, int columns)
        {
            Table table = new Table();
            for(int i = 0; i < columns; i++)
            {
                table.Columns.Add(new TableColumn());
            }
            table.RowGroups.Add(new TableRowGroup());
            for (int i = 0; i < rows; i++)
            {
                table.RowGroups[0].Rows.Add(new TableRow());
            }
            return table;
        }

        private void SetRowContent(TableRow row, String[] contents, FontFamily fontFamily, FontWeight fontWeight)
        {
            row.FontFamily = fontFamily;
            row.FontWeight = fontWeight;
            foreach(string param in contents)
            {
                row.Cells.Add(new TableCell(new Paragraph(new Run(param))));
            }
        }

        private void SetRowImage(TableRow row, System.Drawing.Image image)
        {
            Image cover = new Image();
            cover.Source = ConvertModelToBitMapImage(image);
            cover.Width = 150;
            cover.Height = 150;
            row.Cells.Add(new TableCell(new BlockUIContainer(cover)));
        }

        private void SetRowToContentTable(TableRow row, Table contentTable)
        {
            row.Cells.Add(new TableCell(contentTable));
        }

        private void AddBorder(Table table)
        {
            table.Padding = new Thickness(15);
            table.BorderBrush = Brushes.Gray;
            table.BorderThickness = new Thickness(2);
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