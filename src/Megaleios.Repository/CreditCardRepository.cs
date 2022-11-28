using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using MongoDB.Driver;
using Megaleios.Repository.Interface;
using Megaleios.Data.Entities;
using UtilityFramework.Infra.Core.MongoDb.Business;

namespace Megaleios.Repository
{
    public class CreditCardRepository : BusinessBaseAsync<CreditCard>, ICreditCardRepository
    {
        public async Task<IEnumerable<CreditCard>> ListCreditCard(string profileId) => await FindByAsync(x => x.ProfileId == profileId, Builders<CreditCard>.Sort.Descending(x => x.Created));

        public CreditCardRepository(IHostingEnvironment env) : base(env)
        {
        }
    }
}