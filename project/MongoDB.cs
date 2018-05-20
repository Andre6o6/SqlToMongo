using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SqlToMongo
{
    class MongoDB : IDatabase
    {
        MongoClient client;

        public void Connect(string connectionString)
        {
            client = new MongoClient(connectionString);
        }


        public void ExecuteNonQuery(string query)
        {}

    }
}
