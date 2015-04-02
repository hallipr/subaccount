namespace SubAccount.Controllers
{
    using System.Collections.Generic;
    using SubAccount.Common;

    public interface IBucketStore
    {
        IEnumerable<Bucket> Query();
        void Store(Bucket bucket);
    }
}