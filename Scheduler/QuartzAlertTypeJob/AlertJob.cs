using System;
using System.Threading.Tasks;
using Quartz;

namespace Scheduler.QuartzAlertTypeJob {
    public class AlertJob : IJob {
        public async Task Execute(IJobExecutionContext context) {
            await Console.Out.WriteLineAsync("Greetings from AlertJob!");
        }
    }
}