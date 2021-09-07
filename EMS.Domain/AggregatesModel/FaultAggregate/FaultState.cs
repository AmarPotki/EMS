using System;
using System.Collections.Generic;
using System.Linq;
using EMS.Domain.Exceptions;
using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.FaultAggregate{
    public class FaultStatus:Enumeration{
        public static FaultStatus Submitted = new FaultStatus(1, nameof(Submitted).ToLowerInvariant());
        public static FaultStatus Assigned = new FaultStatus(2, nameof(Assigned).ToLowerInvariant());
        public static FaultStatus Doing = new FaultStatus(3, nameof(Doing).ToLowerInvariant());
        public static FaultStatus Moved = new FaultStatus(4, nameof(Moved).ToLowerInvariant());
        public static FaultStatus Done = new FaultStatus(5, nameof(Done).ToLowerInvariant());

        public FaultStatus(){
            
        }
        public FaultStatus(int id, string name)
            : base(id, name)
        {
        }
        public static IEnumerable<FaultStatus> List() =>
            new[] { Submitted, Assigned, Doing, Moved, Done };

        public static FaultStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new EMSDomainException($"Possible values for FaultState: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static FaultStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new EMSDomainException($"Possible values for OrderStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}