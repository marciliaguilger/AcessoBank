using Bank.Transfer.Domain.Enums;
using Newtonsoft.Json;

namespace Bank.TransferRequest.Application.Dtos
{
    public class RequestStatusDto
    {
        public string Status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

    }
}
