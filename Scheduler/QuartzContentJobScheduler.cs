using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Scheduler.Entities;
using Scheduler.QuartzAlertTypeJob;
using Scheduler.Utilities;

namespace Scheduler {
    public class QuartzContentJobScheduler : IContentJobScheduler {

        public QuartzContentJobScheduler() {
        }

        public async Task<bool> CreateJob(ContentJobDto contentJobDto) {
            if (contentJobDto == null) {
                throw new ArgumentNullException();
            }

            try {
                switch (contentJobDto.ContentType) {
                    case AlertType.Wallpaper:
                        await CreatedSchedulerWallpapers(contentJobDto);
                        break;
                    case AlertType.ScreenSaver:
                        await CreatedSchedulerScreensaver(contentJobDto);
                        break;
                    default:
                        throw new ArgumentException();
                }
                return true;
            }
            catch {
                return false;
            }
        }

        private async Task CreatedSchedulerWallpapers(ContentJobDto contentJobDto) {
            var wpTimeZones = new int[]{1,2,3,4};//_wallpapersContentDeliveringService.GetAllTimeZones();
            foreach (var timezone in wpTimeZones) {
                var job = JobBuilder.Create<AlertJob>()
                    .UsingJobData("contentId", contentJobDto.ContentId)
                    .UsingJobData("timeZone", timezone)
                    .Build();
                job.JobDataMap["wallpapersContentDeliveringService"] = wpTimeZones;//_wallpapersContentDeliveringService;
                await CreatedScheduler(job, timezone, contentJobDto);
            }
        }

        private async Task CreatedSchedulerScreensaver(ContentJobDto contentJobDto) {
            var ssTimeZones = new int[]{1,2,3,4}; //_screensaverContentDeliveringService.GetAllTimeZones();
            foreach (var timezone in ssTimeZones) {
                var job = JobBuilder.Create<AlertJob>()
                    .UsingJobData("contentId", contentJobDto.ContentId)
                    .UsingJobData("timeZone", timezone)
                    .Build();
                job.JobDataMap["screensaverContentDeliveringService"] = ssTimeZones;//_screensaverContentDeliveringService;
                await CreatedScheduler(job, timezone, contentJobDto);
            }
        }

        private async Task CreatedScheduler(IJobDetail job,
                                            int timezone,
                                            ContentJobDto contentJobDto
        ) {
            var startDateTime = contentJobDto.StartDateTime.ToUniversalTime();
            var endDateTime = contentJobDto.EndDateTime.ToUniversalTime();
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            var actualStartDateTime =
                DateTimeUtils.GetServerDateTimeByTimeZoneAndLocalDateTime(timezone, startDateTime);
            var actualEndDateTime = DateTimeUtils.GetServerDateTimeByTimeZoneAndLocalDateTime(timezone, endDateTime);
            var trigger = TriggerBuilder.Create()
                .StartAt(actualStartDateTime)
                .EndAt(actualEndDateTime)
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        private async Task CreatedCronScheduler(IJobDetail job, string cronSchedule, int timezone) {
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            var trigger = TriggerBuilder.Create()
                .WithCronSchedule(cronSchedule,
                    inTimeZone => inTimeZone.InTimeZone(
                        TimeZoneInfo.FindSystemTimeZoneById(Convert.ToString(timezone))))
                .ForJob(job)
                .Build();
            await scheduler.ScheduleJob(trigger);
        }

        private async Task CreatedCronScheduler(IJobDetail job,
                                                string cronSchedule,
                                                ContentJobDto contentJobDto) {
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            var trigger = TriggerBuilder.Create()
                .WithCronSchedule(cronSchedule)
                .ForJob(job)
                .Build();
            await scheduler.ScheduleJob(trigger);
        }

        public bool DeleteJob(ContentJobDto contentJobDto) {
            return true;
        }

        public IEnumerable<ContentJobDto> GetAllJobs() {
            throw new NotImplementedException();
        }
    }
}