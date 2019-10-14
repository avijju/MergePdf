
using iTextSharp.text;
using iTextSharp.text.pdf;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Image = iTextSharp.text.Image;
using Rectangle = iTextSharp.text.Rectangle;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            String SecondPage = AppDomain.CurrentDomain.BaseDirectory+"TranscriptRequest.pdf";
            String FirstPage = AppDomain.CurrentDomain.BaseDirectory+"FirstPage.pdf";

            String Merged = AppDomain.CurrentDomain.BaseDirectory+"TranscriptMerged.pdf";


            Document document = new Document();
         
            using (FileStream newFileStream = new FileStream(Merged, FileMode.Create))
            {
              
                PdfCopy writer = new PdfCopy(document, newFileStream);
                if (writer == null)
                {
                    return;
                }

              
                document.Open();
                PdfReader reader = new PdfReader(FirstPage);

                reader.ConsolidateNamedDestinations();

               
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    writer.AddPage(page);
                }
                reader.Close();


                PdfReader readers = new PdfReader(SecondPage);
                PdfReader.unethicalreading = true;
                readers.ConsolidateNamedDestinations();

                // step 4: we add content
                for (int i = 1; i <= readers.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(readers, i);
                    writer.AddPage(page);
                }
                readers.Close();

                writer.Close();
                document.Close();




            }


        }

    }
}
