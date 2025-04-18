using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace HRMS.Management.MSSQLDataAccess
{
    public class DBMSSQLCommand
    {
        #region Private Variables

        private List<SqlParameter> parameters = null;
        public List<SqlParameter> OutputParameters = null;
        private string strDBCommand = string.Empty;
        private string strConnectionString = string.Empty;
        private SqlTransaction transaction = null;
        private SqlConnection connection = null;
        private CommandType commandType = CommandType.Text;

        #endregion


        #region Constructors
        public DBMSSQLCommand(string _connnectString)
        {
            strConnectionString = _connnectString;
        }
        #endregion

        #region Propertise
        public string txt_DBCommand
        {
            get
            {
                return strDBCommand;
            }
            set
            {
                strDBCommand = value;
            }
        }
        public CommandType Type
        {
            get
            {
                return commandType;
            }
            set
            {
                commandType = value;
            }
        }
        #endregion


        private void OpenDatabase()
        {
            if (connection != null)
                return;

            try
            {
                connection = new SqlConnection(strConnectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred in DBCommand.OpenDatabase w/ connection string: '" + strConnectionString + "', see inner exception for details.", ex);
            }
        }

        private void ClosedDatabase()
        {
            if (connection == null)
                return;
            try
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    transaction.Dispose();
                    transaction = null;
                }
                connection.Close();
                connection.Dispose();
                transaction = null;
            }
            catch (Exception ex) { throw new Exception("An unexpected error occurred in DBCommand.CloseDatabase, see inner exception for details.", ex); }
        }

        public void BeginTransaction()
        {
            if (transaction != null)
                throw new Exception("DBCommand.BeginTransaction: A transaction is already in progress.");

            try
            {
                OpenDatabase();
                connection.Open();
                transaction = connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public void CommitTransaction()
        {
            if (transaction == null)
                return;

            try
            {
                transaction.Commit();
                transaction = null;
                ClosedDatabase();
            }
            catch (System.Exception e)
            {
                throw new Exception("An unexpected error occurred in DBCommand.CommitTransaction, see inner exception for details.", e);
            }
        }
        public void RollbackTransaction()
        {
            if (transaction == null)
                return;

            try
            {
                transaction.Rollback();
                ClosedDatabase();
            }
            catch (System.Exception e)
            {
                throw new Exception("An unexpected error occurred in DBCommand.RollbackTransaction, see inner exception for details.", e);
            }
        }

        private SqlCommand BuildCommand()
        {

            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = commandType;
            cmd.CommandText = strDBCommand;
            cmd.Prepare();
            if (transaction != null)
                cmd.Transaction = transaction;

            if (parameters != null)
            {
                foreach (SqlParameter parm in parameters)
                    cmd.Parameters.Add(parm);

                //cmd.Parameters.Add("s_docnum", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
            }

            return cmd;
        }

        public void AddParameter(string txtParamName, DbType paramDbType, object paramValue, ParameterDirection paramDirection=ParameterDirection.Input)
        {
            if (parameters == null)
                parameters = new List<SqlParameter>();

            SqlParameter newParam = new SqlParameter(txtParamName, paramValue);
            newParam.DbType = paramDbType;
            newParam.Direction = paramDirection;
            newParam.Value = paramValue;

            if (parameters.Contains(newParam))
                throw new Exception("DBCommand.AddParameter: Parameter already exists");
             else
                parameters.Add(newParam);
        }

        public void AddParameter(string txtParamName, DbType paramDbType, int length, object paramValue, ParameterDirection paramDirection)
        {
            if (parameters == null)
                parameters = new List<SqlParameter>();

            SqlParameter newParam = new SqlParameter(txtParamName, paramValue);
            newParam.Size = length;
            newParam.DbType = paramDbType;
            newParam.Direction = paramDirection;
            if (parameters.Contains(newParam))
                throw new Exception("DBCommand.AddParameter: Parameter already exists.");
            else
                parameters.Add(newParam);
        }

        public DataSet ExecuteCommand(string txtCommandText)
        {
            strDBCommand = txtCommandText;
            return ExecuteCommand();
        }

        public DataSet ExecuteCommand()
        {
            if (strDBCommand == String.Empty)
                throw new Exception("Command text not defined.");

            SqlCommand cmd = null;

            try
            {
                OpenDatabase();
                cmd = BuildCommand();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                da.Dispose();

                OutputParameters = new List<SqlParameter>();
                foreach (SqlParameter item in cmd.Parameters)
                {
                    if (item.Direction == ParameterDirection.Output)
                    {
                        OutputParameters.Add(item);
                    }
                }

                return ds;
            }
            catch (System.Exception e)
            {
                e.Data.Add("SQLCommand", strDBCommand);
                if (transaction != null)
                    e.Data.Add("Transaction", "Yes");
                else
                    e.Data.Add("Transaction", "No");
                if (parameters != null)
                {
                    foreach (SqlParameter parm in parameters)
                        e.Data.Add(parm.ParameterName, parm.Value);
                }
                throw new Exception("An unexpected error occurred in DBCommand.DataTable.ExecuteCommand(), see inner exception for details.", e);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (transaction == null)
                    ClosedDatabase();
            }
        }

        /// <summary>
        /// Exceute Non Query 
        /// </summary>
        /// <param name="txtCommandText"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string txtCommandText)
        {
            strDBCommand = txtCommandText;
            return ExecuteNonQuery();
        }
        public int ExecuteNonQuery()
        {
            if (strDBCommand == String.Empty)
                throw new Exception("Command text not defined.");

            int outVal = -1;
            SqlCommand cmd = null;
            try
            {
                OpenDatabase();
                cmd = BuildCommand();

                ////// Add output parameter
                //SqlParameter returnValueParam = new SqlParameter("@ReturnValue", SqlDbType.Int);
                //returnValueParam.Direction = ParameterDirection.Output;
                //cmd.Parameters.Add(returnValueParam);

                if (connection.State != ConnectionState.Open)
                    connection.Open();
                //outVal = cmd.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                //Get the value of the output parameter
                outVal = (int)cmd.Parameters["@ReturnValue"].Value;
                //outVal = (int)returnValueParam.Value;

                //outVal = InsertProcedureData();
            }
            catch (System.Exception e)
            {
                e.Data.Add("SQLCommand", strDBCommand);
                if (transaction != null)
                    e.Data.Add("Transaction", "Yes");
                else
                    e.Data.Add("Transaction", "No");
                if (parameters != null)
                {
                    foreach (SqlParameter parm in parameters)
                        e.Data.Add(parm.ParameterName, parm.Value);
                }
                throw new Exception("An unexpected error occurred in DBCommand.DataTable.ExecuteNonQuery(), see inner exception for details.", e);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (transaction == null)
                    ClosedDatabase();
            }

            return outVal;
        }


        public void Dispose()
        {
            try
            {
                ClosedDatabase();
            }
            catch { }
            finally
            {
                ClearParameters();
            }
        }

        public void ClearParameters()
        {
            if (parameters != null)
                parameters.Clear();
        }

        ////Testing
        //public int InsertProcedureData()
        //{
        //    using (SqlConnection connection = new SqlConnection(strConnectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand("InsertUpdateEmployeeRecords", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;

        //            // Add input parameters
        //            if (parameters != null)
        //            {
        //                foreach (SqlParameter parm in parameters)
        //                    command.Parameters.AddWithValue(parm.ParameterName, parm.Value);
        //            }
        //            //command.Parameters.AddWithValue("@p_EmployeeID", 0);
        //            //command.Parameters.AddWithValue("@p_Name", "Karnan");
        //            //command.Parameters.AddWithValue("@p_BirthDate", "2025-04-13");
        //            //command.Parameters.AddWithValue("@p_Phone", "+65 543564562");
        //            //command.Parameters.AddWithValue("@p_Email", "karnan@gamil.com");
        //            //command.Parameters.AddWithValue("@p_Gender", "Male");
        //            //command.Parameters.AddWithValue("@p_Address", "TEs  tfstydfty");
        //            //command.Parameters.AddWithValue("@p_Status", "Active");

        //            // Add output parameter
        //            SqlParameter returnValueParam = new SqlParameter("@ReturnValue", SqlDbType.Int);
        //            returnValueParam.Direction = ParameterDirection.Output;
        //            command.Parameters.Add(returnValueParam);

        //            try
        //            {
        //                connection.Open();
        //                command.ExecuteNonQuery();
                            
        //                // Get the value of the output parameter
        //                int result = (int)returnValueParam.Value;
        //                //int result = (int)command.Parameters["@ReturnValue"].Value;

        //                return result; // Return true if the stored procedure indicated success
        //            }
        //            catch (Exception ex)
        //            {
        //                //Console.WriteLine($"Error inserting data: {ex.Message}");
        //                return -1;
        //            }
        //        }
        //    }
        //}
    }
}
