using HRMS.Management.Common.Master;
using HRMS.Management.Common.Transaction;
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
    public class LocationData
    {
        private string ConnectionString = DBContext.Instance.ConnectionString;

        //Get All Data
        public LocationCollectionDataModel d_GetLocationData()
        {
            //DBMySqlCommand dbCommand = null;
            DBMSSQLCommand dbCommand = null;
            LocationCollectionDataModel locationResultData = new LocationCollectionDataModel();
            int outVal = Constants.MINUSTWO;

            try
            {
                DataSet ds = new DataSet();
                //dbCommand = new DBMySqlCommand(ConnectionString);
                dbCommand = new DBMSSQLCommand(ConnectionString);
                dbCommand.Type = CommandType.StoredProcedure;
                //dbCommand.AddParameter("@p_name", DbType.String, name);
                ds = dbCommand.ExecuteCommand("sp_GetLocationDetails");
                if (ds?.Tables.Count == Constants.ONE && ds.Tables[0].Rows.Count > Constants.ZERO)
                {
                    ICoreData coreData = new CoreData();
                    locationResultData.LocationModelList = coreData.ConvertToList<Location>(ds.Tables[0]);
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
                return new Common().GetCoreResponse<LocationCollectionDataModel>(Constants.MINUSTWO, string.Format("Function Name: {0} - {1]", "d_GetLocationData",  ex.Message));
            }
            finally
            {
                if (dbCommand != null)
                    dbCommand.Dispose();
            }
            return outVal == Constants.ONE ? locationResultData : new Common().GetCoreResponse<LocationCollectionDataModel>(outVal);
        }
    }
}
