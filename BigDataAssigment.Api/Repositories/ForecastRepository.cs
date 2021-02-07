using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigDataAssigment.Api.Caching;
using BigDataAssigment.Api.DAL;
using BigDataAssigment.Api.Entities;
using BigDataAssigment.Api.Exceptions;
using BigDataAssigment.Api.Extensions;

namespace BigDataAssigment.Api.Repositories
{
    public class ForecastRepository:IForecastRepository
    { 
        private readonly ForecastDbContext _dbContext;
        private readonly IMemoryCacheService _memoryCacheService;

        public ForecastRepository(ForecastDbContext dbContext,
                                  IMemoryCacheService memoryCacheService)
        {
            _dbContext = dbContext;
            _memoryCacheService = memoryCacheService;
        }

        public async Task<IQueryable<Forecast>> GetByDate(DateTime dateTime)
        {
            return await Task.FromResult(_dbContext.Forecasts
                .Join(_dbContext.Locations,
                    forecast => forecast.LocationId,
                    location => location.PlaceId,
                    (forecast, location) =>  new { forecast, location })
                .Where(_ => _.forecast.QueryDateTime.Date == dateTime.Date)
                .Each(e=>e.forecast.Location = e.location).AsQueryable()
                .Select(s=>s.forecast));
        }

        public async Task<Forecast> GetByLocationName(string locationName)
        {
            var forecastFromMemoryCache = await _memoryCacheService.ReadFromCache<Forecast>(locationName.ToLower());
            if (forecastFromMemoryCache != null) return forecastFromMemoryCache;

            var location = _dbContext.Locations.Where(_ => _.LocationName == locationName.ToUpper()).SingleOrDefault();
            if (location == null) return null;

            var forecast = _dbContext.Forecasts.Where(_ => _.LocationId == location.PlaceId && _.QueryDateTime.Date == DateTime.Today.Date);

            if (!forecast.Any()) return null;

            await _memoryCacheService.SetCache(locationName.ToLower(), forecast.Single());

            return await Task.FromResult(forecast.Single());
        }

        public async Task Save(Forecast forecast,string locationName)
        {
            await _dbContext.AddAsync<Forecast>(forecast);
            _dbContext.SaveChanges();

            await _memoryCacheService.SetCache(locationName.ToLower(), forecast);
        }
    }
}
