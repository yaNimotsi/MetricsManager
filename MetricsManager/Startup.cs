using AutoMapper;

using FluentMigrator.Runner;
using MetricsManager.DAL.Jobs;
using MetricsManager.DAL.Jobs.HostedService;
using MetricsManager.DAL.Jobs.Schedule;
using MetricsManager.DAL.Repository;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Polly;

using Quartz;
using Quartz.Impl;
using Quartz.Spi;

using System;
using MetricsManager.Client;
using MetricsManager.Client.Interface;
using MetricsManager.DAL.Jobs.MetricJobs;

namespace MetricsManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<AgentInfo>();

            services.AddSingleton<IAgentsRepository, AgentsRepository>();
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(ConnToDB.ConnectionString)
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());

            var mapperConfiguration = new MapperConfiguration(mp =>
                mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));

            services.AddHostedService<QuartzHostedService>();

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(CpuMetricJob),
                cronExpression: "0/5 * * * * ?"));

            services.AddSingleton<DotNetMetricsJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(DotNetMetricsJob),
                cronExpression: "0/5 * * * * ?"));

            services.AddSingleton<HddMetricsJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(HddMetricsJob),
                cronExpression: "0/5 * * * * ?"));

            services.AddSingleton<NetworkMetricsJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(NetworkMetricsJob),
                cronExpression: "0/5 * * * * ?"));

            services.AddSingleton<RamMetricsJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(RamMetricsJob),
                cronExpression: "0/5 * * * * ?"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
