using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.LocationAggregate{
    public class Location : Entity, IAggregateRoot{
        private bool _isArchive;
        private int? _parentId;
        public Location(string name, int? parentId){
            Name = name;
            _parentId = parentId;
        }
        public string Name { get; private set; }
        public Location Parent { get; private set; }
        public void Edit(string name){
            Name = name;
        }
        public void Disable(){
            _isArchive = true;
        }
        public void Enable(){
            _isArchive = false;
        }
    }
}