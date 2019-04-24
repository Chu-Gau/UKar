using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UKAR.Model;

namespace UKAR.BL
{
    public class BLTrip : BLBase
    {
        private BLUser bLUser;

        private User currentUser;


        public BLTrip(
            IConfiguration configuration,
            UKarDBContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            BLUser bLUser
            ) : base(configuration, dbContext, httpContextAccessor)
        {
            this.bLUser = bLUser;
        }

        public User CurrentUser
        {
            get
            {
                this.currentUser = this.currentUser ?? bLUser.CurrentUser;
                return currentUser;
            }
            set => currentUser = value;
        }

        public void WaitForTrip(string tripTypeString)
        {
            Enum.Trip tripType = System.Enum.Parse<Enum.Trip>(tripTypeString);
            var activeDriver = new ActiveDriver()
            {
                User = CurrentUser,
                TripType = tripType.ToString(),
                CurrentRole = Enum.Role.Driver.ToString()
            };

            var userLocation = DbContext.UserLocations.Where(u => u.UserId == CurrentUser.Id).FirstOrDefault();

            if (userLocation == null)
            {
                userLocation = new UserLocation() { User = CurrentUser };
                DbContext.UserLocations.Add(userLocation);
            }
            activeDriver.UserLocation = userLocation;
            var alreadyActive = DbContext.ActiveDriver.Where(d => d.UserId == CurrentUser.Id).FirstOrDefault();
            if (alreadyActive != null) DbContext.ActiveDriver.Remove(alreadyActive);
            DbContext.Add<ActiveDriver>(activeDriver);
            DbContext.SaveChanges();

        }

        public ActiveTrip FindDriver(ActiveTrip newTrip)
        {
            //Kiểm tra tripType hợp lệ hay không trước khi gán
            Enum.Trip tripType = System.Enum.Parse<Enum.Trip>(newTrip.TripType);
            if (tripType == Enum.Trip.Both) throw new Exception();

            var currentUserLocation = DbContext.UserLocations.Where(l => l.UserId == CurrentUser.Id).FirstOrDefault();

            newTrip.Employer = CurrentUser;

            if (tripType == Enum.Trip.Oneway && (newTrip.LatitudeDestination == null || newTrip.LatitudeDestination == null)) throw new Exception();
            newTrip.LatitudeOrigin = currentUserLocation.Latitude ?? 0;
            newTrip.LongitudeOrigin = currentUserLocation.Longitude ?? 0;

            newTrip.TotalAmount = 100;
            newTrip.Accepted = false;

            var radius = UserLocation.MaxDistanceFindDriver;

            var driver = DbContext.ActiveDriver.Include(d => d.UserLocation).Where(d =>
                (
                    !DbContext.ActiveTrips.Select(t => t.DriverId).Contains(d.UserId)
                )
                &&
                (
                    d.UserLocation.Latitude <= (currentUserLocation.Latitude ?? 0) + radius
                    && d.UserLocation.Latitude >= (currentUserLocation.Latitude ?? 0) - radius
                    && d.UserLocation.Longitude <= (currentUserLocation.Longitude ?? 0) + radius
                    && d.UserLocation.Longitude >= (currentUserLocation.Longitude ?? 0) - radius
                )
                && (System.Enum.Parse<Enum.Trip>(d.TripType) == tripType || System.Enum.Parse<Enum.Trip>(d.TripType) == Enum.Trip.Both)
            ).Include(d => d.User).FirstOrDefault();

            if (driver == null)
            {
                newTrip.RejectReason = "Driver Not Available";
            }
            else
            {
                newTrip.Driver = driver.User;

                var driverFlagLocation = DbContext.UserLocations.Where(l => l.UserId == driver.UserId).FirstOrDefault();
                driverFlagLocation.HasTrip = true;

                var flagLocation = DbContext.UserLocations.Where(l => l.UserId == CurrentUser.Id).FirstOrDefault();
                flagLocation.HasTrip = true;
                DbContext.UserLocations.Update(driverFlagLocation);
                DbContext.UserLocations.Update(flagLocation);

                DbContext.ActiveTrips.Add(newTrip);
                DbContext.SaveChanges();
            }
            return newTrip;
        }

