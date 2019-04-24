using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace UKAR.Model
{
    public class UserLocation
    {
        /// <summary>
        /// Khoảng cách tối đa cho chuyến 1 chiều
        /// </summary>
        public const double MaxDistanceOneway = 30; //Km

        /// <summary>
        /// Khoảng cách tối đa khi tìm driver
        /// </summary>
        public const double MaxDistanceFindDriver = 1; //Km


        [Key]
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public DateTime LocatedTime { get; set; } = DateTime.Now;

        public double CoodinateRadius {
            get
            {
                if ((Latitude ?? Longitude) == null) return 0;
                return ConvertDistanceToLatLong(Latitude ?? 0, Longitude ?? 0, MaxDistanceFindDriver);
            }
        }

        public bool HasTrip { get; set; } = false;
        public bool OnTrip { get; set; } = false;

        /// <summary>
        /// Hàm tính xấp xỉ khoảng cách giữa 2 tọa độ địa lý, sử dụng tính nhanh
        /// </summary>
        /// <returns></returns>
        public static double ConvertCoordinateToKm(double lat1, double lon1, double lat2, double lon2)
        {
            var coord1 = new Geolocation.Coordinate() { Latitude = lat1, Longitude = lon1 };
            var coord2 = new Geolocation.Coordinate() { Latitude = lat2, Longitude = lon2 };

            return Geolocation.GeoCalculator.GetDistance(coord1, coord2, 1, Geolocation.DistanceUnit.Kilometers);
        }

        /// <summary>
        /// Hàm tính xấp xỉ delta tọa độ dựa trên khoảng cách
        /// </summary>
        /// <returns></returns>
        public static double ConvertDistanceToLatLong(double lat, double lon, double distance)
        {
            var coord = new Geolocation.Coordinate() { Latitude = lat, Longitude = lon};
            var boundaries = new Geolocation.CoordinateBoundaries(coord, distance, Geolocation.DistanceUnit.Kilometers);
            return Math.Max(boundaries.MaxLatitude, boundaries.MaxLongitude);
        }

    }
}
