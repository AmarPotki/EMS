using System.Threading.Tasks;
using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.PartAggregate{
    public interface IPartRepository:IRepository<Part>{
        Part Add(Part part);
        Part Remove(Part part);
        Task<Part> FindAsync(int id);
        Task<bool> IsExistByNameAsync(string name);
    }
}