        public void CancelWait()
        {
            ActiveDriver driver = DbContext.ActiveDriver.Where(d => d.UserId == CurrentUser.Id).FirstOrDefault();
            DbContext.ActiveDriver.Remove(driver);

            var flagLocation = DbContext.UserLocations.Where(l => l.UserId == CurrentUser.Id).FirstOrDefault();
            flagLocation.OnTrip = false;
            flagLocation.HasTrip = false;
            DbContext.UserLocations.Update(flagLocation);

            DbContext.SaveChanges();
        }

        public void CancelFinding()
        {
            ActiveTrip trip = DbContext.ActiveTrips.Where(t => t.EmployerId == CurrentUser.Id).FirstOrDefault();
            if (trip.Accepted) throw new Exception();
            trip.Canceled = true;
            DbContext.ActiveTrips.Remove(trip);

            var flagDriverLocation = DbContext.UserLocations.Where(l => l.UserId == trip.DriverId).FirstOrDefault();
            flagDriverLocation.HasTrip = false;
            DbContext.UserLocations.Update(flagDriverLocation);

            DbContext.SaveChanges();
        }

        public void Reject()
        {
            ActiveTrip trip = DbContext.ActiveTrips.Where(t => t.DriverId == CurrentUser.Id).FirstOrDefault();
            var radius = UserLocation.MaxDistanceFindDriver;

            trip.DriverBlackList.Add(trip.DriverId);

            var driver = DbContext.ActiveDriver.Include(d => d.UserLocation).Where(d =>
                (
                    d.UserLocation.Latitude <= trip.LatitudeOrigin + radius
                    && d.UserLocation.Latitude >= trip.LatitudeOrigin - radius
                    && d.UserLocation.Longitude <= trip.LatitudeOrigin + radius
                    && d.UserLocation.Longitude >= trip.LatitudeOrigin - radius
                )
                && (System.Enum.Parse<Enum.Trip>(d.TripType).ToString() == trip.TripType || System.Enum.Parse<Enum.Trip>(d.TripType) == Enum.Trip.Both)
                && !trip.DriverBlackList.Contains(d.UserId)
            ).Include(d => d.User).FirstOrDefault();

            if (driver == null)
            {
                trip.RejectReason = "Driver Not Available";
                var flagEmployerLocation = DbContext.UserLocations.Where(l => l.UserId == trip.EmployerId).FirstOrDefault();
                flagEmployerLocation.HasTrip = true;
                DbContext.UserLocations.Update(flagEmployerLocation);
            }
            else
            {
                var flagDriverLocation = DbContext.UserLocations.Where(l => l.UserId == driver.UserId).FirstOrDefault();
                flagDriverLocation.HasTrip = true;
                DbContext.UserLocations.Update(flagDriverLocation);

                trip.Driver = driver.User;
                trip.Accepted = false;
            }
            DbContext.ActiveTrips.Update(trip);

            var flagLocation = DbContext.UserLocations.Where(l => l.UserId == CurrentUser.Id).FirstOrDefault();
            flagLocation.HasTrip = false;
            DbContext.UserLocations.Update(flagLocation);

            DbContext.SaveChanges();

        }

        public User Accept()
        {
            ActiveTrip trip = DbContext.ActiveTrips.Where(t => t.DriverId == CurrentUser.Id).Include(t => t.Employer).FirstOrDefault();

            trip.Accepted = true;
            if (trip.TripType == Enum.Trip.Round.ToString()) trip.StartTime = DateTime.Now;

            DbContext.ActiveTrips.Update(trip);

            var flagLocation = DbContext.UserLocations.Where(l => l.UserId == trip.EmployerId).FirstOrDefault();
            flagLocation.OnTrip = true;
            DbContext.UserLocations.Update(flagLocation);

            var flagLocationMe = DbContext.UserLocations.Where(l => l.UserId == currentUser.Id).FirstOrDefault();
            flagLocationMe.HasTrip = false;
            flagLocationMe.OnTrip = true;
            DbContext.UserLocations.Update(flagLocationMe);

            DbContext.SaveChanges();

            var activeMe = DbContext.ActiveDriver.Where(d => d.UserId == trip.DriverId).FirstOrDefault();

            if (activeMe != null)
            {
                DbContext.Remove<ActiveDriver>(activeMe);
                DbContext.SaveChanges();
            }

            return trip.Employer;
        }

