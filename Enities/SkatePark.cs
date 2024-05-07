using System.ComponentModel.DataAnnotations;

namespace SkateAPI.Enities
{
    public class SkatePark
    {
        [Key]
        public Guid RowKey { get; set; }
        public Guid AddressRowKey { get; set; }
        public string? Fenced { get; set; } 
        public string? Fee { get; set; }
        public string? FeeFrequency { get; set; }
        public string? MembershipRequired { get; set; }
        public string? WaiverRequired { get; set; }
        public string? Bowl { get; set; }
        public string? Halfpipe { get; set; }
        public string? Stairs { get; set; }
        public string? Rail { get; set; }
        public string? Box { get; set; }
        public string? Quarterpipe { get; set; }
        public string? NumBowls { get; set; }
        public string? NumHalfpipes { get; set; }
        public string? NumStairs { get; set; }
        public string? NumRails { get; set; }
        public string? NumBoxs { get; set; }
        public string? NumQuarterpipes { get; set; }
        public string? ParkingInd { get; set; }
        public string? ParkingFeeInd { get; set; }
        public decimal ParkingFee { get; set; }
        public string? ParkingFeeFrequency { get; set; }
        public string? PPEInd { get; set; }
        public string? RestrictedTypes { get; set; }
        public DateTime DateCreated { get; set; }
 
    }
}
