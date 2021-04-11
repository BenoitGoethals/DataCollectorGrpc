using DataCollector.DataLayer.mongo;

namespace DataAnalyser.Service
{
    public class PdfMsg
    {
        public PdfData Data { get; set; }
        public byte[] Content { get; set; }
    }
}