using System.Collections.Generic;
using Irvine.SeedWork.Domain;
using Microsoft.AspNetCore.Identity;
using MediatR;
using System.ComponentModel.DataAnnotations;
using EMS.Domain.Events;
namespace EMS.Domain.AggregatesModel.UserProfileAggregate{
    public class UserProfile : IdentityUser, IAggregateRoot
    {
        [Required]
        public string Name { get; private set; }
        public string Role { get; private set; }
        public string Tenant { get; private set; }



        #region Entity
        private List<INotification> _domainEvents;
        public List<INotification> DomainEvents => _domainEvents;
        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }
        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }
        #endregion

        protected UserProfile() { }

        
        public UserProfile(string email, string role, string name)
        {
            UserName = email;
            Email = email;
            Name = name;
            Role = role;
          //  AddDomainEvent(new UserCreatedDomainEvent(email, role));
        }
    }
}