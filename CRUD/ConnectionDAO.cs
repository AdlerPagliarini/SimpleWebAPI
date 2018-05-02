using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SimpleWebApi.CRUD
{
    public class ConnectionDAO : IDisposable
    {
        private readonly SqlConnection myConnection;

        public ConnectionDAO()
        {
            myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            myConnection.Open();
        }

        public void ExecuteCommand(string query)//insert,update,delete
        {
            var cmdCommand = new SqlCommand
            {
                CommandText = query,
                CommandType = CommandType.Text,
                Connection = myConnection
            };
            cmdCommand.ExecuteNonQuery();
        }
        public void ExecuteCommand(string query, List<SqlParameter> SQLparam)//insert,update,delete, with param
        {
            var cmdCommand = new SqlCommand
            {
                CommandText = query,
                CommandType = CommandType.Text,
                Connection = myConnection
            };
            cmdCommand.Parameters.AddRange(SQLparam.ToArray());
            try
            {
                cmdCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //so uso no chat
        public SqlDataReader ReturnInsertedExecuteCommand(string query, List<SqlParameter> SQLparam)//insert,update,delete, with param
        {
            var cmdCommand = new SqlCommand
            {
                CommandText = query,
                CommandType = CommandType.Text,
                Connection = myConnection
            };
            cmdCommand.Parameters.AddRange(SQLparam.ToArray());
            return cmdCommand.ExecuteReader();
        }

        public SqlDataReader ExecuteReaderCommand(string query)//select
        {
            var cmdCommand = new SqlCommand(query, myConnection);
            return cmdCommand.ExecuteReader();
        }
        public SqlDataReader ExecuteReaderCommand(string query, List<SqlParameter> SQLparam)//select, with param
        {
            var cmdCommand = new SqlCommand(query, myConnection);
            cmdCommand.Parameters.AddRange(SQLparam.ToArray());
            return cmdCommand.ExecuteReader();
        }

        // para sempre fechar a conexão
        public void Dispose()
        {
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
        }
    }
}