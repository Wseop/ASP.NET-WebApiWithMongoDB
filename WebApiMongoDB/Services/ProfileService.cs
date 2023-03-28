using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApiMongoDB.Models;

namespace WebApiMongoDB.Services
{
    public class ProfileService
    {
        private readonly IMongoCollection<Profile> _profileCollection;

        public ProfileService(IOptions<ProfileDatabaseSettings> profileDatabaseSettings)
        {
            var mongoClient = new MongoClient(profileDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(profileDatabaseSettings.Value.DatabaseName);

            _profileCollection = mongoDatabase.GetCollection<Profile>(profileDatabaseSettings.Value.CollectionName);
        }

        public async Task<List<Profile>> GetAsync()
        {
            return await _profileCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Profile> GetAsync(string name)
        {
            return await _profileCollection.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Profile newProfile)
        {
            await _profileCollection.InsertOneAsync(newProfile);
        }

        public async Task UpdateAsync(string name, Profile updatedProfile)
        {
            await _profileCollection.ReplaceOneAsync(x => x.Name == name, updatedProfile);
        }

        public async Task RemoveAsync(string name)
        {
            await _profileCollection.DeleteOneAsync(x => x.Name == name);
        }
    }
}
