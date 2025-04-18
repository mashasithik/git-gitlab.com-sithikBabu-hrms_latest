using MySqlConnector;
using System.Data;

namespace HRMS.Management.Common.MariaDBAccess
{
    public class DBMySqlCommand
    {
        #region Private Variables
        private List<MySqlParameter>? m_parameters = null;
        public List<MySqlParameter>? OutputParameters = null;
        private string m_txtDBCommand = String.Empty;
        private string m_txtConnectionString = String.Empty;
        private MySqlTransaction m_transaction;
        private MySqlConnection m_connection;
        private CommandType m_commandType = CommandType.Text;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for DBCommand
        /// </summary>
        /// <param name="txtConnectionString"></param>
        public DBMySqlCommand(string txtConnectionString)
        {
            m_txtConnectionString = txtConnectionString;
        }
        #endregion

        private void OpenDatabase()
        {
            if (m_connection != null)
                return;

            try
            {
                m_connection = new MySqlConnection(m_txtConnectionString);
                m_connection.Open();
            }
            catch (System.Exception e)
            {
                throw new Exception("An unexpected error occurred in DBMySQLCommandOpenDatabase w/ connection string: '" + m_txtConnectionString + "', see inner exception for details.", e);
            }
        }
        private void CloseDatabase()
        {
            if (m_connection == null)
                return;

            try
            {
                if (m_transaction != null)
                {
                    m_transaction.Rollback();
                    m_transaction.Dispose();
                    m_transaction = null;
                }
                m_connection.Close();
                m_connection.Dispose();
                m_connection = null;
            }
            catch (System.Exception e)
            {
                throw new Exception("An unexpected error occurred in DBMySQLCommandCloseDatabase, see inner exception for details.", e);
            }
        }
        public void BeginTransaction()
        {
            if (m_transaction != null)
                throw new Exception("DBMySQLCommandBeginTransaction: A transaction is already in progress.");

            try
            {
                OpenDatabase();
                m_connection.Open();
                m_transaction = m_connection.BeginTransaction();
            }
            catch (System.Exception e)
            {
                throw new Exception("An unexpected error occurred in DBMySQLCommandBeginTransaction, see inner exception for details.", e);
            }
        }

        public void CommitTransaction()
        {
            if (m_transaction == null)
                return;

            try
            {
                m_transaction.Commit();
                m_transaction = null;
                CloseDatabase();
            }
            catch (System.Exception e)
            {
                throw new Exception("An unexpected error occurred in DBMySQLCommandCommitTransaction, see inner exception for details.", e);
            }
        }
        public void RollbackTransaction()
        {
            if (m_transaction == null)
                return;

            try
            {
                m_transaction.Rollback();
                CloseDatabase();
            }
            catch (System.Exception e)
            {
                throw new Exception("An unexpected error occurred in DBMySQLCommandRollbackTransaction, see inner exception for details.", e);
            }
        }

        private MySqlCommand BuildCommand()
        {

            MySqlCommand cmd = m_connection.CreateCommand();
            cmd.CommandType = m_commandType;
            cmd.CommandText = m_txtDBCommand;
            cmd.Prepare();
            if (m_transaction != null)
                cmd.Transaction = m_transaction;

            if (m_parameters != null)
            {
                foreach (MySqlParameter parm in m_parameters)
                    cmd.Parameters.Add(parm);

                //cmd.Parameters.Add("s_docnum", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
            }

            return cmd;
        }

        public string txtDBCommand
        {
            get
            {
                return m_txtDBCommand;
            }
            set
            {
                m_txtDBCommand = value;
            }
        }

