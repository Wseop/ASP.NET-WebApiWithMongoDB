using LoaManagerApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LoaManagerApi.Services
{
    public class AdminService
    {
        private readonly IMongoCollection<Admin> _adminCollection;

        public AdminService(IOptions<AdminDatabaseSettings> adminDatabaseSettings)
        {
            var mongoClient = new MongoClient(adminDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(adminDatabaseSettings.Value.DatabaseName);

            _adminCollection = mongoDatabase.GetCollection<Admin>(adminDatabaseSettings.Value.CollectionName);
        }

        public async Task<List<Admin>> GetAsync()
        {
            return await _adminCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Admin> GetAsync(int type)
        {
            return await _adminCollection.Find(x => x.Type == type).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Admin newAdmin)
        {
            await _adminCollection.InsertOneAsync(newAdmin);
        }

        public async Task UpdateAsync(int type, Admin updatedAdmin)
        {
            await _adminCollection.ReplaceOneAsync(x => x.Type == type, updatedAdmin);
        }

        public async Task RemoveAsync(int type)
        {
            await _adminCollection.DeleteOneAsync(x => x.Type == type);
        }
    }
}
