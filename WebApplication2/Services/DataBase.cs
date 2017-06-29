using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebApplication2.Services
{
    public class DataBase
    {        
        private static string ConnectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private static SqlConnection connection;
        private static SqlCommand command;

        public static SqlConnection GetConnection()
        {
            connection = new SqlConnection(ConnectionString);
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
            return connection;
        }

        public static void CloseConnection()
        {
            if (connection == null)
                return;
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public static SqlDataReader Get(string query, List<SqlParameter> Params)
        {
            command = new SqlCommand(query, GetConnection());
            if(Params != null)
            {
                foreach(SqlParameter parameterItem in Params)
                {
                    command.Parameters.Add(parameterItem);
                }
            }

            return command.ExecuteReader();
        }

        public static int Execute(string query, List<SqlParameter> Params)
        {
            command = new SqlCommand(query, GetConnection());
            if (Params != null)
            {
                foreach (SqlParameter parameterItem in Params)
                {
                    command.Parameters.Add(parameterItem);
                }
            }
            return command.ExecuteNonQuery();
        }
    }
}