using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UKAR.Model
{
    public class ActiveDriver
    {
        [Key]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public string CurrentRole { get; set; }
        public string TripType { get; set; }

        public string UserLocationId { get; set; }
        [ForeignKey("UserLocationId")]
        public UserLocation UserLocation { get; set; }
    }
}
