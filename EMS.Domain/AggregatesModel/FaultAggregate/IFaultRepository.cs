using System.Threading.Tasks;
using Irvine.SeedWork.Domain;
using MediatR;
namespace EMS.Domain.AggregatesModel.FaultAggregate{
    public interface IFaultRepository:IRepository<Fault>{
        Fault Add(Fault fault);
        Task<Fault> FindAsync(int id);
        Task<Fault> GetFaultWithFaultResultAndPart(int id);
        Task<Fault> GetFaultWithFlowAndFaultResult(int id);
        Task<Fault> GetFaultWithFaultResult(int id);
        Task<Fault> GetFaultWithPart(int id);
    }
}