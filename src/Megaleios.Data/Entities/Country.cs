using MongoDB.Bson.Serialization.Attributes;
using UtilityFramework.Infra.Core.MongoDb.Data.Modelos;

namespace Megaleios.Data.Entities
{
    [BsonIgnoreExtraElements]
    public class Country : ModelBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Native { get; set; }
        public string Phone { get; set; }
        public string Capital { get; set; }
        public string Currency { get; set; }
        public string Flag { get; set; }

        public override string CollectionName => nameof(Country);
    }
}