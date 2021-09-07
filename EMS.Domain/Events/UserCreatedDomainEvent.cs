using MediatR;
namespace EMS.Domain.Events{
    public class UserCreatedDomainEvent : INotification
    {
        public string Email { get; private set; }
        public string Role { get; private set; }
        public int CompanyId { get; private set; }
        public UserCreatedDomainEvent(string email, string role, int companyId=0)
        {
            Email = email;
            Role = role;
            CompanyId = companyId;
        }
    }
}