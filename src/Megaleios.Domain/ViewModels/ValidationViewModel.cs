using System.ComponentModel.DataAnnotations;
using UtilityFramework.Application.Core;
using Newtonsoft.Json;

namespace Megaleios.Domain.ViewModels
{
    public class ValidationViewModel
    {

        [Required(ErrorMessage = DefaultMessages.FieldRequired)]
        [EmailAddress(ErrorMessage = DefaultMessages.EmailInvalid)]
        [JsonConverter(typeof(ToLowerCase))]
        public string Email { get; set; }

        [Required(ErrorMessage = DefaultMessages.FieldRequired)]
        [IsValidCpf(ErrorMessage = DefaultMessages.CpfInvalid)]
        public string Cpf { get; set; }

        [Required(ErrorMessage = DefaultMessages.FieldRequired)]
        [IsValidCpf(ErrorMessage = DefaultMessages.CnpjInvalid)]

        public string Cnpj { get; set; }

        [Required(ErrorMessage = DefaultMessages.FieldRequired)]
        public string Login { get; set; }

    }
}