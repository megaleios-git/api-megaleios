using Megaleios.Data.Entities;
using Megaleios.Repository.Interface;
using Microsoft.AspNetCore.Hosting;
using UtilityFramework.Infra.Core.MongoDb.Business;

namespace Megaleios.Repository
{
    public class CountryRepository : BusinessBaseAsync<Country>, ICountryRepository
    {
        public CountryRepository(IHostingEnvironment env) : base(env)
        {
        }
    }
}