using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DataCollector.core.model;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace DataAnalyser.Util
{
    public class OutputGenerator : IOutputGenerator
    {
        private string _outName = "test1.pdf";
        private readonly PdfDocument _document = new PdfDocument();
        public  OutputGenerator Create(List<IntelItem> items,string subject)
        {
            ValidateTargetAvailable(_outName);

            var pageNewRenderer = _document.AddPage();

            var renderer = XGraphics.FromPdfPage(pageNewRenderer);

            items.ForEach((i) =>
            {
                renderer.DrawString(i.Content, new XFont("Arial", 12), XBrushes.Black, new XPoint(12, 12));
                
            });

            _outName = $"{subject}{DateTime.Now.Date}.pdf";
           // ValidateFileIsPDF(outName);

            return this;
        }

        public  OutputGenerator Save()
        {
            SaveDocument(_document, _outName);
            return this;
        }
        public  OutputGenerator Save(string path)
        {
            SaveDocument(_document, _outName);
            return this;
        }


        private static readonly string RootPath = Path.GetDirectoryName(typeof(OutputGenerator).GetTypeInfo().Assembly.Location);

        private const string OutputDirName = "Out";

        private void SaveDocument(PdfDocument document, string name)
        {
            var outFilePAth = Path.Combine(RootPath, name);
            var dir = Path.GetDirectoryName(outFilePAth);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            document.Save(outFilePAth);
        }

        private void ValidateFileIsPDF(string v)
        {
            var path = Path.Combine(RootPath, OutputDirName, v);
          
            var fi = new FileInfo(path);
            

            using (var stream = File.OpenRead(path))
            {
                ReadStreamAndVerifyPDFMagicNumber(stream);
            }
        }

        private static void ReadStreamAndVerifyPDFMagicNumber(Stream stream)
        {
            var readBuffer = new byte[5];
            // PDF must start with %PDF-
            var pdfsignature = new byte[5] { 0x25, 0x50, 0x44, 0x46, 0x2d };

            stream.Read(readBuffer, 0, readBuffer.Length);
         
        }

        private void ValidateTargetAvailable(string file)
        {
            var path = Path.Combine(RootPath, OutputDirName, file);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            
        }

      

      
        public void CreateTestPDFWithImage()
        {
            using (var stream = new MemoryStream())
            {
                var document = new PdfDocument();

                PdfPage pageNewRenderer = document.AddPage();

                var renderer = XGraphics.FromPdfPage(pageNewRenderer);

                renderer.DrawImage(XImage.FromFile(Path.Combine(RootPath, "Assets", "lenna.png")), new XPoint(0, 0));

                document.Save(stream);
                stream.Position = 0;
              ;
                ReadStreamAndVerifyPDFMagicNumber(stream);
            }
        }
    }
}