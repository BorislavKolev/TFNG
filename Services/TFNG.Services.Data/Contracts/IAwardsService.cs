namespace TFNG.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAwardsService
    {
        Task<int> CreateAsync(string name, string latinName, DateTime datetime, string location, string place, string imageUrl, string userId);

        Task<int> EditAsync(string name, string latinName, DateTime datetime, string location, string place, string imageUrl, string userId, int id);

        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

        int GetAwardsCount();

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id);

        Task DeleteByIdAsync(int id);
    }
}
