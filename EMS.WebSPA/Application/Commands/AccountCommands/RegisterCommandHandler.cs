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
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>{
        private readonly IUserProfileRepository _profileRepository;
        private readonly UserManager<UserProfile> _userManager;

        public RegisterCommandHandler(IUserProfileRepository profileRepository, UserManager<UserProfile> userManager){
            _profileRepository = profileRepository;
            _userManager = userManager;
        }
        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken){
            var userProfile = new UserProfile(request.UserName, request.Role, request.FullName);
            var result = await _userManager.CreateAsync(userProfile, request.Password);
            if (!result.Succeeded)
            {
                var errorBuilder = new StringBuilder();
                foreach (var error in result.Errors) errorBuilder.Append($"{error.Description}\n");
                throw new EMSDomainException(
                    errorBuilder.ToString(), new Exception("مشکل در ساخت یوزر"));
            }
             await _profileRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return result.Succeeded;
        }
    }
    public class RegisterIdentifiedCommandHandler : IdentifierCommandHandler<RegisterCommand, bool>{
        public RegisterIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager,
            Identifier identifier) : base(mediator, requestManager, identifier){ }
        protected override bool CreateResultForDuplicateRequest(){
            return true;
        }
    }
  
}