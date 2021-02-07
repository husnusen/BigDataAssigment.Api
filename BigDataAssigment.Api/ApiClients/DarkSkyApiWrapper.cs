using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BigDataAssigment.Api.ApiClients.Models;
using BigDataAssigment.Api.Configuration;
using BigDataAssigment.Api.Exceptions;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace BigDataAssigment.Api.ApiClients
{
    public class DarkSkyApiWrapper:IDarkSkyApiWrapper
    {
        private readonly IConfigSettings _configSettings;
        private readonly ILoggerFactory  _loggerFactory;
        public DarkSkyApiWrapper(IConfigSettings configSettings,ILoggerFactory loggerFactory)
        {
            _configSettings = configSettings;
            _loggerFactory = loggerFactory;
        }

        public async Task<Forecast> GetDailyForecast(float lat, float lon)
        {
            var logger = _loggerFactory.CreateLogger("GetDailyForecast");

            try
            {
                logger.LogInformation($"lat:{lat}");
                logger.LogInformation($"lon:{lon}");
                
                var forecast = await _configSettings.DarkSkyUrl
                     .AppendPathSegment(_configSettings.DarkSkyKey)
                     .AppendPathSegment($"{lat},{lon}")
                     .SetQueryParam("lang", Constants.Constants.Lang)
                     .SetQueryParam("exclude", string.Join(',',Constants.Constants.ExcludedBlocks))
                     .GetJsonAsync<Forecast>()
                     .ConfigureAwait(false);

                return forecast;
            }
            catch (FlurlHttpException ex)
            {
                var response = await ex.GetResponseStringAsync().ConfigureAwait(false);
                var errorMessage = $"Error retrieving daily forecast(s) - ({(int)ex.Call.Response.StatusCode}): {response}";

                logger.LogError(errorMessage);

                throw (ex.Call.Response.StatusCode == (int)HttpStatusCode.BadRequest)
                    ? new BadRequestException(errorMessage)
                    : new Exception(errorMessage);
            }
        }
    }
}
