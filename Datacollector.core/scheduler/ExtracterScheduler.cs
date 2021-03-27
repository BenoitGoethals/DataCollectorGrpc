using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using Datacollector.core.collectors;
using FluentScheduler;
using Microsoft.Extensions.Logging;
using NLog;

namespace Datacollector.core.scheduler
{
    public class ExtracterScheduler:IDisposable, IExtracterScheduler
    {
        public ExtracterScheduler(ILogger<ExtracterScheduler> logger)
        {
            var logger1 = logger;
            JobManager.Initialize();
            JobManager.JobStart += info => logger1.LogInformation($"{info.Name}: started");
            JobManager.JobEnd += info => logger1.LogInformation($"{info.Name}: ended ({info.Duration})");
            JobManager.JobException += info => logger1.LogInformation($"{info.Name}: ended ({info.Exception.StackTrace})");

        }
        /// <summary>
        /// Run IExtracter every x minutes
        /// </summary>
        /// <param name="extracter"></param>
        /// <param name="periodicTime"></param>
        public void Add(IExtracter extracter,int periodicTime)
        {
            JobManager.AddJob(extracter, s=>s.ToRunNow().AndEvery(periodicTime).Minutes());
        }



        public IList<Schedule> Schedules()
        {
           return JobManager.AllSchedules.ToImmutableList();
        }


        public IList<Schedule> RunningSchedules()
        {
            return JobManager.RunningSchedules.ToImmutableList();
        }

        public void StopAll()
        {
            
            JobManager.StopAndBlock();
            JobManager.RemoveAllJobs();
        }

        public void Dispose()
        {
            StopAll();
        }
    }
}