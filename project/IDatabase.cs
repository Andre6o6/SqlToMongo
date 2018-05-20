using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlToMongo
{
    interface IDatabase
    {
        void Connect(string connectionString);

        void ExecuteNonQuery(string query);
        //get data in collection as a collection of rows with type and value info
        //smth like DataTable ExecuteQuery(string query);

        //get data in collection entity by entity
        //smth like iEnumerable ExecuteReader(string)
    }
}
