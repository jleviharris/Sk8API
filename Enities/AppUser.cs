using System;
using System.ComponentModel.DataAnnotations;

namespace SkateAPI.Enities
{
    public class AppUser
    {
        [Key]
        public Guid RowKey { get; set; }
        public  string? Username { get; set; }
        public string? UserType { get; set; }
        public DateTime? DisableDate { get; set; }
        public int LoginFailCount { get; set; }
        public DateTime? LoginLastFailTime { get; set; }
        public DateTime? PasswordLastChangedDt { get; set; }
        public string? Salt { get; set; }
        public string? PasswordHash { get; set; }
        public string? PrevPasswordHash1 { get; set; }
        public string? PrevPasswordHash2 { get; set; }
        public string? PrevPasswordHash3 { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? LockedReason { get; set; }
        public int? SecurityLevel { get; set; }
        public string? MobilePasswordHash { get; set; }
        public string? MobilePrevPasswordHash1 { get; set; }
        public string? MobilePrevPasswordHash2 { get; set; }
        public string? MobilePrevPasswordHash3 { get; set; }
        public string? OnlineInd { get; set; }
        public string? SkatingInd { get; set; }
        public string? Notifications { get; set; }
    }
}
