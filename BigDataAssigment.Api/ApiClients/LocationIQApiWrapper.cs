using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BigDataAssigment.Api.ApiClients.Models;
using BigDataAssigment.Api.Configuration;
using BigDataAssigment.Api.Exceptions;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System.Linq;
using BigDataAssigment.Api.Extensions;
using Microsoft.Extensions.Logging;

namespace BigDataAssigment.Api.ApiClients
{
    public class LocationIQApiWrapper:ILocationIQApiWrapper
    {
        private readonly IConfigSettings _configSettings;
        private readonly ILoggerFactory _loggerFactory;

        public LocationIQApiWrapper(IConfigSettings configSettings, ILoggerFactory loggerFactory)
        {
            _configSettings = configSettings;
            _loggerFactory = loggerFactory;
        }

        public async Task<Location> GetLocation(string location)
        {
            var logger = _loggerFactory.CreateLogger("GetLocationApiCall");
            try
            {
                logger.LogInformation($"location : {location}");

               var locations= await _configSettings.LocationIQUrl
                    .SetQueryParam("key",_configSettings.LocationIQKey )
                    .SetQueryParam("q",location)
                    .SetQueryParam("format", "json")
                    .GetJsonAsync<List<Location>>()
                    .ConfigureAwait(false);

                return locations.FirstOrDefault();

            }
            catch (FlurlHttpException ex)
            {
                var response = await ex.GetResponseStringAsync().ConfigureAwait(false);
                var errorMessage = $"Error retrieving location - ({(int)ex.Call.Response.StatusCode}): {response}";

                logger.LogError(errorMessage);

                throw (ex.Call.Response.StatusCode == (int)HttpStatusCode.BadRequest)
                    ? new BadRequestException(errorMessage)
                    : new Exception(errorMessage);
            }
        }
    }
}
