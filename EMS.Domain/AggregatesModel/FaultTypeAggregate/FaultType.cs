using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.FaultTypeAggregate{
    public class FaultType: Entity, IAggregateRoot{
        public FaultType(){
            
        }
        public FaultType(string name){
            Name = name;
            _isArchive = false;
        }
        private bool _isArchive;
        public string Name { get;private set; }

        public void Update(string newName){
            Name = newName;
        }
        public void Archive(){
            _isArchive = true;
        }
    }
}