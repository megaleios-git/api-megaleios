using Newtonsoft.Json;

namespace Megaleios.Domain.ViewModels
{
    public class BankImportViewModel
    {
        [JsonProperty("Banco")]
        public string Name { get; set; }
        [JsonProperty("Agencia")]

        public string AgencyMask { get; set; }
        [JsonProperty("Conta")]

        public string AccountMask { get; set; }
        [JsonProperty("Code")]

        public string Code { get; set; }
    }
}