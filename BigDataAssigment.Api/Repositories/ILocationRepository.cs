using System;
using System.Linq;
using System.Threading.Tasks;
using BigDataAssigment.Api.Entities;

namespace BigDataAssigment.Api.Repositories
{
    public interface ILocationRepository
    {
        Task Save(Location location);

    }
}
