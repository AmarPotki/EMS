using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMS.WebSPA.Application.Dtos;
namespace EMS.WebSPA.Application.Queries
{
    public interface IEmsQuery
    {
        Task<IEnumerable<FaultTypeDto>> GetFaultTypes();
        Task<FaultTypeDto> GetFaultType(int id);
        Task<IEnumerable<LocationDto>> GetLocationChildren(int parentId);
        Task<IEnumerable<ItemTypeDto>> GetItemTypeChildren(int parentId);
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDto> GetUser(string userIdentity);
        Task<bool> IsExistItemWithFaultType(int fautTypeId);
        Task<bool> IsExistItemWithPart(int partId);
        Task<IEnumerable<PartDto>> GetParts();
        Task<PartDto> GetPart(int id);

        Task<IEnumerable<UserDto>> SearchUser(string name);
        Task<bool> FaultsHasRecordWithSpecialLocationId(int locationId);
        Task<bool> FixUnitsHasRecordWithSpecialLocationId(int locationId);
        Task<bool> DoesLocationHasChild(int locationId);
        Task<IEnumerable<LocationDto>> GetAllLocations();
        Task<IEnumerable<ItemTypeDto>> GetAllItemTypes();
        Task<bool> FaultsHasRecordWithSpecialItemTypeId(int itemTypeId);
        Task<bool> FixUnitsHasRecordWithSpecialItemTypeId(int itemTypeId);

        Task<bool> IsExistItems(int fixUnitId);
        Task<bool> DoesItemTypeHasChild(int itemTypeId);
    }
}