namespace EMS.WebSPA.Application.Dtos{
    public class UserDto{
        public string Name { get; set; }
        public string Id { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public bool LockoutEnabled { get; set; }
    }
}