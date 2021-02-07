using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigDataAssigment.Api.Entities;

namespace BigDataAssigment.Api.Repositories
{
    public interface IForecastRepository
    {
        Task Save(Forecast forecast, string locationName);

        Task<IQueryable<Forecast>> GetByDate(DateTime dateTime);

        Task<Forecast> GetByLocationName(string locationName);

    }
}
