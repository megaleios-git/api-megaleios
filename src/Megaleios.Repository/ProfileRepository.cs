using Microsoft.AspNetCore.Hosting;
using Megaleios.Repository.Interface;
using Megaleios.Data.Entities;
using UtilityFramework.Infra.Core.MongoDb.Business;

namespace Megaleios.Repository
{
    public class ProfileRepository : BusinessBaseAsync<Profile>, IProfileRepository
    {
        public ProfileRepository(IHostingEnvironment env) : base(env)
        {
        }
    }
}