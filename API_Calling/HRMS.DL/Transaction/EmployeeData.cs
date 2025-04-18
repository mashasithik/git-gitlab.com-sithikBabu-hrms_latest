using HRMS.Management.Common.MariaDBAccess;
using HRMS.Management.Common.Master;
using HRMS.Management.Common.Transaction;
using HRMS.Management.CommonLayer;
using HRMS.Management.MSSQLDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HRMS.Management.DataAccessLayer.Transaction
{
    public class EmployeeData
    {
        private string ConnectionString = DBContext.Instance.ConnectionString;

        //Get All Data
        public EmployeesCollectionDataModel d_GetEmployeedData(string name)
        {
            //DBMySqlCommand dbCommand = null;
            DBMSSQLCommand dbCommand = null;
            EmployeesCollectionDataModel employeeResultData = new EmployeesCollectionDataModel();
            int outVal = Constants.MINUSTWO;

            try
            {
                DataSet ds = new DataSet();
                //dbCommand = new DBMySqlCommand(ConnectionString);
                dbCommand = new DBMSSQLCommand(ConnectionString);
                dbCommand.Type = CommandType.StoredProcedure;
                dbCommand.AddParameter("@p_name", DbType.String, name);
                ds = dbCommand.ExecuteCommand("sp_GetEmployeeDetailsFilter");
                if (ds?.Tables.Count == Constants.ONE && ds.Tables[0].Rows.Count > Constants.ZERO)
                {
                    ICoreData coreData = new CoreData();
                    employeeResultData.employeeModelList = coreData.ConvertToList<EmployeesModel>(ds.Tables[0]);
                    employeeResultData.Status = CoreResponseStatus.Success;
                    employeeResultData.Message = "Data available";
                    outVal = Constants.ONE;
                }
                else if (ds == null || ds.Tables.Count == Constants.ZERO)
                    outVal = Constants.MINUSONE;
                else
                    outVal = Constants.ZERO;
            }
            catch (Exception ex)
            {
                return new Common().GetCoreResponse<EmployeesCollectionDataModel>(Constants.MINUSTWO, string.Format("Function Name: {0} - {1]", "d_GetEmployeedData", ex.Message));
            }
            finally
            {
                if (dbCommand != null)
                    dbCommand.Dispose();
            }
            return outVal == Constants.ONE ? employeeResultData : new Common().GetCoreResponse<EmployeesCollectionDataModel>(outVal);
        }

        //Post All Data
        public ICoreResponse d_PostEmployeeData(EmployeesModel employeesModel)
        {
            DBMSSQLCommand dbCommand = null;
            int outVal = Constants.MINUSTWO;

            try
            {
                dbCommand = new DBMSSQLCommand(ConnectionString);
                dbCommand.Type = CommandType.StoredProcedure;
                dbCommand.AddParameter("@p_EmployeeID", DbType.Int64, employeesModel.EmployeeID);
                dbCommand.AddParameter("@p_Name", DbType.String, employeesModel.Name);
                dbCommand.AddParameter("@p_BirthDate", DbType.String, employeesModel.BirthDate);
                dbCommand.AddParameter("@p_Phone", DbType.String, employeesModel.Phone);
                dbCommand.AddParameter("@p_Email", DbType.String, employeesModel.Email);
                dbCommand.AddParameter("@p_Gender", DbType.String, employeesModel.Gender);
                dbCommand.AddParameter("@p_Address", DbType.String, employeesModel.Address);
                dbCommand.AddParameter("@p_Status", DbType.String, employeesModel.Status);
                //Return Output
                dbCommand.AddParameter("@ReturnValue", DbType.Int32, -2, ParameterDirection.Output);
                outVal = dbCommand.ExecuteNonQuery("sp_InsertUpdateEmployeeRecords");
             }
            catch (Exception ex)
            {

            }
            finally
            {
                if (dbCommand != null)
                    dbCommand.Dispose();
            }
            Common common = new Common();
            return common.GetCoreResponsePost("Employee Record Inserted/Updated", outVal);
        }

        //Delete the records
        public ICoreResponse d_DeleteEmployeeData(int employeeId)
        {
            DBMSSQLCommand dbCommand = null;
            int outVal = Constants.MINUSTWO;

            try
            {
                Guid newGuid = Guid.NewGuid();
                dbCommand = new DBMSSQLCommand(ConnectionString);
                dbCommand.Type = CommandType.StoredProcedure;
                dbCommand.AddParameter("@p_EmployeeID", DbType.Int32, employeeId);
                dbCommand.AddParameter("@ReturnValue", DbType.Int32, -2, ParameterDirection.Output);
                outVal = dbCommand.ExecuteNonQuery("sp_DeleteEmployeeRecords");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (dbCommand != null)
                    dbCommand.Dispose();
            }

            Common common = new Common();
            return common.GetCoreResponseDelete("Employee", outVal);
        }
    }
}
