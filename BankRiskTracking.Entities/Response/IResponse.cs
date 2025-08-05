using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRiskTracking.Entities.Response
{
    public  interface IResponse<T>
    {
        bool IsSuccess { get; }
        string Message { get; }

        public T Data { get; set; }
    }
}
