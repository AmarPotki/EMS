using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.PartAggregate{
    public class Part:Entity,IAggregateRoot{
        public Part()
        {

        }
        public Part(string name)
        {
            Name = name;
            _isArchive = false;
        }
        private bool _isArchive;
        public string Name { get; private set; }

        public void Update(string newName)
        {
            Name = newName;
        }
        public void Archive()
        {
            _isArchive = true;
        }
    }
}