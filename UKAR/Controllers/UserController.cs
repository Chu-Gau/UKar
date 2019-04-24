using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UKAR.BL;
using UKAR.Model;

namespace UKAR.Profile
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private BLUser blUser;
        private BLLicense bLLicense;

        public UserController(BLUser blUser, BLLicense bLLicense)
        {
            this.blUser = blUser;
            this.bLLicense = bLLicense;
        }

        /// <summary>
        /// Đăng ký tài khoản
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [Route("~/register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] User newUser)
        {
            try
            {
                if (ModelState.IsValid && blUser.ValidateRegister(newUser))
                {
                    var result = await blUser.CreateUserAsync(newUser);
                    if (result.Succeeded)
                        return Ok(Result.Success(result));
                    else
                        return BadRequest(Result.Error(ErrorCode.RegisterFailed, result));
                }
                return BadRequest(Result.InvalidParameter());
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }

        /// <summary>
        /// Kiểm tra trùng
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [Route("~/register/check-exist")]
        [HttpPost]
        public ActionResult CheckExists([FromBody] string emailOrPhone)
        {
            try
            {
                return Ok(Result.Success(blUser.IsExists(emailOrPhone)));
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }

        /// <summary>
        /// Đăng nhập bằng tài khoản vs mật khẩu
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("~/login")]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] User user)
        {
            try
            {
                if (ModelState.IsValid && blUser.ValidateLogin(user))
                {
                    var result = await blUser.LoginAsync(user.UserName, user.Password);
                    if (result.Succeeded)
                        return Ok(Result.Success(result));
                    else
                        return BadRequest(Result.Error(ErrorCode.InvalidLogin, result));
                }
                return BadRequest(Result.InvalidParameter());
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }

        /// <summary>
        /// Lấy thông tin người dùng
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("~/userinfo")]
        [HttpGet]
        [Authorize]
        public ActionResult GetUser()
        {
            try
            {
                var curUser = blUser.CurrentUser;
                return Ok(Result.Success(blUser.GetUserDetail(curUser.Id)));
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }

        /// <summary>
        /// Thiết đặt role cho user
        /// </summary>
        /// <param name="roleString"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/role")]
        [Authorize]
        public async Task<ActionResult> SetUserRole([FromBody] string roleString)
        {
            try
            {
                await blUser.AssignUserRoleAsync(blUser.CurrentUser.UserName, roleString);
                return Ok(Result.Success());
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }

        /// <summary>
        /// Lấy role cho user
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/role")]
        [Authorize]
        public ActionResult GetUserRole()
        {
            try
            {
                return Ok(Result.Success(blUser.CurrentUser.Role));
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }

        /// <summary>
        /// LogOut
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/logout")]
        [Authorize]
        public ActionResult LogOut()
        {
            var task = blUser.LogOut();
            return Ok(Result.Success());
        }

        [HttpGet]
        [Route("~/CreateAdmin")]
        public async Task<ActionResult> CreateAdminAsync()
        {
            var user = new User()
            {
                UserName = "Admin",
                Password = "adminbetez",
                Email = "admin@betezminor.com",
                Role = Enum.RoleString.Admin
            };
            var result = await blUser.UserManager.CreateAsync(user, user.Password);

            if (result.Succeeded)
            {
                await blUser.RoleManager.CreateAsync(new IdentityRole("Admin"));
                await blUser.UserManager.AddToRoleAsync(user, "Admin");
            }
            return Ok();
        }

        [HttpGet]
        [Route("~/user")]
        [Authorize(Roles = Enum.RoleString.Admin)]
        public ActionResult GetAllUsers()
        {
            try
            {
                var users = blUser.GetAllUsers();
                return Ok(Result.Success(users));
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }

        [HttpPost]
        [Route("license")]
        [Authorize(Roles = Enum.RoleString.Admin)]
        public ActionResult SetLicense(DrivingLicense license)
        {
            try
            {
                bLLicense.UpdateLicense(license);
                return Ok(Result.Success());
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }

        [HttpGet]
        [Route("license/{userID}")]
        [Authorize(Roles = Enum.RoleString.Admin)]
        public ActionResult GetLicense(string userID)
        {
            try
            {
                return Ok(Result.Success(bLLicense.GetLicense(userID)));
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }

        [HttpPost]
        [Route("update-test-date")]
        [Authorize(Roles = Enum.RoleString.Admin)]
        public async Task<ActionResult> UpdateTestDateAsync([FromBody]User user)
        {
            try
            {
                //var testDate = DateTime.Parse(date);
                bLLicense.SetTestDateAsync(user);
                return Ok(Result.Success());
            }
            catch (Exception)
            {
                return StatusCode(500, Result.UnexpectedError());
            }
        }
    }
}
