using System;
using System.Collections.Generic;
using System.Linq;
using EMS.Domain.Events;
using EMS.Domain.Exceptions;
using Irvine.SeedWork.Domain;
namespace EMS.Domain.AggregatesModel.FaultAggregate{
    public class Fault : Entity, IAggregateRoot{
        private readonly List<FaultResult> _faultResults;
        private readonly List<ConsumePart> _consumeParts;
        private readonly List<Flow> _flows;
        private int _faultStatusId;
        private int _faultTypeId;
        private int _fixUnitId;
        private int _itemTypeId;
        private int _locationId;
        // identityguid
        private string _ownerId;
        public Fault(){
            _flows = _flows ?? new List<Flow>();
            _faultResults = _faultResults ?? new List<FaultResult>();
            _consumeParts = _consumeParts ?? new List<ConsumePart>();
        }
        public Fault(string title, string description, int faultTypeId, int itemTypeId, int locationId, string ownerId,
            int fixUnitId){
            _faultTypeId = faultTypeId;
            _itemTypeId = itemTypeId;
            _locationId = locationId;
            _ownerId = ownerId;
            _fixUnitId = fixUnitId;
            Title = title;
            Description = description;
            CreatedDate = DateTimeOffset.Now;
            _faultStatusId = FaultStatus.Submitted.Id;
            _flows = new List<Flow>();
            _faultResults = new List<FaultResult>();
            _consumeParts = new List<ConsumePart>();
            Assign = new Assign();
            AddDomainEvent(new FaultCreatedDomainEvent());
        }
        public int GetFixUnitId() => _fixUnitId;
        public string GetOwnerId() => _ownerId;
        public Assign Assign { get; private set; }
        public FaultStatus Faultstatus { get; private set; }
        public DateTimeOffset CreatedDate { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public IReadOnlyCollection<Flow> Flows => _flows;
        public IReadOnlyCollection<FaultResult> FaultResults => _faultResults;
        public IReadOnlyCollection<ConsumePart> ConsumeParts => _consumeParts;
        public void MoveToAnotherFixUnit(Unit from, Unit to, DateTimeOffset time, string description,string userGuid)
        {
            if (_faultStatusId == FaultStatus.Done.Id)
                throw new EMSDomainException("خرابی که رفع کامل شده را نمی توان جا به جا کرد");
            var faultResult = new FaultResult(description, userGuid, from.FixUnitId, true);
            _faultResults.Add(faultResult);
            var flow = new Flow(from, to, time);
            _flows.Add(flow);
            _fixUnitId = to.FixUnitId;
            _faultStatusId = FaultStatus.Moved.Id;
            ClearAssign();
        }
        private void ClearAssign(){
            Assign = new Assign("", "", DateTimeOffset.MinValue);
        }
        public void ChangeStatus(FaultStatus faultStatus){
            _faultStatusId = faultStatus.Id;
        }
        public void AssingFault(Assign assign){
            if(_faultStatusId==FaultStatus.Done.Id)
                throw new EMSDomainException("خرابی که رفع کامل شده را نمی توان واگذار کرد");
            Assign = assign;
            _faultStatusId = FaultStatus.Assigned.Id;
        }
        public void AddConsumePart(ConsumePart consumePart){
           if( _consumeParts.Any(x=>x.PartId==consumePart.PartId)) throw new EMSDomainException("قبلا این قطعه وارد شده است");
            _consumeParts.Add(consumePart);
        }
        public void AddFaultResult(string description, string userId, int fixUnitId){
            var faultResult = new FaultResult(description, userId, fixUnitId);
            _faultResults.Add(faultResult);
            _faultStatusId = FaultStatus.Doing.Id;
        }
        public void FixFault(string description, string userId, int fixUnitId){
            var faultResult = new FaultResult(description, userId, fixUnitId);
            _faultResults.Add(faultResult);
            _faultStatusId = FaultStatus.Done.Id;
        }
        public void DeletePart(int partId){
            var part = _consumeParts.FirstOrDefault(x => x.PartId == partId);
            if (part is null)  throw new EMSDomainException("قبلا این قطعه حذف شده است");
            _consumeParts.Remove(part);
        }
    }
}