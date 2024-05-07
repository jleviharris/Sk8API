using System.ComponentModel.DataAnnotations;

namespace SkateAPI.Enities
{
    public class Address
    {
        [Key]
        public Guid RowKey { get; set; }
        public Guid ParentRowKey { get; set; }
        public string? ParentTypeCd { get; set; }
        public string? StreetNumber { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }

    }
}
