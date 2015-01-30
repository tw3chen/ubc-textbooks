using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using UBCTextbooksCore.Custom;

namespace UBCTextbooksCore.Data
{
    public class DataAccess
    {

        public DataAccess(string accessType)
        {
            connectionString = ConfigurationManager.ConnectionStrings[accessType].ConnectionString;
        }

        private SqlConnection getOpenConnection()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            }
            return connection;
        }

        public void ExecuteNonQuery(string storedProcedureName, SqlParameter[] parameters)
        {
            //try
            //{
                this.command = new SqlCommand(storedProcedureName);

                this.command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    for (int i = 0; i < parameters.Length; i++)
                        this.command.Parameters.Add(parameters[i]);

                if (this.connection == null)
                    this.connection = this.getOpenConnection();
                this.command.Connection = this.connection;

                command.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            //}
        }

        public object ExecuteScalar(string storedProcedureName, SqlParameter[] parameters)
        {
            //try
            //{
                this.command = new SqlCommand(storedProcedureName);

                this.command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    for (int i = 0; i < parameters.Length; i++)
                        this.command.Parameters.Add(parameters[i]);

                if (this.connection == null)
                    this.connection = this.getOpenConnection();
                this.command.Connection = this.connection;

                return command.ExecuteScalar();
            //}
            //catch (Exception ex)
            //{
            //    Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            //}
            //return null;
        }

        public SqlDataReader GetSqlDataReader(string storedProcedureName, SqlParameter[] parameters)
        {
            //try
            //{
                //this.command = new SqlCommand("[dbo].[" + storedProcedureName + "]");
                this.command = new SqlCommand(storedProcedureName);
                this.command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    for (int i = 0; i < parameters.Length; i++)
                        this.command.Parameters.Add(parameters[i]);

                if (this.connection == null)
                    this.connection = this.getOpenConnection();
                this.command.Connection = this.connection;

                return command.ExecuteReader();
            //}
            //catch (Exception ex)
            //{
            //    Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            //}
            //return null;
        }

        public void Dispose()
        {
            //try
            //{
                if (this.command != null)
                    command.Dispose();
                if (this.connection != null)
                    connection.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            //}
        }

        private string connectionString;
        private SqlCommand command;
        private SqlConnection connection;

        public static DataSet RetrieveDataSet(string query, string tableName)
        {
            //try
            //{
                DataSet data = new DataSet();
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["Wedoq"].ConnectionString;
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(data, tableName);
                return data;
            //}
            //catch (Exception ex)
            //{
            //    Log.LogError(MethodInfo.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
            //}
            //return null;
        }
    }
}