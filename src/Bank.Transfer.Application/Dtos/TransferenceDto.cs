namespace Bank.TransferRequest.Application.Dtos
{
    public class TransferenceDto
    {
        public string AccountOrigin { get; set; }

        public string AccountDestination { get; set; }

        public decimal Amount { get; set; }

        
    }
}
