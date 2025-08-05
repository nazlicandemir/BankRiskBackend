using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRiskTracking.Entities.Response
{
    public class ResponseGeneric<T> : IResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ResponseGeneric(T data, bool isSuccess, string message)
        {
            Data = data;
            IsSuccess = isSuccess;
            Message = message;
        }

        public static ResponseGeneric<T> Success(T data, string message = "")
        {
            return new ResponseGeneric<T>(data, true, message);
        }

        public static ResponseGeneric<T> Error(string message = "")
        {
            return new ResponseGeneric<T>(default(T), false, message);
        }
    }

}
