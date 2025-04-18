using HRMS.Management.CommonLayer;
using System;
using System.Runtime.CompilerServices;

namespace HRMS.Management.DataAccessLayer
{
    public class Common
    {
        internal CoreResponse GetCoreResponsePost(string operation, int outVal = -2, string responseValue = "")
        {
            if (outVal >= 1)
                return new CoreResponse { Status = CoreResponseStatus.Success, Message = operation?.Trim() + " added successfully", ResponseValue = responseValue };
            else if (outVal == 0)
                return new CoreResponse { Status = CoreResponseStatus.Duplicate, Message = operation?.Trim() + " name already exists" };
            else if (outVal == -1)
                return new CoreResponse { Status = CoreResponseStatus.DBError, Message = "Unable to add the " + operation };
            else if (outVal == -3)
                return new CoreResponse { Status = CoreResponseStatus.Failure, Message = operation?.Trim() + " creation failed" };
            else
                return new CoreResponse { Status = CoreResponseStatus.Error, Message = "Unable to add the " + operation };
        }

        internal CoreResponse GetCoreResponseUpdate(string operation, int outVal = -2, string responseValue = "")
        {
            if (outVal >= 1)
                return new CoreResponse { Status = CoreResponseStatus.Success, Message = operation?.Trim() + " updated successfully", ResponseValue = responseValue };
            else if (outVal == 0)
                return new CoreResponse { Status = CoreResponseStatus.Duplicate, Message = operation?.Trim() + " name already exists" };
            else if (outVal == -1)
                return new CoreResponse { Status = CoreResponseStatus.DBError, Message = "Unable to update the " + operation };
            else if (outVal == -3)
                return new CoreResponse { Status = CoreResponseStatus.Failure, Message = operation?.Trim() + " updation failed" };
            else
                return new CoreResponse { Status = CoreResponseStatus.Error, Message = string.Format("Unable to fetch the data - Exception Message : {0}", operation)};
        }

        internal CoreResponse GetCoreResponseDelete(string operation, int outVal = -2)
        {
            if (outVal >= 1)
                return new CoreResponse { Status = CoreResponseStatus.Success, Message = operation?.Trim() + " deleted successfully" };
            else if (outVal == -1)
                return new CoreResponse { Status = CoreResponseStatus.DBError, Message = "Unable to delete the " + operation?.Trim() };
            else if (outVal == -3)
                return new CoreResponse { Status = CoreResponseStatus.Failure, Message = operation?.Trim() + " deletion failed" };
            else
                return new CoreResponse { Status = CoreResponseStatus.Error, Message = string.Format("Unable to delete the data - Exception Message : {0}", operation) };
        }

        internal T GetCoreResponse<T>(int outVal = -2, string exception="")
        {
            CoreResponseStatus CoreResponseStatus;
            string message;
            if (outVal >= 1)
            { CoreResponseStatus = CoreResponseStatus.Success; message = "Data available"; }
            else if (outVal == 0)
            { CoreResponseStatus = CoreResponseStatus.NoData; message = "No data available"; }
            else if (outVal == -1)
            { CoreResponseStatus = CoreResponseStatus.DBError; message = "Unable to fetch the data"; }
            else
            { CoreResponseStatus = CoreResponseStatus.Error; message = string.Format("Unable to fetch the data - Exception Message : {0}", exception); }

            var objResponse = (T)Activator.CreateInstance(typeof(T));
            var coreProperties = objResponse.GetType().GetProperties().ToList();
            var propertyKey = coreProperties.Where(x => x.Name == "Status" && x.CanRead).FirstOrDefault();
            if (propertyKey != null)
                propertyKey.SetValue(objResponse, CoreResponseStatus);
            propertyKey = coreProperties.Where(x => x.Name == "Message" && x.CanRead).FirstOrDefault();
            if (propertyKey != null)
                propertyKey.SetValue(objResponse, message);

            return objResponse;
        }

    }
}
