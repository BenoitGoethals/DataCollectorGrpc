using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Reflection;

using DataCollector.core.model;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Path = System.IO.Path;


namespace DataAnalyser.Util
{
    public class OutputGenerator : IOutputGenerator
    {
        private byte[] _result;
        private string _outName;
        private static readonly string RootPath = "C:/TEMP";

        public OutputGenerator Create(List<IntelItem> items, string subject)
        {
            using (var memoryStream = new MemoryStream())
            {
                var pdfWriter = new PdfWriter(memoryStream);
                var pdfDocument = new PdfDocument(pdfWriter);
                var document = new Document(pdfDocument, PageSize.LETTER, true);

                items.ForEach((i) =>
                {
                    var p = new Paragraph();
                    p.Add(i.Description);
                    p.Add(i.Content);
                   // p.Add(i.Author);
                    p.Add(i.Url);
                    document.Add(p);

                });
                document.Close();

                _result = memoryStream.ToArray();
            }
            _outName = $"{subject}{DateTime.Now.ToFileTimeUtc()}.pdf";

            return this;
        }

        public OutputGenerator Save()
        {
            if (_result != null)
            {
                var outFilePAth = Path.Combine(RootPath, "pdf", _outName);
                var dir = Path.GetDirectoryName(outFilePAth);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                File.WriteAllBytes(outFilePAth, _result);
            }
         
            return this;
        }

      


    




    }
}