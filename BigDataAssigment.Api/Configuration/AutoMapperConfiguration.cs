using System;
using System.Linq;
using AutoMapper;
using BigDataAssigment.Api.ApiClients.Models;
using BigDataAssigment.Api.Extensions;

namespace BigDataAssigment.Api.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static IMapper GetMapper() {
            var configuration = new MapperConfiguration(cfg => {

                cfg.CreateMap<Location, Entities.Location>();
            });

            return configuration.CreateMapper();
        }

        public static Entities.Forecast EnrichForeCastEntity(Forecast forecast){

            var maxTempWeekly = forecast.Daily.Data.Max(_ => _.TemperatureMax);
            var minTempWeekly = forecast.Daily.Data.Min(_ => _.TemperatureMax);

            return new Entities.Forecast {
                QueryDateTime = DateTime.UtcNow,
                CurrentTemperature = forecast.CurrentlyForecast.Temperature,
                MaxTemperatureWeekly = maxTempWeekly,
                MinTemperatureWeekly = minTempWeekly,
                Summary = forecast.Daily.Data[0].Summary,
                TodayMaxTemperature = forecast.Daily.Data[0].TemperatureMax,
                TodayMinTemperature = forecast.Daily.Data[0].TemperatureMin
            };


        }
    }
}
