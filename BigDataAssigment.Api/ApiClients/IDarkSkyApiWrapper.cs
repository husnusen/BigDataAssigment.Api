using System;
using System.Threading.Tasks;
using BigDataAssigment.Api.ApiClients.Models;

namespace BigDataAssigment.Api.ApiClients
{
    public interface IDarkSkyApiWrapper
    {
        Task<Forecast> GetDailyForecast(float lat, float lon);
    }
}
