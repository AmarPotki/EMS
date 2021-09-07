using System;
using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.FaultAggregate{
    public class FaultResult:Entity{
        public FaultResult()
        {

        }
        public FaultResult(string description,string userId,int fixUnitId, bool isMoveTo=false){
            Description = description;
            UserId = userId;
            IsMoveTo = isMoveTo;
            Time = DateTimeOffset.Now;
            FixUnitId = fixUnitId;
        }
        //move to or normal
        public bool IsMoveTo { get;private set; }
        public string Description { get; private set; }
        public DateTimeOffset Time { get;private set; }
        public string UserId { get;private set; }
        public int FixUnitId { get; private set; }
    }
}