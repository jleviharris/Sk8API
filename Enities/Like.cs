using System.ComponentModel.DataAnnotations;

namespace SkateAPI.Enities
{
    public class Like
    {
        [Key]
        public Guid RowKey { get; set; }
        public Guid UserRowKey { get; set; }
        public Guid ParentRowKey { get; set; }
        public string? LikeParentTypeCd { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
