using System;

namespace Scheduler.Entities {
    public class AlertEntity {
        public long CampaignId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; }
        public int ScheduleType { get; set; }
        public string Autoclose { get; set; }
        public int Schedule { get; set; }
        public int Terminated { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public bool IsScheduled => Schedule == 1;
        public DateTime? FromDate { get; set; } = null;
        public bool IsExpired => ToDate != null && ToDate < DateTime.Now;
        public DateTime? ToDate { get; set; } = null;
        public bool IsStarted => FromDate != null && FromDate < DateTime.Now;
        public bool IsActive => IsStarted && !IsExpired && !IsScheduled;
        public Recurrence Recurrence { get; set; }
        public int Class { get; set; }

        public int Ticker { get; set; }

        /*
         * B - broadcast content
         * R - contetn for recipients
         * I - content for Ip Groups
         */
        public string Type2 { get; set; }

        public AlertType Type {
            get {
                switch (Class) {
                    case 1:
                        return AlertType.SimpleAlert;
                    case 2:
                        return AlertType.ScreenSaver;
                    case 3:
                        return AlertType.Ticker;
                    case 4:
                        return AlertType.RssFeed;
                    case 5:
                        return AlertType.RssAlert;
                    case 6:
                        return AlertType.Rsvp;
                    case 8:
                        return AlertType.Wallpaper;
                    case 16:
                        return AlertType.EmergencyAlert;
                    case 32:
                        return AlertType.VideoAlert;
                    case 64:
                        return AlertType.SimpleSurvey;
                    case 128:
                        return AlertType.SurveyQuiz;
                    case 256:
                        return AlertType.SurveyPoll;
                    case 2048:
                        return AlertType.DigitalSignature;
                    default:
                        return AlertType.Undefined;
                }
            }
        }
    }

    public enum AlertType {
        Undefined = 0,
        SimpleAlert = 1,
        ScreenSaver = 2,
        Ticker = 3,
        RssFeed = 4,
        RssAlert = 5,
        Rsvp = 6,
        Wallpaper = 8,
        EmergencyAlert = 16,
        VideoAlert = 32,
        SimpleSurvey = 64,
        SurveyQuiz = 128,
        SurveyPoll = 256,
        DigitalSignature = 2048
    }
}