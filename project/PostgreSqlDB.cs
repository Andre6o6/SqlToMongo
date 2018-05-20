using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace SqlToMongo
{
    class PostgreSqlDB : IDatabase
    {
        public NpgsqlConnection connection;

        //should it be public or private and available only via constructor?
        public void Connect(string connectionString)
        {
            try
            {
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
            }
            catch
            {
                connection = null;
                //throw smth
            }
        }

        public void ConnectToServer(string host="localhost", string port="5432", string user="postgres", string password="admin")
        {
            string connectionString = string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", host, port, user, password);
            Connect(connectionString);
        }

        public void ConnectToDatabase(string host="localhost", string port="5432", string user="postgres", string password="admin", string db="test")
        {
            string connectionString = string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", host, port, user, password, db);
            Connect(connectionString);
        }


        public void ExecuteNonQuery(string query)
        {
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            //catch exception
            command.ExecuteNonQuery();
        }

        public DataTable ExecuteQuery(string query)
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            //catch exception
            da.Fill(ds);
            return ds.Tables[0];
        }

    }
}
