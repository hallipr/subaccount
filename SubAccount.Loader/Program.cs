namespace SubAccount.Loader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using SubAccount.Common;
    using SubAccount.Loader.Args;
    using SubAccount.Loader.Ofx.Models;

    internal class Program
    {
        private static void Main(string[] args)
        {
            args = new[] { "download" };
            RunAsync(args).Wait();
        }

        private static async Task RunAsync(string[] args)
        {
            var options = new Options();
            var verb = string.Empty;

            if (!CommandLine.Parser.Default.ParseArguments(args, options, (s, o) => verb = s))
            {
                return;
            }

            if (verb == "add")
            {
                var config = LoadConfig(options.AddAccountsVerb.config);
                await AddNewAccountsAsync(config, options.AddAccountsVerb);
            }
            else
            {
                var config = LoadConfig(options.DownloadTransactionsVerb.config);
                await UpdateTransactionsAsync(config);
            }
        }

        private static async Task UpdateTransactionsAsync(Config config)
        {
            var downloader = new Downloader(config);

            var uploader = new Uploader(config);

            var documents = await downloader.ProcessAccountsAsync();

            var transactions = documents.SelectMany(TransactionSelector);

            await uploader.UploadTransactionsAsync(transactions);
        }

        private static IEnumerable<Transaction> TransactionSelector(OfxResponse response)
        {
            var statement = response.BankMsgsRsV1.StmtTrnRs.StmtRs;

            var bankId = int.Parse(statement.BankAcctFrom.BankId);
            var accountId = long.Parse(statement.BankAcctFrom.AcctId);

            return statement.BankTranList.StmtTrn
                .Select(x => new Transaction
                {
                    Id = CreateId(bankId, accountId, x.FitId),
                    CheckNumber = x.CheckNum,
                    Amount = x.TrnAmt,
                    Name = x.Name,
                    Memo = x.Memo,
                    DateAvailable = x.DtAvail,
                    DatePosted = x.DtPosted
                });
        }

        private static readonly Guid NamespaceId = new Guid("54DD46E6-848B-4F61-93D7-5DD4E13ACC33");
        private static Guid CreateId(int bankId, long accountId, string transactionId)
        {
            return GuidUtility.Create(NamespaceId, string.Format("{0}/{1}/{2}", bankId, accountId, transactionId));
        }

        private static async Task AddNewAccountsAsync(Config config, AddAccountsSubOptions subOptions)
        {
            var downloader = new Downloader(config);

            var accounts = await downloader.ListAccountsAsync(
                subOptions.Fid,
                subOptions.Org,
                subOptions.Url,
                subOptions.Username,
                subOptions.Password
                );

            var newAccounts = accounts.Where(x => !config.Accounts.Any(y => x.AccountId == y.AccountId && x.BankId == y.BankId));

            config.Accounts = config.Accounts.Union(newAccounts).ToArray();

            config.Save();
        }

        private static Config LoadConfig(string path)
        {
            var commandLineArg = Environment.GetCommandLineArgs()[0];
            var directoryName = Path.GetDirectoryName(commandLineArg);

            if (path == null)
            {
                var directory = new DirectoryInfo(directoryName);

                path = AncestorsAndSelf(directory)
                    .SelectMany(d => d.EnumerateFiles("accounts.json"))
                    .Select(x => x.FullName)
                    .FirstOrDefault();
            }

            return Config.Load(path);
        }

        private static IEnumerable<DirectoryInfo> AncestorsAndSelf(DirectoryInfo directory)
        {
            var current = directory;

            while (current != null)
            {
                yield return current;
                current = current.Parent;
            }
        }
    }
}