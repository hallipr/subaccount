namespace SubAccount.Controllers
{
    using System;
    using System.Web.Http;
    using SubAccount.Common;
    using SubAccount.Models;

    [RoutePrefix("categories")]
    public class CategoriesController : ApiController
    {
        private readonly IDataStore dataStore;

        public CategoriesController(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        [Route(Name = Routes.GetCetogies)]
        public IHttpActionResult GetCategories()
        {
            return Ok(this.dataStore.GetCategories());
        }

        [Route("{id}", Name = Routes.GetCategory)]
        public IHttpActionResult GetCategory(Guid id)
        {
            var category = this.dataStore.GetCategory(id);
            return Ok(category);
        }

        [Route(Name = Routes.PostCategory)]
        public IHttpActionResult PostCategory(Category category)
        {
            category.Id = Guid.NewGuid();
            this.dataStore.StoreCategory(category);
            return Created(Url.Route(Routes.GetCategory, new { category.Id }), category);
        }

        [Route("{id}", Name = Routes.PutCategory)]
        public IHttpActionResult PutCategory(Guid id, [FromBody]Category category)
        {
            category.Id = id;
            this.dataStore.StoreCategory(category);
            return Ok(category);
        }

    }
}