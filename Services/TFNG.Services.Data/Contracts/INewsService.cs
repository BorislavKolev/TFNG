namespace TFNG.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INewsService
    {
        Task<int> CreateAsync(string title, string content, string userId, string imageUrl, string latinTitle, string author);

        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

        T GetByName<T>(string name);

        IEnumerable<T> GetLast<T>(int count);

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id);

        int GetNewsCount();

        Task<int> EditAsync(string title, string content, string userId, string imageUrl, string latinTitle, string author, int id);

        Task DeleteByIdAsync(int id);
    }
}
