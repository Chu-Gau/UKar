using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UKAR.Enum
{
    [Flags]
    public enum Role
    {
        Admin = 1,
        Driver = 2,
        Employer = 4,

        UserRole = Driver | Employer
    }
    public class RoleString //Chú ý luôn phải map với cái trên
    {
        public const string Admin = "Admin";
        public const string Driver = "Driver";
        public const string Employer = "Employer";
    }

    [Flags]
    public enum Trip
    {
        Oneway = 1, //1 chiều
        Round = 2, // Khứ hồi
        Both = Oneway | Round // Cả 2
    }
}
