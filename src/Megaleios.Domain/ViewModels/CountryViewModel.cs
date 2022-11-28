using UtilityFramework.Application.Core.ViewModels;

namespace Megaleios.Domain.ViewModels
{
    public class CountryViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Flag { get; set; }
        public string Code { get; set; }
    }
}