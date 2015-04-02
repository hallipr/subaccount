namespace SubAccount.Loader.Ofx.Models.Signup
{
    using System;
    using System.Collections.Generic;

    public class AcctInfoRs
    {
        public DateTime DtAcctUp { get; set; }
        public List<AcctInfo> AcctInfo { get; set; }
    }
}