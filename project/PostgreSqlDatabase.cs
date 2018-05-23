using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace SqlToMongo
{
    public class PostgreSqlDatabase : Database
    {
        private NpgsqlConnection connection = null;

        public PostgreSqlDatabase(string connectionString)
        {
            Connect(connectionString);
        }

        public PostgreSqlDatabase(string host="localhost", string port="5432", string user="postgres", string password="admin", string db="")
        {
            ConnectToServer(host, port, user, password);

            if (db != "")
                ConnectToDatabase(db);
        }


        protected override void Connect(string connectionString)
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

        private void ConnectToServer(string host, string port, string user, string password)
        {
            string connectionString = string.Format("Server={0};Port={1};User Id={2};Password={3};", host, port, user, password);
            Connect(connectionString);
        }

        public void ConnectToDatabase(string db)
        {
            if (connection == null)
                return;
            //catch ex
            connection.ChangeDatabase(db);
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

        public NpgsqlDataReader ExecuteQueryReader(string query)    //need some abstraction for readers
        {
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            NpgsqlDataReader dr = command.ExecuteReader();
            return dr;
        }
        
        public override List<string> ListDatabases()
        {
            //check if not connected to db

            var databases = new List<string>();

            var r = ExecuteQueryReader("select datname from pg_database where datistemplate = false");
            while (r.Read())
            {
                databases.Add(r.GetString(0));
            }
            r.Close();

            return databases;
        }

        public override List<string> ListTables()
        {
            var tables = new List<string>();

            var r = ExecuteQueryReader("select table_name from information_schema.tables where table_schema = 'public'");
            while (r.Read())
            {
                tables.Add(r.GetString(0));
            }
            r.Close();

            return tables;
        }


        public DataTable LoadTable(string table)
        {
            table = '"' + table + '"';
            return ExecuteQuery($"SELECT * FROM public.{table}");
        }
    }
}
