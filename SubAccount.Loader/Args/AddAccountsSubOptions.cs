namespace SubAccount.Loader.Args
{
    using global::CommandLine;

    class AddAccountsSubOptions : CommonSubOptions
    {
        [Option("i", Required = true, HelpText = "")]
        public string Fid { get; set; }

        [Option("o", Required = true, HelpText = "")]
        public string Org { get; set; }

        [Option("u", Required = true, HelpText = "")]
        public string Url { get; set; }

        [Option("n", Required = true, HelpText = "")]
        public string Username { get; set; }

        [Option("p", Required = true, HelpText = "")]
        public string Password { get; set; }
    }
}