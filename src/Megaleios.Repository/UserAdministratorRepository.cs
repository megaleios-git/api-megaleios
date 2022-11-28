using Microsoft.AspNetCore.Hosting;
using Megaleios.Repository.Interface;
using Megaleios.Data.Entities;
using UtilityFramework.Infra.Core.MongoDb.Business;

namespace Megaleios.Repository
{
    public class UserAdministratorRepository : BusinessBaseAsync<UserAdministrator>, IUserAdministratorRepository
    {
        public UserAdministratorRepository(IHostingEnvironment env) : base(env)
        {
        }
    }
}