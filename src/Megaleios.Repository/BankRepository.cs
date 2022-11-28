using Microsoft.AspNetCore.Hosting;
using Megaleios.Repository.Interface;
using Megaleios.Data.Entities;
using UtilityFramework.Infra.Core.MongoDb.Business;

namespace Megaleios.Repository
{
    public class BankRepository : BusinessBaseAsync<Bank>, IBankRepository
    {
        public BankRepository(IHostingEnvironment env) : base(env)
        {
        }
    }
}