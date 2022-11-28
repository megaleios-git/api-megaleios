using MongoDB.Bson.Serialization.Attributes;

namespace Megaleios.Data.Entities
{
    [BsonIgnoreExtraElements]
    public class City : UtilityFramework.Infra.Core.MongoDb.Data.Modelos.City
    {
        public string StateUf { get; set; }
    }
}