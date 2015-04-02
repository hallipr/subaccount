namespace SubAccount.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using SubAccount.Common;
    using SubAccount.Models;

    [RoutePrefix("transactions")]
    public class TransactionsController : ApiController
    {
        private readonly IDataStore dataStore;

        public TransactionsController(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        [Route(Name = Routes.GetTransactions)]
        public IHttpActionResult GetTransactions()
        {
            return Ok(this.dataStore.GetTransactions());
        }

        [Route("{id}", Name = Routes.GetTransaction)]
        public IHttpActionResult GetTransaction(Guid id)
        {
            var transaction = this.dataStore.GetTransaction(id);

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [Route(Name = Routes.PostTransactions)]
        public IHttpActionResult PostTransactions([FromBody] IEnumerable<Transaction> transactions)
        {
            this.dataStore.StoreTransactions(transactions);
            return Ok();
        }

        [Route("{id}", Name = Routes.PutTransaction)]
        public IHttpActionResult PutTransaction(Guid id, [FromBody] Transaction transaction)
        {
            transaction.Id = id;
            this.dataStore.StoreTransactions(new [] { transaction });
            return Ok(transaction);
        }
    }
}
