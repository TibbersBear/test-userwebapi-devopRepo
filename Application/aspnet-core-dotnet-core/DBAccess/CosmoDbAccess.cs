using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TestTechAwsLogin.Models;
using TestTechAwsLogin.Service;

namespace TestTechAwsLogin.DBAccess
{
    public class CosmoDbAccess : IDisposable
    {
        //private MongoServer mongoServer = null;
        private bool disposed = false;

        // To do: update the connection string with the DNS name
        // or IP address of your server. 
        //For example, "mongodb://testlinux.cloudapp.net
        private string userName = "test-userwebapi-database";
        private string host = "test-userwebapi-database.mongo.cosmos.azure.com";
        private string password = "kyRd66y0aTkBecbUGPGv6jBWjeGoJlLFSgEPzCEunLP2tFpLH4ycIk8h1NSUtPhYPDY1dsd1XTzwZ2SMCOll6w==";

        // This sample uses a database named "Tasks" and a 
        //collection named "TasksList".  The database and collection 
        //will be automatically created if they don't already exist.
        private string dbName = "DeepestFearDb";
        private string collectionName = "Users";
        private MongoClientSettings _settings;

        // Default constructor.        
        public CosmoDbAccess()
        {
            _settings = new MongoClientSettings();
            //string connectionString = @"mongodb://test-userwebapi-database:kyRd66y0aTkBecbUGPGv6jBWjeGoJlLFSgEPzCEunLP2tFpLH4ycIk8h1NSUtPhYPDY1dsd1XTzwZ2SMCOll6w==@test-userwebapi-database.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@test-userwebapi-database@";
            //_settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));

            _settings.Server = new MongoServerAddress(host, 10255);
            _settings.UseSsl = true;
            _settings.SslSettings = new SslSettings();
            _settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;
            _settings.RetryWrites = false;
            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            _settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);
        }

        // Gets all Task items from the MongoDB server.        
        public List<UserModel> GetAllUsers()
        {
            try
            {
                var collection = GetUsersCollection();
                return collection.Find(new BsonDocument()).ToList();
            }
            catch (MongoConnectionException e)
            {
                return new List<UserModel>();
            }
        }

        // Creates a Task and inserts it into the collection in MongoDB.
        public void CreateUser(UserModel task)
        {
            var collection = GetUsersCollection();
            collection.InsertOne(task);
        }

        public IMongoCollection<UserModel> GetUsersCollection()
        {
            MongoClient client = new MongoClient(_settings);
            var database = client.GetDatabase(dbName);
            return database.GetCollection<UserModel>(collectionName);
        }



        # region IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                _settings = null;
                if (disposing)
                {
                }
            }

            this.disposed = true;
        }

        # endregion
    }
}
