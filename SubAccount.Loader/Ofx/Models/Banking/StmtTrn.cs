namespace SubAccount.Loader.Ofx.Models.Banking
{
    using System;

    public class StmtTrn
    {
        public string TrnType { get; set; }
        public DateTime? DtPosted { get; set; }
        public DateTime? DtAvail { get; set; }
        public decimal TrnAmt { get; set; }
        public string FitId { get; set; }
        public string Name { get; set; }
        public string Memo { get; set; }
        public string CheckNum { get; set; }
    }
}