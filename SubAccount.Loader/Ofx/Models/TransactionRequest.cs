namespace SubAccount.Loader.Ofx.Models
{
    public class TransactionRequest
    {
        public string TrnUid { get; set; }
        public string Tan { get; set; }
        public string CltCookie { get; set; }
    }
}