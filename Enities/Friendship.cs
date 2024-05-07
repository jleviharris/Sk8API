using System.ComponentModel.DataAnnotations;

namespace SkateAPI.Enities
{
    public class Friendship
    {
        [Key]
        public Guid RowKey { get; set; }
        public Guid RequestingUser { get; set; }
        public Guid AcceptingUser { get; set; }
        public string? FriendsInd { get; set; }
        public DateTime SentDt { get; set; }
        public DateTime AcceptedDt { get; set; }
        public DateTime DeniedDt { get; set; }

    }
}
