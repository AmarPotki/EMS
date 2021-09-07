using System.Collections.Generic;
using System.Threading.Tasks;
using EMS.WebSPA.Application.Dtos;
namespace EMS.WebSPA.Application.Queries{
    public interface IFixUnitQuery{
        Task<IEnumerable<FixUnitDto>> GetFixUnits(string userIdentity);
        Task<bool> IsValid(string userGuid, int fixUnitId);
        Task<int> GetFaultsCountAsync(int fixUnitId);
        Task<IEnumerable<FaultDto>> GetFaultsAsync(int fixUnitId, int pageSize, int pageIndex);
        Task<IEnumerable<FixUnitDto>> GetFixUnits();
        Task<FixUnitDto> GetFixUnit(int fixUnitId);
        Task<IEnumerable<MemberDto>> GetMembers(IEnumerable<string> userGuids);
        Task<int> GetArchiveFaultsCountAsync(int fixUnitId);
        Task<IEnumerable<FaultDto>> GetArchiveFaultsAsync(int fixUnitId, int pageSize, int pageIndex);
    }
}