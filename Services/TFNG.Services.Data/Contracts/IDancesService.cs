namespace TFNG.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface IDancesService
    {
        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

        int GetDancesCount();
    }
}
