using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Idempotency;
using EMS.Domain.AggregatesModel.UserProfileAggregate;
using EMS.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
namespace EMS.WebSPA.Application.Commands.AccountCommands
{
    public class LockOutCommandHandler : IRequestHandler<LockOutCommand, bool>
    {
        private readonly IUserProfileRepository _profileRepository;
        private readonly UserManager<UserProfile> _userManager;

        public LockOutCommandHandler(IUserProfileRepository profileRepository, UserManager<UserProfile> userManager)
        {
            _profileRepository = profileRepository;
            _userManager = userManager;
        }
        public async Task<bool> Handle(LockOutCommand request, CancellationToken cancellationToken){
            var user =await _profileRepository.FindAsync(request.UserIdentity);
            var result = await _userManager.SetLockoutEnabledAsync(user,true);


            if (!result.Succeeded)
            {

                var errorBuilder = new StringBuilder();
                foreach (var error in result.Errors) errorBuilder.Append($"{error.Description}\n");
                throw new EMSDomainException(
                    errorBuilder.ToString(), new Exception("مشکل در غیرفعال کردن یوزر"));
            }
            result = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
           // await _profileRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return result.Succeeded;
        }
    }
    public class LockOutIdentifiedCommandHandler : IdentifierCommandHandler<LockOutCommand, bool>
    {
        public LockOutIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier) { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true;
        }
    }


}