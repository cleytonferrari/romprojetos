using System;
using System.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Driver;

namespace ROMProjetos.Repositorio
{
    public class Repositorio<T> where T : class
    {
        private readonly MongoDatabase db;
        private readonly MongoServer server;

        public Repositorio()
        {
            var connectionString = new MongoConnectionStringBuilder(Environment.GetEnvironmentVariable("MONGOLAB_URI"));
            //var connectionString = new MongoConnectionStringBuilder(ConfigurationManager.ConnectionStrings["ROMProjetos"].ConnectionString);

            var mongoSettings = new MongoClientSettings
                                    {
                                        Server = connectionString.Server,
                                        //DefaultCredentials = new MongoCredentials(connectionString.Username, connectionString.Password)
                                    };
            var mongoClient = new MongoClient(mongoSettings);
            
            server = mongoClient.GetServer();
            db = server.GetDatabase(connectionString.DatabaseName);
            Collection = db.GetCollection<T>(typeof(T).Name.ToLower());

            //corrige a hora no servidor do banco
            DateTimeSerializationOptions.Defaults = new DateTimeSerializationOptions(DateTimeKind.Local, BsonType.Document);

            var convensoes = new ConventionProfile();

            //Ajuda no migration, se tiver campo a mais no banco ele ignora
            convensoes.SetIgnoreExtraElementsConvention(new AlwaysIgnoreExtraElementsConvention());
            BsonClassMap.RegisterConventions(convensoes, (type) => true);
        }

        public MongoCollection<T> Collection { get; set; }
    }
}