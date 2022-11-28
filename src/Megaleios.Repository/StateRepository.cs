using Microsoft.AspNetCore.Hosting;
using Megaleios.Repository.Interface;
using Megaleios.Data.Entities;
using UtilityFramework.Infra.Core.MongoDb.Business;

namespace Megaleios.Repository
{
    public class StateRepository : BusinessBaseAsync<Data.Entities.State>, IStateRepository
    {
        public StateRepository(IHostingEnvironment env) : base(env)
        {
        }
    }
}