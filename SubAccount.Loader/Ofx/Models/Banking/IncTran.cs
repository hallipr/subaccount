namespace SubAccount.Loader.Ofx.Models.Banking
{
    using System;

    public class IncTran
    {
        public DateTime? DtStart { get; set; }
        public DateTime? DtEnd { get; set; }
        public bool Include { get; set; }
    }
}