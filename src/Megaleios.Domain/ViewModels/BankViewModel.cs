using UtilityFramework.Application.Core.ViewModels;

namespace Megaleios.Domain.ViewModels
{
    public class BankViewModel : BaseViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string AccountMask { get; set; }
        public string AgencyMask { get; set; }
    }
}