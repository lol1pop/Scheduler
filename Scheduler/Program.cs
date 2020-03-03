using System;
using System.Threading.Tasks;
using Scheduler.QuartzAlertType;

namespace Scheduler {
    internal static class Program {
        public static async Task Main(string[] args) {
            var alert = new QuartzAlertScheduler();
            await alert.AddNewRepitSchedulerJod();
            await Task.Delay(TimeSpan.FromSeconds(5));
            await new QuartzAlertScheduler().AddNewRepitSchedulerJod();
            
            // // some sleep to show what's happening
            await Task.Delay(TimeSpan.FromSeconds(60));
        }
    }
}