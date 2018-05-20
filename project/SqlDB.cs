using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlToMongo
{
    // this as a parent class to all sql shit or no?
    class SqlDB : IDatabase
    {
        //should it be public or private and available only via constructor?
        public void Connect(string connectionString) {}
        public void ConnectToServer(string host, string port, string user, string password) {}
        public void ConnectToDatabase(string host, string port, string user, string password, string db) {}

        public void ExecuteQuery(string query) {}
        public void ExecuteNonQuery(string query) {}
    }
}
