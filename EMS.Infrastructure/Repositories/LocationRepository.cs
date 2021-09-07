using System;
using System.Threading.Tasks;
using EMS.Domain.AggregatesModel.LocationAggregate;
using Irvine.SeedWork.Domain;
using Microsoft.EntityFrameworkCore;
namespace EMS.Infrastructure.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly EmsContext _emsContext;
        public LocationRepository(EmsContext emsContext)
        {
            _emsContext = emsContext ?? throw new ArgumentNullException(nameof(emsContext));
        }
        public IUnitOfWork UnitOfWork => _emsContext;
        public Location Add(Location location)
        {
            return _emsContext.Locations.Add(location).Entity;
        }
        public async Task<bool> IsValid(int id)
        {
            return await _emsContext.Locations.AnyAsync(x => x.Id == id);
        }
        public async Task<Location> FindAsync(int id)
        {
            return await _emsContext.Locations.FindAsync(id);
        }

        public Location Remove(Location location)
        {
            return _emsContext.Locations.Remove(location).Entity;
        }

        public Location Update(Location location)
        {
            return _emsContext.Locations.Update(location).Entity;
        }

        public Task<bool> Disable(int id)
        {
            throw new NotImplementedException();
        }

    }

}

