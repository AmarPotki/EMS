using System;
using System.Linq;
using System.Threading.Tasks;
using EMS.Domain.AggregatesModel.FaultAggregate;
using Irvine.SeedWork.Domain;
using Microsoft.EntityFrameworkCore;
namespace EMS.Infrastructure.Repositories{
    public class FaultRepository:IFaultRepository{
        private readonly EmsContext _emsContext;
        public IUnitOfWork UnitOfWork => _emsContext;
        public FaultRepository(EmsContext emsContext){
            _emsContext = emsContext;
        }
        public Fault Add(Fault fault){
            return _emsContext.Faults.Add(fault).Entity;
        }
        public async Task<Fault> FindAsync(int id){
            return await _emsContext.Faults.FindAsync(id);
        }
        public async Task<Fault> GetFaultWithFaultResultAndPart(int id){
            return await _emsContext.Faults.Include("FaultResults").Include("ConsumeParts").FirstOrDefaultAsync(x => x.Id == id);

        }
        public async Task<Fault> GetFaultWithFlowAndFaultResult(int id){
            return await _emsContext.Faults.Include("Flows").Include("FaultResults").FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Fault> GetFaultWithFaultResult(int id){
            return await _emsContext.Faults.Include("FaultResults").FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Fault> GetFaultWithPart(int id){
            return await _emsContext.Faults.Include("ConsumeParts").FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}