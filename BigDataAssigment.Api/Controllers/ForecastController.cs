using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BigDataAssigment.Api.ApiClients;
using BigDataAssigment.Api.Caching;
using BigDataAssigment.Api.Configuration;
using BigDataAssigment.Api.Entities;
using BigDataAssigment.Api.Repositories;
using BigDataAssigment.Api.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BigDataAssigment.Api.Controllers
{
    [Route("api/[controller]")]
    public class ForecastController : Controller
    {
        private readonly ILocationIQApiWrapper _locationIQApiWrapper;
        private readonly IDarkSkyApiWrapper _darkSkyApiWrapper;
        private readonly ILocationRepository _locationRepository;
        private readonly IForecastRepository _forecastRepository;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IMapper _mapper;
        private readonly ForecastHub _forecastHub;

        public ForecastController(ILocationIQApiWrapper locationIQApiWrapper,
                                  IDarkSkyApiWrapper darkSkyApiWrapper,
                                  ILocationRepository locationRepository,
                                  IForecastRepository forecastRepository,
                                  ILoggerFactory loggerFactory,
                                  IMapper mapper,
                                  ForecastHub forecastHub

            )
        {
            _locationIQApiWrapper = locationIQApiWrapper;
            _darkSkyApiWrapper = darkSkyApiWrapper;
            _locationRepository = locationRepository;
            _forecastRepository = forecastRepository;
            _loggerFactory = loggerFactory;
            _mapper = mapper;
            _forecastHub = forecastHub;
        }

        
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            ILogger logger = _loggerFactory.CreateLogger("GetForecast");
            try
            {
                var forecastsEnquiriedToday = await _forecastRepository.GetByDate(DateTime.Today.AddDays(-1)).ConfigureAwait(false);
                return new ObjectResult(forecastsEnquiriedToday.ToList());
            }
            catch (Exception ex)
            {
                var message = $"Api returned with error for forecast queried today. ErrorMessage:{ex.Message}";
                logger.LogError(message);
                return new BadRequestObjectResult(message);
            }
        }

        [HttpGet("{locationName}")]
       // [ResponseCache(Duration = 60)]
        public async Task<IActionResult> Get(string locationName)
        {

            ILogger logger = _loggerFactory.CreateLogger("CheckAndPostForecast");
            try
            {

                logger.LogInformation($"location Name:{locationName}");
                 
                var forecastQueriedForLocationToday = await _forecastRepository.GetByLocationName(locationName).ConfigureAwait(false);
                if (forecastQueriedForLocationToday != null )
                {
                     await _forecastHub.SendMessage(forecastQueriedForLocationToday);
                    return new ObjectResult(forecastQueriedForLocationToday);
                }


                var locationDetails = await _locationIQApiWrapper.GetLocation(locationName).ConfigureAwait(false);
                if (locationDetails == null) return new NotFoundObjectResult($"Location {locationName.ToUpper()} is not found! ");


                var forecastDetails = await _darkSkyApiWrapper.GetDailyForecast(locationDetails.Lat, locationDetails.Lon).ConfigureAwait(false);
                if (forecastDetails == null ||
                    forecastDetails.Daily == null ||
                    !forecastDetails.Daily.Data.Any()) return new NotFoundObjectResult($"Daily Forecast for location {locationName.ToUpper()} is not found! ");

                var locationEntity = _mapper.Map<Entities.Location>(locationDetails);
                locationEntity.LocationName = locationName.ToUpper();
                await _locationRepository.Save(locationEntity).ConfigureAwait(false);


                var forecastEntity = AutoMapperConfiguration.EnrichForeCastEntity(forecastDetails);
                forecastEntity.LocationId = locationDetails.PlaceId;
                await _forecastRepository.Save(forecastEntity,locationName).ConfigureAwait(false);

                await _forecastHub.SendMessage(forecastEntity);

                return new ObjectResult(forecastEntity);
            }
            catch (Exception ex)
            {
                var message = $"Api returned with error for location {locationName.ToUpper()} forecast. ErrorMessage:{ex.Message}";
                logger.LogError(message);
                return new BadRequestObjectResult(message);
            }
        }

    }
}
