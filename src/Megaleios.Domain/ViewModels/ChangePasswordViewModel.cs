using System.ComponentModel.DataAnnotations;

namespace Megaleios.Domain.ViewModels
{
    public class ChangePasswordViewModel
    {

        [Required(ErrorMessage = DefaultMessages.FieldRequired)]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = DefaultMessages.FieldRequired)]
        public string NewPassword { get; set; }
    }
}