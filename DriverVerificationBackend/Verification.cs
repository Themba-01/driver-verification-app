using System;
using System.ComponentModel.DataAnnotations;

namespace DriverVerificationBackend.Models
{
    public class Verification
    {
        public int Id { get; set; }
        
        [Required]
        public required string Name { get; set; }
        
        [Required]
        public required string Surname { get; set; }
        
        [Required]
        public required string OrderNumber { get; set; }
        
        [Required]
        public required string VehicleRegistration { get; set; }
        
        [Required]
        public DateTime PickupTime { get; set; }
        
        public required string SelfieFilePath { get; set; } // Store the file path of the selfie
    }
}
