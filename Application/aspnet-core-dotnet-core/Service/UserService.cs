using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TestTechAwsLogin.DBAccess;
using TestTechAwsLogin.Models;

namespace TestTechAwsLogin.Service
{
    public class UserModelService
    {
        private readonly IMongoCollection<UserModel> _UserModels;
        private readonly CosmoDbAccess _dbAccess;

        public UserModelService(IDeepestFearDbSettings settings)
        {
            //Working with local MongoDB
            /*var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName); 
            _UserModels = database.GetCollection<UserModel>(settings.UserCollectionName);*/
            _dbAccess = new CosmoDbAccess();
            _UserModels = _dbAccess.GetUsersCollection();
        }

        public List<UserModel> Get() =>
            _UserModels.Find(UserModel => true).ToList();

        public UserModel Get(string id) =>
            _UserModels.Find<UserModel>(UserModel => UserModel.Id == id).FirstOrDefault();

        public UserModel Create(UserModel UserModel)
        {
            _UserModels.InsertOne(UserModel);
            return UserModel;
        }

        public void Update(string id, UserModel UserModelIn) =>
            _UserModels.ReplaceOne(UserModel => UserModel.Id == id, UserModelIn);

        public void Remove(UserModel UserModelIn) =>
            _UserModels.DeleteOne(UserModel => UserModel.Id == UserModelIn.Id);

        public void Remove(string id) =>
            _UserModels.DeleteOne(UserModel => UserModel.Id == id);
    }
}
