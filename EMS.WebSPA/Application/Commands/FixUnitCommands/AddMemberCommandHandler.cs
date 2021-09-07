using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.FixUnitAggregate;
using EMS.Domain.AggregatesModel.UserProfileAggregate;
using Irvine.SeedWork.Domain;
using MediatR;
namespace EMS.WebSPA.Application.Commands.FixUnitCommands{
    public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, bool>
    {
        private readonly IFixUnitRepository _fixUnitRepository;
        private readonly IUserProfileRepository _profileRepository;
        public AddMemberCommandHandler(IFixUnitRepository fixUnitRepository, IUserProfileRepository profileRepository){
            _fixUnitRepository = fixUnitRepository;
            _profileRepository = profileRepository;
        }
        public async Task<bool> Handle(AddMemberCommand request, CancellationToken cancellationToken){
            var fixUnit = await _fixUnitRepository.GetFixUnitWithMembers(request.FixUnitId);
            if (fixUnit is null)
                throw new DomainException("شناسه نا معتبر است");
            var newMember = await _profileRepository.FindAsync(request.UserGuid);
            if (newMember is null)
                throw new DomainException("کاربر نا معتبر است");

            fixUnit.AddMember(newMember.Id, newMember.Name);
            return await _fixUnitRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
    public class AddMemberIdentifiedCommandHandler : IdentifierCommandHandler<AddMemberCommand, bool>
    {
        public AddMemberIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier) { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
}