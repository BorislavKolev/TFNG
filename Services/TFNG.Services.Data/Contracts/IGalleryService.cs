﻿namespace TFNG.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGalleryService
    {
        int GetCompetitionPicturesCount();

        IEnumerable<T> GetAllCompetition<T>(int? take = null, int skip = 0);

        int GetVacationPicturesCount();

        IEnumerable<T> GetAllVacation<T>(int? take = null, int skip = 0);
    }
}
