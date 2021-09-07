using System;
using System.Collections.Generic;
using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.FaultAggregate{
    public class Assign : ValueObject{
        public Assign(){
            
        }
        public Assign(string userId, string userDisplayName, DateTimeOffset time)
        {
            UserId = userId;
            UserDisplayName = userDisplayName;
            Time = time;
        }
 
        public string UserDisplayName { get;private set; }
        public string UserId { get;private set; }
        public DateTimeOffset? Time { get; set; }
        protected override IEnumerable<object> GetAtomicValues(){
            yield return UserDisplayName;
            yield return UserId;
            yield return Time;
        }
    }
}