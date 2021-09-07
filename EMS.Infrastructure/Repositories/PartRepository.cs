using System.Threading.Tasks;
using EMS.Domain.AggregatesModel.PartAggregate;
using Irvine.SeedWork.Domain;
using Microsoft.EntityFrameworkCore;
namespace EMS.Infrastructure.Repositories{
    public class PartRepository : IPartRepository
    {
        private readonly EmsContext _emsContext;
        public PartRepository(EmsContext emsContext)
        {
            _emsContext = emsContext;
        }
        public IUnitOfWork UnitOfWork => _emsContext;
        public Part Add(Part part)
        {
            return _emsContext.Parts.Add(part).Entity;
        }
        public Part Remove(Part part)
        {
            return _emsContext.Parts.Remove(part).Entity;
        }
        public async Task<Part> FindAsync(int id)
        {
            return await _emsContext.Parts.FindAsync(id);
        }
        public async Task<bool> IsExistByNameAsync(string name){
            return await _emsContext.Parts.AnyAsync(x=>x.Name ==name);
        }
    }
}