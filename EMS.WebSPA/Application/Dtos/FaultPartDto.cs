namespace EMS.WebSPA.Application.Dtos{
    public class FaultPartDto
    {
        public FaultPartDto(){
            
        }
        public FaultPartDto(int partId, string partName, int count, int id){
            PartId = partId;
            PartName = partName;
            Count = count;
            Id = id;
        }
        public int PartId { get;private set; }
        public int Id { get;private set; }
        public string PartName { get; private set; }
        public int Count { get; private set; }
    }
}