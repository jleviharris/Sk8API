using System.ComponentModel.DataAnnotations;

namespace SkateAPI.Enities
{
    public class Notification
    {
        [Key]
        public Guid RowKey { get; set; }
        public Guid UserRowKey { get; set; }
        public string? Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
