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

        public void UpdateDrivingTest(DrivingTest drivingTest)
        {
            var oldTest = DbContext.DrivingTests.Where(l => l.UserId == drivingTest.UserId).FirstOrDefault();
            if (oldTest != null)
            {
                DbContext.DrivingTests.Remove(oldTest);
            }
            var user = DbContext.Users.Where(u => u.Id == drivingTest.UserId).FirstOrDefault();
            if (drivingTest.ExamScore >= 18 && drivingTest.PracticeScore >= 18)
            {
                drivingTest.Passed = true;
                user.DriverTestPassed = true;
                user.TestTime = drivingTest.Date;
            }
            else
            {
                drivingTest.Passed = false;
                user.DriverTestPassed = false;
                user.TestTime = null;
            }
            DbContext.DrivingTests.Add(drivingTest);
            DbContext.Users.Update(user);
            DbContext.SaveChanges();
        }

        public DrivingTest GetDrivingTest(string userId)
        {
            return DbContext.DrivingTests.Where(l => l.UserId == userId).FirstOrDefault();
        }
    }
}