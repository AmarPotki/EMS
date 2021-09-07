using System.Threading.Tasks;
using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.ItemTypeAggregate{
    public interface IItemTypeRepository : IRepository<ItemType>{
        ItemType Add(ItemType itemType);
        Task<bool> IsValid(int id);
        ItemType Remove(ItemType itemType);
        Task<ItemType> FindAsync(int id);
    }
}