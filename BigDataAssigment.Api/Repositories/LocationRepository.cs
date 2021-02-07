using System;
using System.Linq;
using System.Threading.Tasks;
using BigDataAssigment.Api.DAL;
using BigDataAssigment.Api.Entities;

namespace BigDataAssigment.Api.Repositories
{
    public class LocationRepository:ILocationRepository
    {
        private readonly ForecastDbContext _dbContext;
        public LocationRepository(ForecastDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        public async Task Save(Location location)
        {
            if (_dbContext.Locations.Any(_ => _.PlaceId == location.PlaceId)) return;

            await _dbContext.AddAsync(location);
            _dbContext.SaveChanges();
        }
    }
}
