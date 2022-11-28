using System.ComponentModel.DataAnnotations;
using UtilityFramework.Application.Core.ViewModels;
using UtilityFramework.Application.Core;
using Newtonsoft.Json;

namespace Megaleios.Domain.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        [Required(ErrorMessage = DefaultMessages.FieldRequired)]
        public string FullName { get; set; }
        [Required(ErrorMessage = DefaultMessages.FieldRequired)]
        public string Login { get; set; }

        [Required(ErrorMessage = DefaultMessages.FieldRequired)]
        [EmailAddress(ErrorMessage = DefaultMessages.EmailInvalid)]
        [JsonConverter(typeof(ToLowerCase))]
        public string Email { get; set; }
        [Required(ErrorMessage = DefaultMessages.FieldRequired)]

        public string Photo { get; set; }
        [Required(ErrorMessage = DefaultMessages.FieldRequired)]
        [IsValidCpf(ErrorMessage = DefaultMessages.CpfInvalid)]
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public bool Blocked { get; set; }
    }
}