using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.ItemTypeAggregate{
    //  faultType
    public class ItemType : Entity,IAggregateRoot{
        private int? _parentId;
        private bool _isArchive;

        public ItemType(string name, int? parentId)
        {
            Name = name;
            _parentId = parentId;
        }
        public string Name { get; private set; }
        public ItemType Parent { get; private set; }
        public void Enable(){
            _isArchive = false;
        }
        public void Disable()
        {
            _isArchive = true;
        }
    }
}