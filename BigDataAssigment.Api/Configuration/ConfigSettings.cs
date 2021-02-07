using System;
using Microsoft.Extensions.Configuration;

namespace BigDataAssigment.Api.Configuration
{
    public class ConfigSettings : IConfigSettings
    {
        private readonly IConfiguration _config;

        public ConfigSettings(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string LocationIQUrl => _config.GetValue<string>("LocationIQUrl");

        public string LocationIQKey => _config.GetValue<string>("LocationIQKey");

        public string DarkSkyUrl => _config.GetValue<string>("DarkSkyUrl");

        public string DarkSkyKey => _config.GetValue<string>("DarkSkyKey");
    }
}
