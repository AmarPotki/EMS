using System;
using System.Threading.Tasks;
using EMS.Domain.AggregatesModel.ItemTypeAggregate;
using EMS.Domain.AggregatesModel.LocationAggregate;
using Irvine.SeedWork.Domain;
using Microsoft.EntityFrameworkCore;
namespace EMS.Infrastructure.Repositories
{
    public class ItemTypeRepository : IItemTypeRepository
    {
        private readonly EmsContext _emsContext;
        public ItemTypeRepository(EmsContext emsContext)
        {
            _emsContext = emsContext;
        }
        public IUnitOfWork UnitOfWork => _emsContext;
        public ItemType Add(ItemType itemType)
        {
            return _emsContext.ItemTypes.Add(itemType).Entity;
        }
        public async Task<bool> IsValid(int id)
        {
            return await _emsContext.ItemTypes.AnyAsync(x => x.Id == id);
        }

        public ItemType Remove(ItemType itemType)
        {
            return _emsContext.ItemTypes.Remove(itemType).Entity;
        }

        public async Task<ItemType> FindAsync(int id)
        {
            return await _emsContext.ItemTypes.FindAsync(id);
        }
    }
}