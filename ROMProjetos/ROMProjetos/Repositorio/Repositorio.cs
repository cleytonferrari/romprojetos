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
        private readonly MongoDatabase _db;
        private readonly MongoServer _server;

        public Repositorio()
        {
            var conneccaoString = new MongoConnectionStringBuilder(ConfigurationManager.ConnectionStrings["ROMProjetos"].ConnectionString);
            _server = MongoServer.Create(conneccaoString);
            _db = _server.GetDatabase(conneccaoString.DatabaseName);
            Collection = _db.GetCollection<T>(typeof(T).Name.ToLower());

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