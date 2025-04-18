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
    public class RolesData
    {
        private string ConnectionString = DBContext.Instance.ConnectionString;

        //Get All Data
        public RolesCollectionDataModel d_GetRolesData()
        {
            //DBMySqlCommand dbCommand = null;
            DBMSSQLCommand dbCommand = null;
            RolesCollectionDataModel rolesResultData = new RolesCollectionDataModel();
            int outVal = Constants.MINUSTWO;

            try
            {
                DataSet ds = new DataSet();
                //dbCommand = new DBMySqlCommand(ConnectionString);
                dbCommand = new DBMSSQLCommand(ConnectionString);
                dbCommand.Type = CommandType.StoredProcedure;
                //dbCommand.AddParameter("@p_name", DbType.String, name);
                ds = dbCommand.ExecuteCommand("sp_GetRolesDetails");
                if (ds?.Tables.Count == Constants.ONE && ds.Tables[0].Rows.Count > Constants.ZERO)
                {
                    ICoreData coreData = new CoreData();
                    rolesResultData.RolesModelList = coreData.ConvertToList<Roles>(ds.Tables[0]);
                    rolesResultData.Status = CoreResponseStatus.Success;
                    rolesResultData.Message = "Data available";
                    outVal = Constants.ONE;
                }
                else if (ds == null || ds.Tables.Count == Constants.ZERO)
                    outVal = Constants.MINUSONE;
                else
                    outVal = Constants.ZERO;
            }
            catch (Exception ex)
            {
                return new Common().GetCoreResponse<RolesCollectionDataModel>(Constants.MINUSTWO, string.Format("Function Name: {0} - {1]", "d_GetRolesData", ex.Message));
            }
            finally
            {
                if (dbCommand != null)
                    dbCommand.Dispose();
            }
            return outVal == Constants.ONE ? rolesResultData : new Common().GetCoreResponse<RolesCollectionDataModel>(outVal);
        }
    }
}
