using System;
using Irvine.SeedWork.Domain;
using System.Collections.Generic;
using System.Linq;
using EMS.Domain.Exceptions;
namespace EMS.Domain.AggregatesModel.FixUnitAggregate
{
    public class FixUnit : Entity, IAggregateRoot
    {
        public FixUnit(string description, string title, int itemTypeId, int locationId, string owner)
        {
            Description = description;
            Title = title;
            ItemTypeId = itemTypeId;
            LocationId = locationId;
            Owner = owner;
            _members = new List<Member>();
        }
        public int ItemTypeId { get; private set; }
        public string Owner { get; private set; }
        public int LocationId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        private readonly List<Member> _members;
        public IReadOnlyCollection<Member> Members => _members;

        public void AddMember(string identityGuid,string name)
        {
            if (_members.Any(x => x.IdentityGuid == identityGuid)) throw new  EMSDomainException("کاربر تکراری سات");
                var member = new Member(identityGuid,name);
            _members.Add(member);
        }

        public void Edit(string description, string title, int itemTypeId, int locationId, string owner)
        {
            Description = description;
            Title = title;
            ItemTypeId = itemTypeId;
            LocationId = locationId;
            Owner = owner;
        }
        public void RemoveMember(string userGuid){
            var member = _members.FirstOrDefault(x => x.IdentityGuid == userGuid);
            if (member is null) throw new EMSDomainException("کاربر معتبر نیست");
            _members.Remove(member);
        }
    }
}
