using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlToMongo
{
    public abstract class Database
    {
        protected abstract void Connect(string connectionString);

        //public abstract void ExecuteNonQuery(string query);

        //get data in collection as a collection of rows with type and value info
        //smth like DataTable ExecuteQuery(string query);

        //get data in collection entity by entity
        //smth like iEnumerable ExecuteReader(string)

        public abstract List<string> ListDatabases();   //return the list of all dbs on server
        public abstract List<string> ListTables();      //return the list of all tables/collections in db

    }
}
