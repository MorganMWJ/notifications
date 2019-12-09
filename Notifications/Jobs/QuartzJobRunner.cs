using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Notifications.Jobs
{
    /**
     * Class taken from blog post: https://andrewlock.net/using-scoped-services-inside-a-quartz-net-hosted-service-with-asp-net-core/
     * The class allows Scoped Services (dbcontext) to be used in Quartz Scheduled Jobs
     */
    public class QuartzJobRunner : IJob
    {
        private readonly IServiceProvider _serviceProvider;
        public QuartzJobRunner(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var jobType = context.JobDetail.JobType;
                var job = scope.ServiceProvider.GetRequiredService(jobType) as IJob;

                 await job.Execute(context);
            }
        }

        //// FIXME - added for Quarty 3.0.7
        //Task IJob.Execute(IJobExecutionContext context)
        //{
        //    //throw new NotImplementedException();
        //}
    }
}