        private void CalculateAmount(ActiveTrip activeTrip)
        {
            if (activeTrip.TripType == Enum.Trip.Oneway.ToString())
            {

            }
            else if (activeTrip.TripType == Enum.Trip.Round.ToString())
            {
                var timeOffset = activeTrip.TimeOffset ?? new TimeSpan();
                activeTrip.TotalAmount = (decimal)timeOffset.Hours * 20000 - (decimal)activeTrip.Discount;
            }
        }

        public ActiveTrip FinishTrip()
        {
            ActiveTrip trip = DbContext.ActiveTrips.Where(t => t.DriverId == CurrentUser.Id).FirstOrDefault();
            //todo: tính tiền cẩn thận
            if (trip.TripType == Enum.Trip.Round.ToString())
            {
                trip.FinishTime = DateTime.Now;
                trip.TimeOffset = trip.FinishTime - trip.StartTime;
                CalculateAmount(trip);
            }
            SaveTrip(trip);

            var flagLocation = DbContext.UserLocations.Where(l => l.UserId == CurrentUser.Id).FirstOrDefault();
            flagLocation.OnTrip = false;
            flagLocation.HasTrip = false;
            DbContext.UserLocations.Update(flagLocation);

            var flagEmployerLocation = DbContext.UserLocations.Where(l => l.UserId == trip.EmployerId).FirstOrDefault();
            flagEmployerLocation.OnTrip = false;
            flagEmployerLocation.HasTrip = false;
            DbContext.UserLocations.Update(flagLocation);

            DbContext.SaveChanges();

            return trip;
        }

        public void CancelTrip()
        {
            ActiveTrip trip = DbContext.ActiveTrips.Where(t => t.DriverId == CurrentUser.Id || t.EmployerId == CurrentUser.Id).FirstOrDefault();
            trip.Canceled = true;
            DbContext.ActiveTrips.Remove(trip);
            SaveTrip(trip);

            var flagLocation = DbContext.UserLocations.Where(l => l.UserId == CurrentUser.Id).FirstOrDefault();
            flagLocation.OnTrip = false;
            flagLocation.HasTrip = false;
            DbContext.UserLocations.Update(flagLocation);

            var flagEmployerLocation = DbContext.UserLocations.Where(l => l.UserId == trip.EmployerId).FirstOrDefault();
            flagEmployerLocation.OnTrip = false;
            flagEmployerLocation.HasTrip = false;
            DbContext.UserLocations.Update(flagLocation);

            DbContext.SaveChanges();
        }

        public void SaveTrip(ActiveTrip activeTrip)
        {
            Trip trip = new Trip()
            {
                TripType = activeTrip.TripType,
                Accepted = activeTrip.Accepted,
                RejectReason = activeTrip.RejectReason,
                DriverId = activeTrip.DriverId,
                EmployerId = activeTrip.EmployerId,
                Canceled = activeTrip.Canceled,
                LatitudeOrigin = activeTrip.LatitudeOrigin,
                LongitudeOrigin = activeTrip.LongitudeOrigin,
                LatitudeDestination = activeTrip.LatitudeDestination,
                LongitudeDestination = activeTrip.LongitudeDestination,
                Distance = activeTrip.Distance,
                StartTime = activeTrip.StartTime,
                FinishTime = activeTrip.FinishTime,
                TimeOffset = activeTrip.TimeOffset,
                Discount = activeTrip.Discount,
                TotalAmount = activeTrip.TotalAmount
            };
            DbContext.Trips.Add(trip);
            DbContext.ActiveTrips.Remove(activeTrip);
            DbContext.SaveChanges();
        }

