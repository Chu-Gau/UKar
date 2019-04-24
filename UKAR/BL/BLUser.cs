using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UKAR.Model;

namespace UKAR.BL
{
    public class BLUser:BLBase
    {
        private User _currentUser;

        public UserManager<User> UserManager { get; }

        public SignInManager<User> SignInManager { get; }

        public IEmailSender EmailSender { get; }

        public RoleManager<IdentityRole> RoleManager { get; }

        public User CurrentUser
        {
            get
            {
                _currentUser = _currentUser ?? UserManager.GetUserAsync(HttpAccessor.HttpContext.User).Result;
                return _currentUser;
            }
        }

        /// <summary>
        /// Tạo mới BL User
        /// </summary>
        /// <param name="userManager">Truyển từ context</param>
        /// <param name="signInManager">Truyển từ context</param>
        /// <param name="emailSender">Truyển từ context</param>
        /// <param name="configuration">Truyển từ context</param>
        /// <param name="roleManager">Truyển từ context</param>
        /// <param name="dbContext">Truyển từ context</param>
        /// <param name="user">Truyển từ controller</param>
        public BLUser(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager,
            UKarDBContext dbContext,
            IHttpContextAccessor httpContextAccessor
            ):base(configuration, dbContext, httpContextAccessor)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.EmailSender = emailSender;
            this.RoleManager = roleManager;
        }

        /// <summary>
        /// Tạo tài khoản mới
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateUserAsync(User newUser)
        {
            //Lấy phone number làm tên đăng nhập
            newUser.UserName = newUser.PhoneNumber;
            var result = await UserManager.CreateAsync(newUser, newUser.Password);

            if (result.Succeeded)
            {
                //Đăng ký role luôn
                await AssignUserRoleAsync(newUser.UserName, newUser.Role);
            }
            return result;
        }

        public bool IsExists(string mailOrPhone)
        {
            User user = DbContext.Users.Where(u => u.Email == mailOrPhone || u.PhoneNumber == mailOrPhone).FirstOrDefault();
            return user == null;
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent">Nhớ mật khẩu</param>
        /// <returns></returns>
        public async Task<SignInResult> LoginAsync(string username, string password, bool isPersistent = false)
        {
            var result = await SignInManager.PasswordSignInAsync(username, password, isPersistent, true);
            if (!result.Succeeded)
            {//Nếu không thành công thì thử với email
                username = DbContext.Users.Where(u => u.Email == username).Select(u => u.PhoneNumber).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(username))
                    result = await SignInManager.PasswordSignInAsync(username, password, isPersistent, true);
            }
            return result;
        }

        /// <summary>
        /// Thiết lập vai trò người dùng
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleString"></param>
        /// <returns></returns>
        public async Task AssignUserRoleAsync(string userName, string roleString)
        {
            var role = System.Enum.Parse<Enum.Role>(roleString);
            var user = DbContext.Users.Where(u => u.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                if ((role & Enum.Role.UserRole) == role) //Kiểm tra role hợp lệ
                {
                    await AssignRoleAsync(user, role);
                }
            }
        }

        /// <summary>
        /// Thiết đặt vai trò tài khoản
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task AssignRoleAsync(User user, Enum.Role role)
        {
            user.Role = role.ToString();

            await RoleManager.CreateAsync(new IdentityRole(role.ToString()));
            await UserManager.UpdateAsync(user);
            await UserManager.AddToRoleAsync(user, role.ToString());
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        public async Task LogOut()
        {
            try
            {
                var activeDriver = DbContext.ActiveDriver.Where(d => d.UserId == CurrentUser.Id).FirstOrDefault();
                DbContext.ActiveDriver.Remove(activeDriver);
                DbContext.SaveChanges();
                var userLocation = DbContext.UserLocations.Where(d => d.UserId == CurrentUser.Id).FirstOrDefault();
                DbContext.UserLocations.Remove(userLocation);
                DbContext.SaveChanges();
            }
            catch (Exception)
            {
                //kệ
            }
            await SignInManager.SignOutAsync();
        }

        /// <summary>
        /// Kiểm tra dữ liệu truyền lên có hợp lệ khi tạo tài khoản
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool ValidateRegister(User user)
        {
            bool isNotNull = (user.FullName ?? user.Email ?? user.PhoneNumber ?? user.Password) != null;
            bool validEmail = Regex.Match(user.Email, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z0-9]{2,4}$").Success;
            bool validPhoneNumber = Regex.Match(user.PhoneNumber, "^[0-9]*$").Success;
            return isNotNull && validEmail && validPhoneNumber;
        }

        /// <summary>
        /// Kiểm tra dữ liệu truyền lên có hợp lệ khi tạo đăng nhập 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool ValidateLogin(User user)
        {
            return (user.UserName ?? user.Password) != null;
        }

        public User GetUserDetail(string userId)
        {
            return DbContext.Users.Where(u => u.Id == userId)
                .Include(u => u.Car)
                .FirstOrDefault();
        }

        public List<User> GetAllUsers()
        {
            return DbContext.Users.Select(u => new User {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Role = u.Role,
                TestTime = u.TestTime
            }).Where(u => u.Role != Enum.RoleString.Admin).ToList();
        }


    }
}
