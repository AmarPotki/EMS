namespace BuildingBlocks.Infrastructure.Services.Identity
{
    public interface IIdentityService
    {
        string GetUserIdentity();
        string GetUserName();
        string DisplayName();
    }

}