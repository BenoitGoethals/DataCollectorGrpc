using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataCollector.core.model;
using NLog;

namespace Datacollector.core.collectors
{
    public abstract class Extracter
    {
        protected readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public event EventHandler<IntelItem> AddedItem;
        public event EventHandler<List<IntelItem>> Completed;
        protected CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();


        protected readonly ConcurrentQueue<IntelItem> IntelItems = new ConcurrentQueue<IntelItem>();


        protected abstract  Task Execute();

        protected  void ProcessItemAdded(IntelItem intelItem)
        {
            IntelItems.Enqueue(intelItem);
            AddedItem?.Invoke(this, intelItem);
        }

        //public List<IntelItem> GetaIntelItems()
        //{
        //    var ret= IntelItems.ToList();
        //    IntelItems.Clear();
        //    return ret;
        //}

        protected void ProcessCompleted()
        {
            Completed?.Invoke(this, IntelItems.ToList());
            IntelItems.Clear();
        }
    }
}