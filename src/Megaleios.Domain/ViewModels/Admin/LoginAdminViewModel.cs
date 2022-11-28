using System.ComponentModel.DataAnnotations;
using UtilityFramework.Application.Core;
using Newtonsoft.Json;

namespace Megaleios.Domain.ViewModels.Admin
{
    public class LoginAdminViewModel
    {
        [Required(ErrorMessage = DefaultMessages.FieldRequired)]
        public string Login { get; set; }
        [Required(ErrorMessage = DefaultMessages.FieldRequired)]

        public string Password { get; set; }
        [Required(ErrorMessage = DefaultMessages.FieldRequired)]
        [EmailAddress(ErrorMessage = DefaultMessages.EmailInvalid)]
        [JsonConverter(typeof(ToLowerCase))]

        public string Email { get; set; }
    }
}