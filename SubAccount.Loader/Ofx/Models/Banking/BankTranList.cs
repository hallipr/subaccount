namespace SubAccount.Loader.Ofx.Models.Banking
{
    using System;
    using System.Collections.Generic;

    public class BankTranList
    {
        public DateTime DtStart { get; set; }
        public DateTime DtEnd { get; set; }
        public List<StmtTrn> StmtTrn { get; set; }
    }
}