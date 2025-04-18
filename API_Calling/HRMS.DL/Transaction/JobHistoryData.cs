using HRMS.Management.Common.Transaction;
using HRMS.Management.CommonLayer;
using HRMS.Management.MSSQLDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Management.DataAccessLayer.Transaction
{
    public class JobHistoryData
    {
        private string ConnectionString = DBContext.Instance.ConnectionString;
        //Get All Data
        public JobHistoryCollectionDataModel d_GetJobHistoryData(int employeeID)
        {
            //DBMySqlCommand dbCommand = null;
            DBMSSQLCommand dbCommand = null;
            JobHistoryCollectionDataModel jobHistoryResultData = new JobHistoryCollectionDataModel();
            int outVal = Constants.MINUSTWO;

            try
            {   
                DataSet ds = new DataSet();
                //dbCommand = new DBMySqlCommand(ConnectionString);
                dbCommand = new DBMSSQLCommand(ConnectionString);
                dbCommand.Type = CommandType.StoredProcedure;
                dbCommand.AddParameter("@p_EmployeeID", DbType.Int32, employeeID);

                ds = dbCommand.ExecuteCommand("sp_GetJobHistoryDetail");
                if (ds?.Tables.Count == Constants.ONE && ds.Tables[0].Rows.Count > Constants.ZERO)
                {
                    ICoreData coreData = new CoreData();
                    jobHistoryResultData.JobHistoryModelList = coreData.ConvertToList<JobHistory>(ds.Tables[0]);
                    jobHistoryResultData.Status = CoreResponseStatus.Success;
                    jobHistoryResultData.Message = "Data available";
                    outVal = Constants.ONE;
                }
                else if (ds == null || ds.Tables.Count == Constants.ZERO)
                    outVal = Constants.MINUSONE;
                else
                    outVal = Constants.ZERO;
            }
            catch (Exception ex)
            {
                return new Common().GetCoreResponse<JobHistoryCollectionDataModel>(Constants.MINUSTWO, string.Format("Function Name: {0} - {1]", "d_GetJobHistoryData", ex.Message));
            }
            finally
            {
                if (dbCommand != null)
                    dbCommand.Dispose();
            }
            return outVal == Constants.ONE ? jobHistoryResultData : new Common().GetCoreResponse<JobHistoryCollectionDataModel>(outVal);
        }

        //Post All Data
        public ICoreResponse d_PostJobHistoryData(JobHistory jobHistory)
        {
            DBMSSQLCommand dbCommand = null;
            int outVal = Constants.MINUSTWO;
            Common common = new Common();
            try
            {
                dbCommand = new DBMSSQLCommand(ConnectionString);
                dbCommand.Type = CommandType.StoredProcedure;
                dbCommand.AddParameter("@p_JobHistoryID", DbType.Int64, jobHistory.JobHistoryID);
                dbCommand.AddParameter("@p_EmployeeID", DbType.String, jobHistory.EmployeeID);
                dbCommand.AddParameter("@p_ManagerID", DbType.String, jobHistory.ManagerID);
                dbCommand.AddParameter("@p_RoleID", DbType.String, jobHistory.RoleID);
                dbCommand.AddParameter("@p_StartDate", DbType.String, jobHistory.StartDate);
                dbCommand.AddParameter("@p_EndDate", DbType.String, jobHistory.EndDate);
                dbCommand.AddParameter("@p_Status", DbType.String, jobHistory.Status);
                dbCommand.AddParameter("@p_Comments", DbType.String, jobHistory.Comments);
                //Return Output
                dbCommand.AddParameter("@ReturnValue", DbType.Int32, -2, ParameterDirection.Output);
                outVal = dbCommand.ExecuteNonQuery("sp_InsertUpdateJobHistoryRecords");
            }
            catch (Exception ex)
            {
                return common.GetCoreResponsePost(string.Format("Function Name: {0} - {1]", "d_PostJobHistoryData", ex.Message), Constants.MINUSTWO);
            }
            finally
            {
                if (dbCommand != null)
                    dbCommand.Dispose();
            }
            
            return common.GetCoreResponsePost("JobHistory Record Inserted/Updated", outVal);
        }

        //Delete the records
        public ICoreResponse d_DeletJobHistoryData(int jobHistoryId)
        {
            DBMSSQLCommand dbCommand = null;
            int outVal = Constants.MINUSTWO;
            Common common = new Common();
            try
            {
                Guid newGuid = Guid.NewGuid();
                dbCommand = new DBMSSQLCommand(ConnectionString);
                dbCommand.Type = CommandType.StoredProcedure;
                dbCommand.AddParameter("@p_JobHistoryID", DbType.Int32, jobHistoryId);
                dbCommand.AddParameter("@ReturnValue", DbType.Int32, -2, ParameterDirection.Output);
                outVal = dbCommand.ExecuteNonQuery("sp_DeleteJobHistoryRecords");
            }
            catch (Exception ex)
            {
                return common.GetCoreResponseDelete(string.Format("Function Name: {0} - {1]", "d_PostJobHistoryData", ex.Message), Constants.MINUSTWO);
            }
            finally
            {
                if (dbCommand != null)
                    dbCommand.Dispose();
            }           
            return common.GetCoreResponseDelete("JobHistory", outVal);
        }
    }
}
