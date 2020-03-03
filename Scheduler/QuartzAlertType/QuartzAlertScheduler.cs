using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz;
using Scheduler.QuartzAlertTypeJob;

namespace Scheduler.QuartzAlertType {
    internal class QuartzAlertScheduler : QuartzScheduler {
        
        private const string NameGroup = "Alert";
        
        private static readonly NameValueCollection Props = new NameValueCollection {
            {"quartz.serializer.type", "binary"}
        };

        public QuartzAlertScheduler(string data = default)
            : base(Props, NameGroup) {
        }

        public async Task AddNewRepitSchedulerJod(string dataOfAlert = default) {
            Guid guid = Guid.NewGuid();
            IJobDetail job = JobBuilder.Create<AlertJob>()
                .WithIdentity($"{guid.ToString()}-AlertJob", NameGroup)
                .Build();
            await CreateSchedulerTrigger(job);
        }
    }
}