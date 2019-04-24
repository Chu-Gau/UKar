using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UKAR.Model
{
    public class DrivingLicense
    {
        [Key]
        public string DriverId { get; set; }
        [ForeignKey("DriverId")]
        public User Driver { get; set; }
        public string LicenseNumber { get; set; }
        public string Image { get; set; }
        public string ImageFileType { get; set; }
        public string ImageBack { get; set; }
        public string ImageBackFileType { get; set; }
    }
}
