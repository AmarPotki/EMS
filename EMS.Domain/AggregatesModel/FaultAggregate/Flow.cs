using System;
using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.FaultAggregate{
    public class Flow:Entity{
        public Flow(){
            
        }
        public Flow(Unit @from, Unit to, DateTimeOffset time){
            From = @from;
            To = to;
            Time = time;
        }
        public Unit From { get;private set; }
        public Unit To { get;private set; }
        public DateTimeOffset Time { get;private set; }
    }
}