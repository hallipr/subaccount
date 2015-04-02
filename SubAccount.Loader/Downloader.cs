namespace SubAccount.Loader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using SubAccount.Loader.Ofx.Models;
    using SubAccount.Loader.Ofx.Models.Banking;
    using SubAccount.Loader.Ofx.Models.Signon;
    using SubAccount.Loader.Ofx.Models.Signup;
    using SubAccount.Loader.Ofx.Parsing;

    class Downloader
    {
        private const string AppVer = "1700";
        private const string AppId = "QWIN";
        private readonly Config config;

        public Downloader(Config config)
        {
            this.config = config;
        }

        public async Task<OfxResponse[]> ProcessAccountsAsync()
        {
            var results = new List<OfxResponse>();

            foreach (var accountConfig in config.Accounts)
            {
                results.Add(await DownloadAccountAsync(accountConfig));
            }

            return results.ToArray();
        }

        public async Task<AccountConfig[]> ListAccountsAsync(string fid, string org, string url, string username, string password)
        {
            var ofxRequest = CreateOfxRequest(fid, org, username, password);

            ofxRequest.SignupMsgsRqV1 = new SignupMsgsRqV1
            {
                AcctInfoTrnRq = new AcctInfoTrnRq
                {
                    TrnUid = Guid.NewGuid().ToString(),
                    AcctInfoRq = new AcctInfoRq()
                }
            };

            var ofxString = await GetResponseStringAsync(url, ofxRequest);

            var ofxResponse = OfxParser.Parse(ofxString);

            var acctInfos = ofxResponse.SignupMsgsRsV1.AcctInfoTrnRs.AcctInfoRs.AcctInfo;

            return acctInfos.Select(x => new AccountConfig
                {
                    Fid = fid,
                    FidOrg = org,
                    Url = url,

                    Username = username,
                    Password = password,

                    BankId = x.BankAcctInfo.BankAcctFrom.BankId,
                    AccountId = x.BankAcctInfo.BankAcctFrom.AcctId,
                    AccountType = x.BankAcctInfo.BankAcctFrom.AcctType
                })
                .ToArray();
        }


        private async Task<OfxResponse> DownloadAccountAsync(AccountConfig accountConfig)
        {
            var ofxRequest = CreateOfxRequest(accountConfig.Fid, accountConfig.FidOrg, accountConfig.Username, accountConfig.Password);

            ofxRequest.BankMsgsRqV1 = new BankMsgsRqV1
            {
                StmtTrnRq = new StmtTrnRq
                {
                    TrnUid = Guid.NewGuid().ToString(),
                    StmtRq = new StmtRq
                    {
                        BankAcctFrom = new BankAcctFrom
                        {
                            AcctId = accountConfig.AccountId,
                            AcctType = accountConfig.AccountType,
                            BankId = accountConfig.BankId
                        },
                        IncTran = new IncTran
                        {
                            DtStart = DateTime.Now.AddMonths(-1),
                            Include = true
                        }
                    }
                }
            };

            var ofxString = await GetResponseStringAsync(accountConfig.Url, ofxRequest);

            var ofxResponse = OfxParser.Parse(ofxString);

            return ofxResponse;
        }

        private static async Task<string> GetResponseStringAsync(string url, OfxRequest request)
        {
            var reqStr = Header() + request;

            var client = new HttpClient();

            var message = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(reqStr)
            };

            message.Headers.Add("User-Agent", "banking-js");
            message.Headers.Add("Accept", "application/ofx");

            var response = await client.SendAsync(message);

            var resStr = await response.Content.ReadAsStringAsync();
            return resStr;
        }

        private static string Header()
        {
            return "OFXHEADER:100\n" +
                   "DATA:OFXSGML\n" +
                   "VERSION:102\n" +
                   "SECURITY:NONE\n" +
                   "ENCODING:USASCII\n" +
                   "CHARSET:1252\n" +
                   "COMPRESSION:NONE\n" +
                   "OLDFILEUID:NONE\n" +
                   "NEWFILEUID:" + Guid.NewGuid().ToString("N").ToUpper() + "\n" +
                   "\n";
        }

        private static OfxRequest CreateOfxRequest(string fid, string org, string username, string password)
        {
            return new OfxRequest
            {
                SignonMsgsRqV1 = new SignonMsgsRqV1
                {
                    SonRq = new SonRq
                    {
                        UserId = username,
                        UserPass = password,
                        Language = "ENG",
                        DtClient = DateTime.Now,
                        AppId = AppId,
                        AppVer = AppVer,
                        Fi = new Fi {Fid = fid, Org = org}
                    }
                }
            };
        }
    }
}
