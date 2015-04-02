namespace SubAccount.Controllers
{
    using System;
    using System.Web.Http;
    using SubAccount.Common;
    using SubAccount.Models;

    [RoutePrefix("goals")]
    public class GoalsController : ApiController
    {
        private readonly IDataStore dataStore;

        public GoalsController(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        [Route(Name = Routes.GetGoals)]
        public IHttpActionResult GetGoals()
        {
            return Ok(this.dataStore.GetGoals());
        }

        [Route("{id}", Name = Routes.GetGoal)]
        public IHttpActionResult GetGoal(Guid id)
        {
            var goal = this.dataStore.GetGoal(id);
            
            if (goal == null) 
                return NotFound();
            
            return Ok(goal);
        }

        [Route(Name = Routes.PostGoal)]
        public IHttpActionResult PostGoal(Goal goal)
        {
            goal.Id = Guid.NewGuid();

            this.dataStore.StoreGoal(goal);

            return Created(Routes.GetGoal, goal);
        }

        [Route("{id}", Name = Routes.PutGoal)]
        public IHttpActionResult PutGoal(Guid id, [FromBody]Goal goal)
        {
            goal.Id = id;
            this.dataStore.StoreGoal(goal);

            return Ok(goal);
        }
    }
}
