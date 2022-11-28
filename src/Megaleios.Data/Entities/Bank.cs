﻿using MongoDB.Bson.Serialization.Attributes;
using UtilityFramework.Infra.Core.MongoDb.Data.Modelos;

namespace Megaleios.Data.Entities
{
    [BsonIgnoreExtraElements]
    public class Bank : ModelBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string AccountMask { get; set; }
        public string AgencyMask { get; set; }

        public override string CollectionName => nameof(Bank);


    }
}