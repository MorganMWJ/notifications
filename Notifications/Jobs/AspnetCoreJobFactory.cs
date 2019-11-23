using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Simpl;
using Quartz.Spi;
using System;

namespace Notifications.Jobs
{
    public class AspnetCoreJobFactory : SimpleJobFactory
    {
        IServiceProvider _provider;

        public AspnetCoreJobFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public override IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            //try
            //{
            //    // this will inject dependencies that the job requires
            //    return (IJob)this._provider.GetService(bundle.JobDetail.JobType);
            //}
            //catch (Exception e)
            //{
            //    throw new SchedulerException(string.Format("Problem while instantiating job '{0}' from the Aspnet Core IOC.", bundle.JobDetail.Key, e));
            //}
            return _provider.GetRequiredService<QuartzJobRunner>();
        }
    }
}
