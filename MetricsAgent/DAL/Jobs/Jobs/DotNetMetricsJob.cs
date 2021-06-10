﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repository;
using Quartz;

namespace MetricsAgent.DAL.Jobs.Jobs
{
    public class DotNetMetricsJob : IJob
    {
        private readonly IDotNetMetricsRepository _repository;
        private readonly PerformanceCounter _dotNetCounter;

        public DotNetMetricsJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotNetCounter = new PerformanceCounter(".Net Memory Cache 4.0", "Cache Hits");
        }
        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_dotNetCounter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            _repository.Create(new DotNetMetrics { Time = time, Value = cpuUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
