using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UKAR.BL
{
    public class BLBase:IBLBase
    {
        public UKarDBContext DbContext { get; }
        public IConfiguration Configuration { get; }

        public IHttpContextAccessor HttpAccessor { get; set; }

        public BLBase(
            IConfiguration configuration,
            UKarDBContext dbContext,
            IHttpContextAccessor httpContextAccessor
            )
        {
            this.HttpAccessor = httpContextAccessor;
            this.DbContext = dbContext;
            this.Configuration = configuration;
        }
    }
}
