using MHA.Data.Contracts;
using MHA.Models.Models;
using MHA.Tools.Attributes;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Data.Context
{
    public class ClientInstancer : IClientInstancer
    {
        private readonly string _connectionString;
        private readonly IConfiguration configuration;
        private readonly ConventionPack _conventionPack;
        private static IMongoClient dbClient;

        public ClientInstancer(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", _conventionPack, t => true);

            dbClient = new MongoClient(_connectionString);
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>() where TDocument : ModelBase
        {
            var db = dbClient.GetDatabase(GetDatabase());
            var collection = db.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
            return collection;
        }

        private string GetDatabase()
        {
            var database = this.configuration.GetSection("MyMongoSettings").GetSection("database");
            return database.Value;
        }

        private string GetCollectionName(Type docType)
        {
            return ((CollectionNameAttribute)docType.GetCustomAttributes(typeof(CollectionNameAttribute), true).FirstOrDefault())?.CollectionName;
        }
    }
}
