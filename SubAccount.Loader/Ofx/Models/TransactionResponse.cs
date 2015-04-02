namespace SubAccount.Loader.Ofx.Models
{
    public class TransactionResponse
    {
        public string TrnUid { get; set; }
        public Status Status { get; set; }
        public string CltCookie { get; set; }
    }
}
