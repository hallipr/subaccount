namespace SubAccount.Loader.Ofx.Models.Signup
{
    public class AcctInfo
    {
        public string Desc { get; set; }
        public BankAcctInfo BankAcctInfo { get; set; }
        public BpAcctInfo BpAcctInfo { get; set; }
    }
}