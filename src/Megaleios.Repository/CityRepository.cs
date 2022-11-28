using Microsoft.AspNetCore.Hosting;
using Megaleios.Repository.Interface;
using Megaleios.Data.Entities;
using UtilityFramework.Infra.Core.MongoDb.Business;

namespace Megaleios.Repository
{
    public class CityRepository : BusinessBaseAsync<Data.Entities.City>, ICityRepository
    {
        public CityRepository(IHostingEnvironment env) : base(env)
        {
        }
    }
}