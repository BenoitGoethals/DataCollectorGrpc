using System;
using System.Collections.Generic;

namespace DataAnalyser.Util
{
    public class IntelItemDateComparer : IComparer<DateTime>
    {
      
        int IComparer<DateTime>.Compare(DateTime x, DateTime y)
        {
           return DateTime.Compare((DateTime)x, (DateTime)y);
        }
    }
}