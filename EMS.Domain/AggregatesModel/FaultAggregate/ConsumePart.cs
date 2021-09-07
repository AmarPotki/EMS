using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.FaultAggregate{
    public class ConsumePart:Entity{
        public ConsumePart(string partName, int partId, int count){
            PartName = partName;
            PartId = partId;
            Count = count;
        }
        public string PartName { get;private set; } 
        public int PartId { get;private set; } 
        public int Count { get;private set; } 
    }
}