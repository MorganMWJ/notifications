using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Data;
using Notifications.Email;
using Notifications.Jobs;
using Notifications.Services;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

namespace Notifications
{
    public class Startup
    {
        private IScheduler _quartzScheduler;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _quartzScheduler = ConfigureQuartz();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /* MVC Service */
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            /* SMTP Email Service */
            services.AddSingleton<IEmailService, EmailService>();

            /* HTTP Client Service */
            /*services.AddHttpClient("messageStoreClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44360/"); //CHANGE THIS
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            */

            /* PostgreSQL Database Service */
            services.AddDbContext<NotificationsContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("NotificationsContext")));

            /* Quartz Services */
            services.AddSingleton<QuartzJobRunner>();
            services.AddTransient<HourlyMailJob>();
            services.AddTransient<DailyMailJob>();
            services.AddSingleton(provider => _quartzScheduler);
        }

        private void OnShutdown()
        {
            if (!_quartzScheduler.IsShutdown)
            {
                _quartzScheduler.Shutdown();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            /* Use custom factory so I can use dependency injection in scheduled tasks */
            _quartzScheduler.JobFactory = new AspnetCoreJobFactory(app.ApplicationServices);

            app.UseHttpsRedirection();
            app.UseMvc();

            /* Set up running the two background tasks */
            StartEmailJobs();

        }

        public IScheduler ConfigureQuartz()
        {
            NameValueCollection props = new NameValueCollection
            {
                {"quartz.scheduler.instanceName","MyScheduler"},
                {"quartz.threadPool.threadCount","3"},
                {"quartz.jobStore.type","Quartz.Simpl.RAMJobStore, Quartz"}
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            var scheduler = factory.GetScheduler();
            scheduler.Start();
            return scheduler;
        }

        private void StartEmailJobs()
        {
            /* STEP 2: We create HourlyMailJob & DailyMailJob instances using Quartz JobBuilder. */
            IJobDetail dailyJob = JobBuilder.Create<DailyMailJob>().Build();
            IJobDetail hourlyJob = JobBuilder.Create<HourlyMailJob>().Build();

            /* STEP 3: We Create a Trigger and configure it to fire once everyday. */
            ITrigger dailyTrigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                    s.WithIntervalInSeconds(15) //DEBUG TIME CHANGE THIS
                    .OnEveryDay()
                    //.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                   )
                .Build();

            /* STEP 4: We Create a Trigger and configure it to fire once every hour. */
            ITrigger hourlyTrigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                    s.WithIntervalInSeconds(5) //DEBUG TIME CHANGE THIS
                    .OnEveryDay()
                   )
                .Build();

            /* STEP 5: Finally we schedule our Jobs in the scheduler using the trigger we created above. */
            _quartzScheduler.ScheduleJob(dailyJob, dailyTrigger);
            _quartzScheduler.ScheduleJob(hourlyJob, hourlyTrigger);
        }
    }
}
