using System;
using System.Threading.Tasks;
using BigDataAssigment.Api.ApiClients.Models;

namespace BigDataAssigment.Api.ApiClients
{
    public interface ILocationIQApiWrapper
    {
        Task<Location> GetLocation(string location);
    }
}
