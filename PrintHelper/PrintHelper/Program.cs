using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;

namespace PrintHelper
{
    class Program
    {
        private const int LinesPerPage = 80;

        static void Main(string[] args)
        {
            if (args.Count() != 2)
                return;
            else
            {
                System.IO.StreamReader fileToPrint = new System.IO.StreamReader(args[1]);
                int counter = 0;
                int pageNumber = 1;
                string line = null;
                string[] page = null;
                string printerName = args[0];
                PrintHelper ph;

                do
                {
                    line = fileToPrint.ReadLine();
                    if (line == null)
                        break;
                    page[pageNumber - 1] += line;
                    page[pageNumber - 1] += "\n";
                    if (counter++ == LinesPerPage)
                        pageNumber++;
                } while (line != null);
                ph = new PrintHelper(page, printerName);
                ph.Dispose();
            }
        }
    }//end class program

    class PrintHelper: IDisposable
    {
        string txtToPrint = string.Empty;
        string[] pages = null;
        PrintDocument myPrinter = new System.Drawing.Printing.PrintDocument();

        public PrintHelper(string[] printMe, string printerName)
        {
            pages = printMe;
            myPrinter.PrinterSettings.PrinterName = printerName;

            myPrinter.PrintPage += new PrintPageEventHandler(myPrinter_PrintPage);
            myPrinter.Print();
        }

        private void myPrinter_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font printFont = new Font("Courier New", 10);

            int pageCount = pages.Count();
            int pageCurrent = 1;
            foreach (string s in pages)
            {
                txtToPrint = s;
                e.Graphics.DrawString(txtToPrint, printFont, Brushes.Black, 25, 25);
                if (pageCurrent < pageCount)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
        }

        public void Dispose()
        {
            myPrinter.Dispose();
        }

    }//end class printhelper
}