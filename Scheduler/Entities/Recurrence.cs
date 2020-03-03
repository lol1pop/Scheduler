namespace Scheduler.Entities {
    public class Recurrence {
        public long AlertId { get; set; }
        public string Pattern { get; set; }
        public int NumberDays { get; set; }
        public int NumberMonth { get; set; }
        public int WeekDays { get; set; }
    }
}