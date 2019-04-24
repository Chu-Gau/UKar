using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UKAR.BL;
using UKAR.Model;

namespace UKAR.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private BLTrip bLTrip;
        public LocationController(BLTrip bLTrip)
        {
            this.bLTrip = bLTrip;
        }

        [Route("~/location")]
        [HttpPost]
        public async Task<ActionResult> SetLocationAsync([FromBody] UserLocation location)
        {
            try
            {
                return Ok(Result.Success(bLTrip.SetLocation(location)));
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
                throw;
            }
        }
    }
}
