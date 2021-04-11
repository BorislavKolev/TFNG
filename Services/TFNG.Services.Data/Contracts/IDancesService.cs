namespace TFNG.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDancesService
    {
        Task<int> CreateAsync(string name, string description, string userId, string imageUrl, string latinName, string folkloreArea, string videoUrl);

        Task<int> EditAsync(string name, string description, string userId, string imageUrl, string latinName, string folkloreArea, string videoUrl, int id);

        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id);

        T GetByName<T>(string name);

        int GetDancesCount();

        Task DeleteByIdAsync(int id);
    }
}
