using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace Scheduler {
    public class QuartzScheduler {

        private IScheduler _scheduler;
        private string _nameGroup;

        public QuartzScheduler(NameValueCollection props, string nameGroup) {
            StartScheduler(props).GetAwaiter();
            _nameGroup = nameGroup;
        }

        protected async Task StartScheduler(NameValueCollection props) {
            try {
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();
                _scheduler = scheduler;
                await scheduler.Start();
            }
            catch (SchedulerException se) { 
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }

        public async Task CreateSchedulerTrigger(IJobDetail job, string dataOfAlert = default) {
            Guid guid = Guid.NewGuid();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"{guid.ToString()}-${_nameGroup}Trigger", _nameGroup)
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();
            await _scheduler.ScheduleJob(job, trigger);
        }
        
        protected IScheduler GetScheduler() {
            return _scheduler;
        }
        
    }
}