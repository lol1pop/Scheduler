using System;

namespace Scheduler.Entities {
    public class User {
        public const string AdminRole = "A";
        public const string PublisherRole = "E";
        public const string UserRole = "U";
        public string Version { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public long DomainId { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
        public DateTime LastRequestDateTime { get; set; } = new DateTime(1970, 1, 1);
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PollPeriod { get; set; } = "300";
        public DateTime RegistrationDateTime { get; set; } = DateTime.Now;
        public string Role { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string StartPage { get; set; } = string.Empty;
        public int LanguageId { get; set; }
    }
}