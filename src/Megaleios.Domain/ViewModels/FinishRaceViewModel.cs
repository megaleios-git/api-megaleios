using UtilityFramework.Application.Core.ViewModels;

namespace Megaleios.Domain.ViewModels
{
    public class FinishRaceViewModel : BaseViewModel
    {
        public double FinalDistance { get; set; }
        public string RoutePath { get; set; }
    }
}