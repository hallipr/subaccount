namespace SubAccount.Loader.Ofx.Models.Signon
{
    using System;

    public class SonRq
    {
        public DateTime DtClient { get; set; }
        public string UserId { get; set; }
        public string UserPass { get; set; }
        public string Language { get; set; }
        public Fi Fi { get; set; }
        public string AppId { get; set; }
        public string AppVer { get; set; }
    }
}