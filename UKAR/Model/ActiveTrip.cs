using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Geolocation;
using Newtonsoft.Json;

namespace UKAR.Model
{
    [Serializable]
    public class ActiveTrip
    {
        [JsonIgnore]
        public string DriverId { get; set; }
        [JsonIgnore]
        [ForeignKey("DriverId")]
        public User Driver { get; set; }
        [Key]
        [JsonIgnore]
        public string EmployerId { get; set; }
        [JsonIgnore]
        [ForeignKey("EmployerId")]
        public User Employer { get; set; }

        public string TripType { get; set; }

        public double LatitudeOrigin { get; set; }
        public double LongitudeOrigin { get; set; }
        public double? LatitudeDestination { get; set; }
        public double? LongitudeDestination { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? FinishTime { get; set; }

        public double? Distance { get; set; }
        public TimeSpan? TimeOffset { get; set; }

        public double Discount { get; set; }
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }

        public bool Accepted { get; set; }
        public bool Canceled { get; set; }
        public string RejectReason { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]//chỉ có tác dụng ngăn editor
        [JsonIgnore]
        public string DriverBlackListString { get; set; }
        [NotMapped]
        [JsonIgnore]
        public List<string> DriverBlackList {
            get
            {
                if (String.IsNullOrWhiteSpace(this.DriverBlackListString)) return new List<string>();
                return this.DriverBlackListString.Split('|').ToList();
            }
            set
            {
                DriverBlackListString = string.Join('|', value);
            }
        }
    }
}
