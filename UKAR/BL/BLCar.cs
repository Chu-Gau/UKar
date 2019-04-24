using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UKAR.Model;

namespace UKAR.BL
{
    public class BLCar:BLBase
    {
        private BLUser bLUser;

        private User currentUser;


        public BLCar(
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

        public void Register(Car car)
        {
            CurrentUser.Car = car;
            DbContext.Users.Update(CurrentUser);
            DbContext.SaveChanges();
        }

    }
}
