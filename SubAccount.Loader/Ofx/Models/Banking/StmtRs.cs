namespace SubAccount.Loader.Ofx.Models.Banking
{
    public class StmtRs
    {
        public string CurDef { get; set; }
        public BankAcctFrom BankAcctFrom { get; set; }
        public BankTranList BankTranList { get; set; }
        public Balance LedgerBal { get; set; }
        public Balance AvailBal { get; set; }
    }
}