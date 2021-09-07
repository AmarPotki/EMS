using System.Threading.Tasks;
using EMS.Domain.AggregatesModel.FixUnitAggregate;
using Irvine.SeedWork.Domain;
using Microsoft.EntityFrameworkCore;
namespace EMS.Infrastructure.Repositories{
    public class FixUnitRepository:IFixUnitRepository{
        private readonly EmsContext _emsContext;
        public FixUnitRepository(EmsContext emsContext){
            _emsContext = emsContext;
        }
        public IUnitOfWork UnitOfWork => _emsContext;
        public async Task<bool> IsExistAsync(int locationId, int itemTypeId){
          return  await _emsContext.FixUnits.AnyAsync(x => x.LocationId == locationId && x.ItemTypeId == itemTypeId);

        }
        public async Task<FixUnit> GetFixUnit(int locationId, int itemTypeId){
            return await _emsContext.FixUnits.FirstOrDefaultAsync(x => x.LocationId == locationId && x.ItemTypeId == itemTypeId);

        }
        public FixUnit Add(FixUnit fixUnit){
            return _emsContext.FixUnits.Add(fixUnit).Entity;
        }
        public async Task<FixUnit> FindAsync(int id){
            return await _emsContext.FixUnits.FindAsync(id);
        }

        public  FixUnit Remove(FixUnit fixUnit)
        {
            return  _emsContext.FixUnits.Remove(fixUnit).Entity;
        }
        public async Task<FixUnit> GetFixUnitWithMembers(int id){
            return await _emsContext.FixUnits.Include("Members").FirstOrDefaultAsync(x => x.Id==id);
        }
    }
}
 