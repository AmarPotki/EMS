using System.Collections.Generic;
using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.FaultAggregate{
    public class Unit:ValueObject{
        public Unit(string fixUnitName, int fixUnitId){
            FixUnitName = fixUnitName;
            FixUnitId = fixUnitId;
        }
        public int FixUnitId { get;private set; }
        public string FixUnitName { get; private set; }
        protected override IEnumerable<object> GetAtomicValues(){
            yield return FixUnitId;
            yield return FixUnitName;
        }
    }
}