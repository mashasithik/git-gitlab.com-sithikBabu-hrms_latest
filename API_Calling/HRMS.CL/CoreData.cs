using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Management.CommonLayer
{
    public class DBContext
    {
        private DBContext()
        {

        }
        private static DBContext instance = null;
        public static DBContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBContext();
                }
                return instance;
            }
        }
        public string ConnectionString { get; set; }
    }

    public class UserToken : IUserTokenValidate
    {
        private string _loggerInUserName;
        private string _token;
        public string Token { get { return _token; } set { _token = value; } }
        public string LoggedInUserName { get { return _loggerInUserName; } set { _loggerInUserName = value; } }
        public Guid LoggedInUserId { get; set; }
    }

    public class CoreResponse : ICoreResponse
    {
        private string _responseValue;
        private CoreResponseStatus _status;
        private string _message;
        public string ResponseValue { get { return _responseValue; } set { _responseValue = value; } }
        public CoreResponseStatus Status { get { return _status; } set { _status = value; } }
        public string Message { get { return _message; } set { _message = value; } }
    }

    public class CoreData
    {
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

    public enum CoreResponseStatus
    {
        Success = 200,
        Warning = 201,
        Failure = 202,
        Information = 203,
        NoData = 204,
        Error = 500,
        TokenInvalid = 501,
        Duplicate = 205,
        DBError = 206
    }
}
