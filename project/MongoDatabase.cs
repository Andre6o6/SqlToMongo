using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SqlToMongo
{
    public class MongoDatabase : Database
    {
        private MongoClient client = null;
        private IMongoDatabase db = null;

        public MongoDatabase(string connectionString)
        {
            Connect(connectionString);
        }

        public MongoDatabase(string host="localhost", string port="27017", string user="", string password="", string db="")
        {
            ConnectToServer(host, port, user, password);
            if (db != "")
                ConnectToDatabase(db);
        }


        protected override void Connect(string connectionString)
        {
            client = new MongoClient(connectionString);
            //catch ex
        }

        private void ConnectToServer(string host, string port, string user, string password)
        {
            string connectionString;
            if (user == "" && password=="")
                connectionString = string.Format("mongodb://{0}:{1}", host, port);
            else
                connectionString = string.Format("mongodb://{0}:{1}@{2}:{3}",user,password,host,port);
            Connect(connectionString);
        }

        public void ConnectToDatabase(string db)
        {
            if (client == null)
                return;
            //catch ex
            this.db = client.GetDatabase(db);
        }


        public IMongoCollection<BsonDocument> GetCollection(string collection)
        {
            var col = db.GetCollection<BsonDocument>(collection);
            return col;
        }

        public override List<string> ListDatabases()
        {
            var databases = new List<string>();
            using (IAsyncCursor <BsonDocument> cursor = client.ListDatabases())
            {
                while (cursor.MoveNext())
                {
                    foreach (var doc in cursor.Current)
                    {
                        databases.Add(doc["name"].ToString());
                    }
                }
            }
            return databases;
        }

        public override List<string> ListTables()
        {
            if (db == null)     //throw smth
                return null;

            var collections = new List<string>();
            using (IAsyncCursor<BsonDocument> collectionCursor = db.ListCollections())
            {
                while (collectionCursor.MoveNext())
                {
                    foreach (var collDoc in collectionCursor.Current)
                    {
                        collections.Add(collDoc["name"].ToString());
                    }
                }
            }

            return collections;
        }

        public List<BsonDocument> ListDocuments(string collection)
        {
            var col = GetCollection(collection);
            var documents = col.Find(new BsonDocument()).ToList();
            return documents;
        }

        public List<string> ListDocumentsAsString(string collection)
        {
            var documents = AsString(ListDocuments(collection));   
            return documents;
        }


        public List<BsonDocument> Find(string collection, Dictionary<string,object> filter)
        {
            var col = GetCollection(collection);
            var filterDoc = new BsonDocument(filter);
            var documents = col.Find(filterDoc).ToList();
            return documents;
        }


        //do smth with this, make it extension or smth
        public List<string> AsString(List<BsonDocument> collection)
        {
            return collection.Select(x => x.ToString()).ToList();   //may be slow, idk
        }
    }
}
