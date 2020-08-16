using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ImageMagick;
using System.Reflection;
using Microsoft.VisualBasic;
using System.Windows.Input;
using System.Diagnostics;

namespace Crews.Utility.PDF2IMG
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Define operating directory.
            string appDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pdf2jpg";

            Debug.WriteLine(appDir);

            // Create appdata directory if it does not exist.
            Directory.CreateDirectory(appDir);

            // Copy files if they do not exist.
            WriteFileFromResource("PDF2IMG.gs.gsdll64.dll", appDir + @"\gsdll64.dll");
            WriteFileFromResource("PDF2IMG.gs.gswin64c.exe", appDir + @"\gswin64c.exe");

            // Point ImageMagick to ghostscript files.
            MagickNET.SetGhostscriptDirectory(appDir);
        }

        /// <summary>
        /// Convert the given PDF into a JPEG file.
        /// </summary>
        /// <param name="pdffile">Path to the PDF file.</param>
        public static int Convert(string pdffile)
        {
            // Get containing folder and file name of PDF.
            string currentDir = Path.GetDirectoryName(pdffile);
            string fileName = Path.GetFileName(pdffile);

            // Prepare settings.
            MagickReadSettings readSettings = new MagickReadSettings()
            {
                Density = new Density(300, 300)
            };

            // Prepare return variable.
            int pages;

            // Begin conversion.
            using (MagickImageCollection imageCollection = new MagickImageCollection())
            {
                // Create image collection.
                imageCollection.Read(pdffile, readSettings);

                // Initialize page count.
                int page = 1;

                // Set return value based on number of pages.
                pages = imageCollection.Count;

                // Iterate through pages, creating image files.
                foreach(MagickImage image in imageCollection)
                {
                    image.Format = MagickFormat.Jpg;
                    image.Write(currentDir + @"\" + (imageCollection.Count == 1 ? "" : @"\page" + page.ToString() + "-") +
                        fileName.Replace(".pdf", ".jpg"));

                    // Advance page counter.
                    page++;
                }
            }
            // Return page count.
            return pages;
        }

        private static void WriteFileFromResource(string resName, string destPath)
        {
            if (!File.Exists(destPath))
            {
                using (Stream resStream = Assembly.GetExecutingAssembly().
                    GetManifestResourceStream(resName))
                using (Stream fileStream = File.Create(destPath)) 
                {
                    resStream.CopyTo(fileStream);
                }
            }
        }
    }
}
