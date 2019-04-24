using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UKAR.BL;
using UKAR.Model;

namespace UKAR.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]

    public class TripController : ControllerBase
    {
        public BLTrip BLTrip { get; set; }

        public TripController(BLTrip blTrip)
        {
            this.BLTrip = blTrip;
        }


        [Route("get-start")]
        [HttpPost]
        [Authorize(Roles = Enum.RoleString.Driver)]
        public async Task<ActionResult> DriverGetStart([FromBody]ActiveTrip activeTrip)
        {
            try
            {
                BLTrip.WaitForTrip(activeTrip.TripType);
                return Ok(Result.Success());
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }

        [Route("find-driver")]
        [HttpPost]
        [Authorize(Roles = Enum.RoleString.Employer)]
        public async Task<ActionResult> FindDriver([FromBody] ActiveTrip newTrip)
        {
            try
            {
                return Ok(Result.Success(BLTrip.FindDriver(newTrip)));
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }

        }

        [Route("reject")]
        [HttpPost]
        [Authorize(Roles = Enum.RoleString.Driver)]
        public ActionResult RejectTrip()
        {
            try
            {
                BLTrip.Reject();
                return Ok(Result.Success());
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }

        }

        [Route("accept")]
        [HttpPost]
        [Authorize(Roles = Enum.RoleString.Driver)]
        public ActionResult AcceptTrip()
        {
            try
            {
                return Ok(Result.Success(BLTrip.Accept()));
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }

        }

        [Route("cancel-wait")]
        [HttpPost]
        [Authorize(Roles = Enum.RoleString.Driver)]
        public ActionResult CancelWait()
        {
            try
            {
                BLTrip.CancelWait();
                return Ok(Result.Success());
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }

        [Route("cancel-find")]
        [HttpPost]
        [Authorize(Roles = Enum.RoleString.Employer)]
        public ActionResult CancelFinding()
        {
            try
            {
                BLTrip.CancelFinding();
                return Ok(Result.Success());
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }

        [Route("cancel")]
        [HttpPost]
        [Authorize(Roles = Enum.RoleString.Driver)]
        public ActionResult Cancel()
        {
            try
            {
                BLTrip.CancelTrip();
                return Ok(Result.Success());
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }

        [Route("finish")]
        [HttpPost]
        [Authorize(Roles = Enum.RoleString.Driver)]
        public ActionResult Finish()
        {
            try
            {
                return Ok(Result.Success(BLTrip.FinishTrip()));
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }

        [Route("partner")]
        [HttpGet]
        [Authorize]
        public ActionResult GetPartnerInfo()
        {
            try
            {
                return Ok(Result.Success(BLTrip.GetPartner()));
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }
    }

}