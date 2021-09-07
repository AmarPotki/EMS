using System.Collections.Generic;
using System.Threading.Tasks;
using EMS.WebSPA.Application.Dtos;
namespace EMS.WebSPA.Application.Queries{
    public interface IFaultQuery{
        Task<IEnumerable<FaultPartDto>> GetFaultsParts(int faultId);
        Task<bool> IsExistFaultResult(string userGuid);
        Task<IEnumerable<FaultDto>> MyFaultsAsync(string userGuid, int pageSize, int pageIndex);
        Task<int> MyFaultsCountAsync(string userGuid);

        Task<IEnumerable<FaultDto>> MyArchiveFaultsAsync(string userGuid, int pageSize, int pageIndex);
        Task<int> MyArchiveFaultsCountAsync(string userGuid);

    }
}