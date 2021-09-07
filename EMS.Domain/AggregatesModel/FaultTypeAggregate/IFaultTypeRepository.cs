using System.Threading.Tasks;
using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.FaultTypeAggregate{
    public interface IFaultTypeRepository:IRepository<FaultType>{
        FaultType Add(FaultType faultType);
        FaultType Remove(FaultType faultType);
        Task<FaultType> FindAsync(int id);
        Task<bool> IsExistByNameAsync(string name);
    }
}