namespace SubAccount.Loader.Ofx.Models
{
    public class OfxResponse
    {
        public Signon.SignonMsgsRsV1 SignonMsgsRsV1 { get; set; }
        public Signup.SignupMsgsRsV1 SignupMsgsRsV1 { get; set; }
        public Banking.BankMsgsRsV1 BankMsgsRsV1 { get; set; }
    }
}