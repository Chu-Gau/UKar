using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UKAR
{
    public enum ErrorCode
    {
        Success = 0, //request thành công, không lỗi
        NotLoggedIn = 1,
        UnexpectedError = 2, //Lỗi không xác định
        InvalidParameter = 4,
        InvalidLogin = 5,
        RegisterFailed = 6
    }
}
