using System.ComponentModel.DataAnnotations;

namespace SkateAPI.Enities
{
    public class Comment
    {
        [Key]
        public Guid RowKey { get; set; }
        public Guid UserRowKey { get; set; }
        public Guid ParentRowKey { get; set; }
        public string? CommentParentTypeCd { get; set; }
        public string? Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
