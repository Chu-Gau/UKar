using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UKAR.Model
{
    public class Car
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public bool IsConfirmed { get; set; }
        public string CarImage { get; set; }
        public string CarImageFileType { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PlateNumber { get; set; }
        public string RegistrationImage { get; set; }
        public string RegistrationImageFileType { get; set; }
    }
}
