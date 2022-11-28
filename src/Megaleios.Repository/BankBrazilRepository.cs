using Megaleios.Data.Entities;
using Megaleios.Repository.Interface;
using Microsoft.AspNetCore.Hosting;
using UtilityFramework.Infra.Core.MongoDb.Business;

namespace Megaleios.Repository
{
    public class BankBrazilRepository : BusinessBaseAsync<BankBrazil>, IBankBrazilRepository
    {
        public BankBrazilRepository(IHostingEnvironment env) : base(env)
        {
        }
    }
}