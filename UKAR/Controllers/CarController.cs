using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UKAR.BL;
using UKAR.Model;

namespace UKAR.Controllers
{
    [Route("~/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private BLCar blCar;
        public CarController(BLCar blCar)
        {
            this.blCar = blCar;
        }

        [Route("register")]
        [HttpPost]
        [Authorize(Roles = Enum.RoleString.Employer)]
        public async Task<ActionResult> Register([FromBody]Car car)
        {
            try
            {
                blCar.Register(car);
                return Ok(Result.Success());
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }
    }
}