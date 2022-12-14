using Infrastructuur.Dto;
using Infrastructuur.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using Thrift.Protocols.Entities;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Infrastructuur.Database.Mongo
{

    public class MongoDbContext
    {
        private MongoClient dbClient = new MongoClient("mongodb://localhost:27017/SeatleTeams");
        //team
        public async Task<T> GetByIdAsync<T>(Func<T, bool> predicate, string collection) where T : class
        {
            var data = (await GetAllAsync<T>(collection)).FirstOrDefault(predicate);
            return data;
        }
        public async Task<List<TeamEntity>> GetAllTeamsFromUser(int userId)
        {
            // get the database
            var userTeams = new List<UserTeamEntity>();
            var teamsToPush = new List<TeamEntity>();
            var database = dbClient.GetDatabase("SeatleTeams");
            // get the collection in that database
            var collection = database.GetCollection<BsonDocument>("UserTeam");
            // change one onbject for objectid replacer otherwise error in code
            var json = (await collection.FindAsync(new BsonDocument())).ToList();
            foreach (var item in json)
            {
                userTeams.Add(JsonConvert.DeserializeObject<UserTeamEntity>(item
                    .ToString()
                    .Replace("ObjectId(", "")
                    .Replace(")", "")));
            }

            var teams = await GetAllAsync<TeamEntity>("Team");
            var userIds = userTeams.Where(x => x.UserId == userId).Select(x => x.TeamId).ToList();
            teamsToPush.AddRange(teams.Where(team => userIds.Contains(team.Id)));
            return teamsToPush;
        }
        public async Task<bool> DeleteAsync<T>(int id,string collectionData, Func<T,bool> pred) where T : class
        {
            var database = dbClient.GetDatabase("SeatleTeams");

            // get the collection in that database
            var collection = database.GetCollection<BsonDocument>(collectionData);
          
            // make error if this document(employee) does not exist

            // filter based on the document with employee id equaling employee.IdNumber.
            var filter = Builders<BsonDocument>.Filter.Eq("id", id);
            //delete
            var data = (await GetAllAsync<T>(collectionData)).FirstOrDefault(pred);
            if (data is null) {
                return false;
            }
            collection.DeleteOne(filter);
            return true;
        }
        // generic
        public async Task<IQueryable<T>> GetAllAsync<T>(string collection) where T : class
        {
            var database = dbClient.GetDatabase("SeatleTeams");
            var listOb = new List<T>();
            var collectionTeams = database.GetCollection<BsonDocument>(collection);
            // change one onbject for objectid replacer otherwise error in code
            var jsonTeams = (await collectionTeams.FindAsync(new BsonDocument())).ToList();
            foreach (var item in jsonTeams)
            {
                listOb.Add(JsonConvert.DeserializeObject<T>(item
                    .ToString()
                    .Replace("ObjectId(", "")
                    .Replace(")", "")));
            }
            return listOb.AsQueryable();
        }

        // create 
        public async Task<T> CreateAsync<T>(T toAdd, string collectionData,  BsonDocument bsonDocument) where T : class
        {
            var database = dbClient.GetDatabase("SeatleTeams");
         
            // get the collection in that database
            var collection = database.GetCollection<BsonDocument>(collectionData);
            var serialize = JsonConvert.SerializeObject(toAdd);

            await collection.InsertOneAsync(bsonDocument);

            return toAdd;
        }

        // update
        public async Task<bool> UpdateAsync<T>(int id,string fieldtoUpdate,string valueToUpdate,string collectionData, BsonDocument bsonDocument) where T : class
        {
            var database = dbClient.GetDatabase("SeatleTeams");

            // get the collection in that database
            var collection = database.GetCollection<BsonDocument>(collectionData);
          

            var document = bsonDocument;
            // filter based on the document with employee id equaling employee.IdNumber.
            var filter = Builders<BsonDocument>.Filter.Eq("id", id);

            // to update
          
            var updateName = Builders<BsonDocument>.Update.Set(fieldtoUpdate, valueToUpdate);

            collection.UpdateOne(filter, updateName);


            return true;
        }
    }
}
