using HRMS.Management.Common.Master;
using HRMS.Management.CommonLayer;
using HRMS.Management.MSSQLDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Management.DataAccessLayer.Master
{
    public class DepartmentData
    {
        private string ConnectionString = DBContext.Instance.ConnectionString;
        //Get All Data
        public DepartmentCollectionDataModel d_GetDepartmentData()
        {
            //DBMySqlCommand dbCommand = null;
            DBMSSQLCommand dbCommand = null;
            DepartmentCollectionDataModel locationResultData = new DepartmentCollectionDataModel();
            int outVal = Constants.MINUSTWO;

            try
            {
                DataSet ds = new DataSet();
                //dbCommand = new DBMySqlCommand(ConnectionString);
                dbCommand = new DBMSSQLCommand(ConnectionString);
                dbCommand.Type = CommandType.StoredProcedure;
                //dbCommand.AddParameter("@p_name", DbType.String, name);
                ds = dbCommand.ExecuteCommand("sp_GetDepartmentDetails");
                if (ds?.Tables.Count == Constants.ONE && ds.Tables[0].Rows.Count > Constants.ZERO)
                {
                    ICoreData coreData = new CoreData();
                    locationResultData.DepartmentModelList = coreData.ConvertToList<Department>(ds.Tables[0]);
                    locationResultData.Status = CoreResponseStatus.Success;
                    locationResultData.Message = "Data available";
                    outVal = Constants.ONE;
                }
                else if (ds == null || ds.Tables.Count == Constants.ZERO)
                    outVal = Constants.MINUSONE;
                else
                    outVal = Constants.ZERO;
            }
            catch (Exception ex)
            {
                    return new Common().GetCoreResponse<DepartmentCollectionDataModel>(Constants.MINUSTWO, string.Format("Function Name: {0} - {1]", "d_GetDepartmentData", ex.Message));
            }
            finally
            {
                if (dbCommand != null)
                    dbCommand.Dispose();
            }
            return outVal == Constants.ONE ? locationResultData : new Common().GetCoreResponse<DepartmentCollectionDataModel>(outVal);
        }
    }
}