        public CommandType Type
        {
            get
            {
                return m_commandType;
            }
            set
            {
                m_commandType = value;
            }
        }
        public void AddParameter(string txtParamName, DbType paramType, object paramValue)
        {
            if (m_parameters == null)
                m_parameters = new List<MySqlParameter>();

            MySqlParameter newParm = new MySqlParameter(txtParamName, paramValue);
            newParm.DbType = paramType;
            //newParm.Direction = ParameterDirection.Output;
            //objCmd.Parameters.Add("NEWDOCNO", OracleDbType.NVarchar2, 20).Direction = ParameterDirection.Output;
            if (m_parameters.Contains(newParm))
                throw new Exception("DBMySQLCommandAddParameter: Parameter already exists.");
            else
                m_parameters.Add(newParm);

        }



        public void AddParameter(string txtParamName, DbType paramType, int Length, object paramValue, ParameterDirection parameterDirection)
        {
            if (m_parameters == null)
                m_parameters = new List<MySqlParameter>();

            //OracleParameter newParm = new OracleParameter(txtParamName, OracleDbType.Varchar2, parameterDirection);
            MySqlParameter newParm = new MySqlParameter(txtParamName, paramValue);
            newParm.Size = Length;
            newParm.DbType = paramType;
            newParm.Direction = parameterDirection;//ParameterDirection.Output;
            //OracleParameter newParm = new OracleParameter();
            //newParm.ParameterName = txtParamName;
            //newParm.OracleDbType = OracleDbType.NVarchar2;
            //newParm.Direction = ParameterDirection.Output;

            if (m_parameters.Contains(newParm))
                throw new Exception("DBMySQLCommandAddParameter: Parameter already exists.");
            else
                m_parameters.Add(newParm);
        }

        public DataSet ExecuteCommand(string txtCommandText)
        {
            m_txtDBCommand = txtCommandText;
            return ExecuteCommand();
        }
        public DataSet ExecuteCommand()
        {
            if (m_txtDBCommand == String.Empty)
                throw new Exception("Command text not defined.");

            MySqlCommand cmd = null;

            try
            {
                OpenDatabase();
                cmd = BuildCommand();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                da.Dispose();

                OutputParameters = new List<MySqlParameter>();
                foreach (MySqlParameter item in cmd.Parameters)
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
                e.Data.Add("SQLCommand", m_txtDBCommand);
                if (m_transaction != null)
                    e.Data.Add("Transaction", "Yes");
                else
                    e.Data.Add("Transaction", "No");
                if (m_parameters != null)
                {
                    foreach (MySqlParameter parm in m_parameters)
                        e.Data.Add(parm.ParameterName, parm.Value);
                }
                throw new Exception("An unexpected error occurred in DBMySQLCommandDataTable.ExecuteCommand(), see inner exception for details.", e);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (m_transaction == null)
                    CloseDatabase();
            }
        }
        public int ExecuteNonQuery(string txtCommandText)
        {
            m_txtDBCommand = txtCommandText;
            return ExecuteNonQuery();
        }
        public int ExecuteNonQuery()
        {
            if (m_txtDBCommand == String.Empty)
                throw new Exception("Command text not defined.");

            int outVal = -1;
            MySqlCommand cmd = null;
            try
            {
                OpenDatabase();
                cmd = BuildCommand();
                if (m_connection.State != ConnectionState.Open)
                    m_connection.Open();
                outVal = cmd.ExecuteNonQuery();
            }
            catch (System.Exception e)
            {
                e.Data.Add("SQLCommand", m_txtDBCommand);
                if (m_transaction != null)
                    e.Data.Add("Transaction", "Yes");
                else
                    e.Data.Add("Transaction", "No");
                if (m_parameters != null)
                {
                    foreach (MySqlParameter parm in m_parameters)
                        e.Data.Add(parm.ParameterName, parm.Value);
                }
                throw new Exception("An unexpected error occurred in DBMySQLCommandDataTable.ExecuteNonQuery(), see inner exception for details.", e);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (m_transaction == null)
                    CloseDatabase();
            }

            return outVal;
        }
        public void Dispose()
        {
            try
            {
                CloseDatabase();
            }
            catch { }
            finally
            {
                ClearParameters();
            }
        }

        public void ClearParameters()
        {
            if (m_parameters != null)
                m_parameters.Clear();
        }
    }
}
