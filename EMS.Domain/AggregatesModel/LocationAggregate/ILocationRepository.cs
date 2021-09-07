using System.Threading.Tasks;
using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.LocationAggregate
{
    public interface ILocationRepository : IRepository<Location>
    {
        Location Add(Location location);
        Task<bool> IsValid(int id);
        Task<Location> FindAsync(int id);
        Location Remove(Location location);
    }
}