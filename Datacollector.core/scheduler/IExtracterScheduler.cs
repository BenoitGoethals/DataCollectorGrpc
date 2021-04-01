using System.Collections.Generic;
using Datacollector.core.collectors;
using FluentScheduler;

namespace Datacollector.core.scheduler
{
    public interface IExtracterScheduler
    {
        /// <summary>
        /// Run IExtracter every x minutes
        /// </summary>
        /// <param name="extracter"></param>
        /// <param name="periodicTime"></param>
        void Add(IExtracter extracter,int periodicTime);

        IList<Schedule> Schedules();
        IList<Schedule> RunningSchedules();
        void StopAll();
    }
}