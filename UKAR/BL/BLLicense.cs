using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UKAR.Model;

namespace UKAR.BL
{
    public class BLLicense:BLBase
    {
        private BLUser bLUser;

        public BLLicense(
            IConfiguration configuration,
            UKarDBContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            BLUser bLUser
            ) : base(configuration, dbContext, httpContextAccessor)
        {
                this.bLUser = bLUser;

        }

        public void UpdateLicense(DrivingLicense license)
        {
            var oldLicense = DbContext.DrivingLicenses.Where(l => l.DriverId == license.DriverId).FirstOrDefault();
            if (oldLicense != null)
            {
                DbContext.DrivingLicenses.Remove(oldLicense);
            }
            DbContext.DrivingLicenses.Add(license);
            DbContext.SaveChanges();
        }

        public DrivingLicense GetLicense(string userId)
        {
            return DbContext.DrivingLicenses.Where(l => l.DriverId == userId).FirstOrDefault();
        }

        public void SetTestDateAsync(User user)
        {
            var userupdate = DbContext.Users.Where(u => u.Id == user.Id).FirstOrDefault();
            userupdate.TestTime = user.TestTime;
            DbContext.Users.Update(userupdate);
            DbContext.SaveChanges();
        }
    }
}