namespace EMS.WebSPA.Application.Dtos{
    public class ItemTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public bool HasChildren { get; set; }
    }
}
