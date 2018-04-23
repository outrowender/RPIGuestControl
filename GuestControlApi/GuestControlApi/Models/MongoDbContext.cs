using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace GuestControlApi.Models
{
    public class MongoDbContext
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }
        private IMongoDatabase _database { get; }
        public MongoDbContext()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                if (IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o servidor.", ex);
            }
        }

        public IMongoDatabase GetDatabase => _database;
        public IMongoCollection<Pessoas> Pessoas => _database.GetCollection<Pessoas>("Pessoas");
        public IMongoCollection<Registros> Registros => _database.GetCollection<Registros>("Registros");
        public IMongoCollection<Departamentos> Departamentos => _database.GetCollection<Departamentos>("Departamentos");
        
    }
}
