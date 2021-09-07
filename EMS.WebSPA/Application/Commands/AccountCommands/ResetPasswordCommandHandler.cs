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
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IUserProfileRepository _profileRepository;
        private readonly UserManager<UserProfile> _userManager;

        public ResetPasswordCommandHandler(IUserProfileRepository profileRepository, UserManager<UserProfile> userManager)
        {
            _profileRepository = profileRepository;
            _userManager = userManager;
        }
        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _profileRepository.FindAsync(request.UserIdentity);
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, code,"aA123456!");

            if (!result.Succeeded)
            {

                var errorBuilder = new StringBuilder();
                foreach (var error in result.Errors) errorBuilder.Append($"{error.Description}\n");
                throw new EMSDomainException(
                    errorBuilder.ToString(), new Exception("مشکل در ریست کردن پسورد"));
            }
            result = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            return result.Succeeded;
        }
    }
    public class ResetPasswordIdentifiedCommandHandler : IdentifierCommandHandler<ResetPasswordCommand, bool>
    {
        public ResetPasswordIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier) { }
        protected override bool CreateResultForDuplicateRequest()
        {
            return true;
        }
    }
}