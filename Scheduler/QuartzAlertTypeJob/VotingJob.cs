using System;
using System.Threading.Tasks;
using Quartz;

namespace Scheduler.QuartzAlertTypeJob {
    public class VotingJob : IJob {
        public async Task Execute(IJobExecutionContext context) {
            await Console.Out.WriteLineAsync("Greetings from VotingJob!");
        }
    }
}