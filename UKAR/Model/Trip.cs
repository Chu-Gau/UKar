using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Geolocation;
using Newtonsoft.Json;

namespace UKAR.Model
{
    public class Trip
    {
        [Key]
        [JsonIgnore]
        public Int64 TripId { get; set; }
        public string DriverId { get; set; }
        public User Driver { get; set; }
        public string EmployerId { get; set; }
        public User Employer { get; set; }

        public string TripType { get; set; }

        public double LatitudeOrigin { get; set; }
        public double LongitudeOrigin { get; set; }
        public double? LatitudeDestination { get; set; }
        public double? LongitudeDestination { get; set; }

        public DateTime? StartTime  { get; set; }
        public DateTime? FinishTime { get; set; }
        
        public double? Distance { get; set; }
        public TimeSpan? TimeOffset { get; set; }

        public double Discount { get; set; }
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }

        public bool Accepted { get; set; }
        public bool Canceled { get; set; }
        public string RejectReason { get; set; }
    }
}
