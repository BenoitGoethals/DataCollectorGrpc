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
using Microsoft.Extensions.Logging;
using NLog;
using Path = System.IO.Path;


namespace DataAnalyser.Util
{
    public class OutputGenerator : IOutputGenerator
    {
        private byte[] _result;
        private string _outName;
        private  readonly string RootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "pdf");
        private  readonly ILogger<OutputGenerator> _logger;

        public OutputGenerator(ILogger<OutputGenerator> logger)
        {
            _logger = logger;
        }

        public OutputGenerator Create(List<IntelItem> items, string subject)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    var pdfWriter = new PdfWriter(memoryStream);
                    var pdfDocument = new PdfDocument(pdfWriter);
                    var document = new Document(pdfDocument, PageSize.LETTER, true);

                    items.ForEach((i) =>
                    {
                        var p = new Paragraph();
                        p.Add(new Text(!String.IsNullOrEmpty(i.Description) ? i.Description : "").SetBold());
                        p.Add("\n\n");
                        p.Add(new Text(!String.IsNullOrEmpty(i.Content) ? i.Content : ""));
                        p.Add("\n\n");
                        p.Add(new Text(!String.IsNullOrEmpty(i.Author) ? i.Author : ""));
                        p.Add("\n\n");
                        p.Add(new Text(!String.IsNullOrEmpty(i.Url) ? i.Url : ""));
                        p.Add("\n\n");
                        document.Add(p);

                    });
                    document.Close();

                    _result = memoryStream.ToArray();
                }
                _outName = $"{subject}{DateTime.Now.ToFileTimeUtc()}.pdf";
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
           

            return this;
        }

        public OutputGenerator Save()
        {
            try
            {
                if (_result != null)
                {
                    var outFilePAth = Path.Combine(RootPath,  _outName);
                    var dir = Path.GetDirectoryName(outFilePAth);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    File.WriteAllBytes(outFilePAth, _result);
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
          
            return this;
        }


    }
}