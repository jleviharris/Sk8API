using System.ComponentModel.DataAnnotations;

namespace SkateAPI.Enities
{
    public class Post
    {
        [Key]
        public Guid RowKey { get; set; }
        public Guid UserRowKey { get; set; }
        public string? Content { get; set; }
        public string? PostTypeCd { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
