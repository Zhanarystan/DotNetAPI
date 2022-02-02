using System;

namespace DotNetAPI.Models
{
    public class TroubleAlert
    {
        public Guid Id {get;set;}
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public AppUser Specialist { get; set; }
        public AppUser User { get; set; }
        public Status Status { get; set; }
    }
}