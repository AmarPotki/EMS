using System.Threading.Tasks;
using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.FixUnitAggregate{
    public interface IFixUnitRepository : IRepository<FixUnit>
    {
        Task<FixUnit> GetFixUnit(int locationId, int itemTypeId);
        Task<bool> IsExistAsync(int locationId, int itemTypeId);
        FixUnit Add(FixUnit fixUnit);
        Task<FixUnit> FindAsync(int id);
        FixUnit Remove(FixUnit fixUnit);
        Task<FixUnit> GetFixUnitWithMembers(int id);
    }
}