        public UserLocation GetPartnerLocation()
        {
            ActiveTrip trip = DbContext.ActiveTrips.Where(t => t.DriverId == CurrentUser.Id || t.EmployerId == CurrentUser.Id).FirstOrDefault();
            if (trip.DriverId == CurrentUser.Id)
            {
                return DbContext.UserLocations.Where(u => u.UserId == trip.EmployerId).FirstOrDefault();
            }
            else if (trip.EmployerId == CurrentUser.Id)
            {
                return DbContext.UserLocations.Where(u => u.UserId == trip.DriverId).FirstOrDefault();
            }
            throw new Exception();
        }

        public User GetPartner()
        {
            ActiveTrip trip = DbContext.ActiveTrips.Where(t => t.DriverId == CurrentUser.Id || t.EmployerId == CurrentUser.Id).FirstOrDefault();
            if (trip.DriverId == CurrentUser.Id)
            {
                return DbContext.Users.Where(u => u.Id == trip.EmployerId).FirstOrDefault();
            }
            else if (trip.EmployerId == CurrentUser.Id)
            {
                return DbContext.Users.Where(u => u.Id == trip.DriverId).FirstOrDefault();
            }
            throw new Exception();
        }

        public object SetLocation(UserLocation newLocation)
        {
            ActiveTrip trip = null;
            UserLocation partnerLocation = null;


            var userLocation = DbContext.UserLocations.Where(l => l.UserId == CurrentUser.Id).FirstOrDefault();
            if (userLocation != null)
            {
                userLocation.Latitude = newLocation.Latitude;
                userLocation.Longitude = newLocation.Longitude;
                userLocation.LocatedTime = DateTime.Now;
                if (userLocation.HasTrip)
                {
                    if (currentUser.Role == Enum.RoleString.Driver)
                    {
                        var activeTrip = DbContext.ActiveTrips.Where(t => t.DriverId == CurrentUser.Id).FirstOrDefault();
                        if (activeTrip != null)
                        {
                            trip = activeTrip;
                        }
                    }
                    else if (currentUser.Role == Enum.RoleString.Employer)
                    {
                        var activeTrip = DbContext.ActiveTrips.Where(t => t.EmployerId == CurrentUser.Id /*&& (t.Accepted || !String.IsNullOrWhiteSpace(t.RejectReason))*/).FirstOrDefault();
                        if (activeTrip != null)
                        {
                            if (!String.IsNullOrWhiteSpace(activeTrip.RejectReason))
                            {
                                userLocation.HasTrip = false;
                                DbContext.ActiveTrips.Remove(activeTrip);
                            } else if(activeTrip.Accepted)
                            {
                                userLocation.HasTrip = false;
                                userLocation.OnTrip = true;
                            }
                            trip = activeTrip;
                        }
                    }
                }
                if (userLocation.OnTrip)
                {
                    if (currentUser.Role == Enum.RoleString.Driver)
                    {
                        var activeTrip = DbContext.ActiveTrips.Where(t => t.DriverId == CurrentUser.Id).FirstOrDefault();
                        partnerLocation = DbContext.UserLocations.Where(l => l.UserId == activeTrip.EmployerId).FirstOrDefault();
                    } else if (currentUser.Role == Enum.RoleString.Employer)
                    {
                        var activeTrip = DbContext.ActiveTrips.Where(t => t.EmployerId == CurrentUser.Id).FirstOrDefault();
                        partnerLocation = DbContext.UserLocations.Where(l => l.UserId == activeTrip.DriverId).FirstOrDefault();
                    }
                }
                DbContext.UserLocations.Update(userLocation);
            }
            else
            {
                userLocation = newLocation;
                userLocation.User = CurrentUser;
                DbContext.UserLocations.Add(newLocation);
            }

            DbContext.SaveChanges();
            return new
            {
                Trip = trip,
                PartnerLocation = partnerLocation
            };
        }
    }
}
