namespace SubAccount.Controllers
{
    using System.Web.Http;
    using Newtonsoft.Json.Linq;

    public class AccountController : ApiController
    {
        public IHttpActionResult GetAccounts()
        {
            return Json(new[] { new { Id = "BOFI"  } });
        }

        public IHttpActionResult GetAccount(string id)
        {
            return Json(new { Id = "BOFI", Goals = new[] { new { Name = "Bills", Remaining = 1234.56 }  } });
        }

        public IHttpActionResult PostTransactions([FromBody] JObject update)
        {
            return Ok();
        }
    }
}
