using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UKAR
{
    public class Result
    {
        private Result()
        {

        }

        private ErrorCode _errorCode;

        public object Data { get; set; }
        public ErrorCode ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
                ErrorMessage = _errorCode.ToString();
            }
        }
        public String ErrorMessage { get; private set; }

        public static Result Error(ErrorCode errorCode, object data = null)
        {
            return new Result
            {
                Data = data,
                ErrorCode = errorCode
            };
        }

        public static Result Success(object data = null)
        {
            return Error(ErrorCode.Success, data);
        }

        public static Result UnexpectedError(object data = null)
        {
            return Error(ErrorCode.UnexpectedError, data);
        }

        public static Result InvalidParameter(object data = null)
        {
            return Error(ErrorCode.InvalidParameter, data);
        }
    }
}
