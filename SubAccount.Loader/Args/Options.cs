namespace SubAccount.Loader.Args
{
    using global::CommandLine;
    using global::CommandLine.Text;

    internal class Options
    {
        [VerbOption("add", HelpText = "Add accounts to config file.")]
        public AddAccountsSubOptions AddAccountsVerb { get; set; }

        [VerbOption("download", HelpText = "Download transactions.")]
        public DownloadTransactionsSubOptions DownloadTransactionsVerb { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this);
        }

        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }
    }
}