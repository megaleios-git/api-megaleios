using System.ComponentModel.DataAnnotations;

namespace Megaleios.Domain.ViewModels
{
    public class ProfileRegisterViewModel : ProfileViewModel
    {
        [Required(ErrorMessage = DefaultMessages.FieldRequired)]
        public string Password { get; set; }
        [Required(ErrorMessage = DefaultMessages.FieldRequired)]
        public string FacebookId { get; set; }
    }
}