namespace SubAccount.Loader.Args
{
    using global::CommandLine;

    abstract class CommonSubOptions
    {
        [Option('v', "verbose", HelpText = "Display verbose output.")]
        public bool Verbose { get; set; }

        [Option('c', HelpText = "Config file path")]
        public string config { get; set; }
    }
}