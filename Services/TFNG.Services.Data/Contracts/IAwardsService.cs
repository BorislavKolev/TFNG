namespace TFNG.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface IAwardsService
    {
        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

        int GetAwardsCount();
    }
}
