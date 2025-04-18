using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Management.CommonLayer
{
    public interface ICoreResponse
    {
        string ResponseValue { get; set; }
        CoreResponseStatus Status { get; set; } 
        string Message { get; set; }
    }

    public interface IUserTokenValidate
    {
        string LoggedInUserName { get; set; }
        string Token { get; set; }

    }
}
