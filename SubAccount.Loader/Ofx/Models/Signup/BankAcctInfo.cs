namespace SubAccount.Loader.Ofx.Models.Signup
{
    public class BankAcctInfo
    {
        public BankAcctFrom BankAcctFrom { get; set; }
        public bool SupTxDl { get; set; }
        public bool XferSrc { get; set; }
        public bool XferDest { get; set; }
        public string SvcStatus { get; set; }
    }
}