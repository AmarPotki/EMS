using System;
using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.FixUnitAggregate{
    public class Member:Entity{
        public string Name { get;private set; }
        public string IdentityGuid { get; private set; }
        public Member(string identityGuid, string name){
            IdentityGuid = identityGuid;
            Name = name;
        }
    }
}