using System.Threading.Tasks;
using EMS.Domain.AggregatesModel.FaultTypeAggregate;
using Irvine.SeedWork.Domain;
using Microsoft.EntityFrameworkCore;
namespace EMS.Infrastructure.Repositories{
    public class FaultTypeRepository:IFaultTypeRepository{
        private readonly EmsContext _emsContext;
        public FaultTypeRepository(EmsContext emsContext){
            _emsContext = emsContext;
        }
        public IUnitOfWork UnitOfWork => _emsContext;
        public FaultType Add(FaultType faultType){
            return _emsContext.FaultTypes.Add(faultType).Entity;
        }
        public FaultType Remove(FaultType faultType){
            return _emsContext.FaultTypes.Remove(faultType).Entity;
        }
        public async Task<FaultType> FindAsync(int id){
            return await _emsContext.FaultTypes.FindAsync(id);
        }
        public async Task<bool> IsExistByNameAsync(string name){
            return await _emsContext.FaultTypes.AnyAsync(x => x.Name == name);
        }
    }
}