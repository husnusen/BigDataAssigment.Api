using System;
namespace BigDataAssigment.Api.Configuration
{
    public interface IConfigSettings
    {
        string LocationIQUrl { get; }
        string LocationIQKey { get; }
        string DarkSkyUrl { get; }
        string DarkSkyKey { get; }
    }
}
