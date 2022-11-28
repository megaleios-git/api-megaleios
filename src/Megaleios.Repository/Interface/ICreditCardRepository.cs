using System.Collections.Generic;
using System.Threading.Tasks;
using UtilityFramework.Infra.Core.MongoDb.Business;
using Megaleios.Data.Entities;

namespace Megaleios.Repository.Interface
{
    public interface ICreditCardRepository : IBusinessBaseAsync<CreditCard>
    {
        Task<IEnumerable<CreditCard>> ListCreditCard(string profileId);
    }
}