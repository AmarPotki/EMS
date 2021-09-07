using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using BuildingBlocks.Infrastructure.Services.Identity;
using EMS.Domain.AggregatesModel.FaultAggregate;
using EMS.Domain.AggregatesModel.FixUnitAggregate;
using EMS.Domain.AggregatesModel.UserProfileAggregate;
using EMS.Domain.Exceptions;
using EMS.WebSPA.Application.Queries;
using MediatR;
namespace EMS.WebSPA.Application.Commands.FaultCommands{
    public class AssignCommandHandler : IRequestHandler<AssignCommand, bool>
    {
        private readonly IFaultRepository _faultRepository;
        private readonly IFixUnitRepository _fixUnitRepository;
        private readonly IIdentityService _identityService;
        private readonly IUserProfileRepository _profileRepository;
        private readonly IFixUnitQuery _fixUnitQuery;
        public AssignCommandHandler(IFixUnitRepository fixUnitRepository, IIdentityService identityService,
            IFaultRepository faultRepository, IUserProfileRepository profileRepository, IFixUnitQuery fixUnitQuery)
        {
            _fixUnitRepository = fixUnitRepository;
            _identityService = identityService;
            _faultRepository = faultRepository;
            _profileRepository = profileRepository;
            _fixUnitQuery = fixUnitQuery;
        }
        public async Task<bool> Handle(AssignCommand request, CancellationToken cancellationToken)
        {
            var userIdentity = _identityService.GetUserIdentity();
            var fault = await _faultRepository.FindAsync(request.FaultId);
            if (fault == null) throw new EMSDomainException("خرابی با این شناسه وجود ندارد");
            var fixUnit = await _fixUnitRepository.FindAsync(fault.GetFixUnitId());
            if (fixUnit.Owner != userIdentity) throw new EMSDomainException("شما دسترسی برای واگذاری این خرابی را ندارید");
            if(! await _fixUnitQuery.IsValid(request.UserId, fault.GetFixUnitId()))
                throw new EMSDomainException("خرابی را فقط به کاربرهای عضو این واحد می توان اختصاص داد");
            var user = await _profileRepository.FindAsync(request.UserId);
            fault.AssingFault(new Assign(request.UserId, user.Name, DateTimeOffset.Now));
            return await _faultRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class AssignFaultIdentifiedCommandHandler : IdentifierCommandHandler<AssignCommand, bool>
    {
        public AssignFaultIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier) { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
}