using System.ComponentModel.DataAnnotations;

namespace SkateAPI.Enities
{
    public class Message
    {
        [Key]
        public Guid RowKey { get; set; }
        public Guid SenderUserRowKey { get; set; }
        public Guid ReceiverUserRowKey { get; set; }
        public string? Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
