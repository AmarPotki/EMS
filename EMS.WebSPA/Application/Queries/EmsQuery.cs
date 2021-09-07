using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using EMS.WebSPA.Application.Dtos;
using Microsoft.Extensions.Options;
namespace EMS.WebSPA.Application.Queries
{
    public class EmsQuery : IEmsQuery
    {
        private readonly string _connectionString = string.Empty;
        private readonly string _identityConnectionString = string.Empty;
        public EmsQuery(IOptions<AppSettings> settings)
        {
            _connectionString = !string.IsNullOrWhiteSpace(settings.Value.ConnectionString)
                ? settings.Value.ConnectionString
                : throw new ArgumentNullException(nameof(settings.Value.ConnectionString));
            _identityConnectionString = !string.IsNullOrWhiteSpace(settings.Value.IdentityConnectionString)
                ? settings.Value.IdentityConnectionString
                : throw new ArgumentNullException(nameof(settings.Value.IdentityConnectionString));
        }
        public async Task<IEnumerable<FaultTypeDto>> GetFaultTypes()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select id,name ,isArchive from [EMS].[ems].[faultTypes] ";
                return await connection.QueryAsync<FaultTypeDto>(query);
            }
        }
        public async Task<FaultTypeDto> GetFaultType(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select id,name ,isArchive from [EMS].[ems].[faultTypes] where id={id} ";
                return await connection.QueryFirstAsync<FaultTypeDto>(query);
            }
        }
        public async Task<IEnumerable<LocationDto>> GetLocationChildren(int parentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT loc.id, loc.ParentId, loc.Name,
	                           case when exists (select null from [EMS].[ems].[locations] u2 where u2.ParentId = loc.Id)
                               then 1
                               else 0
                               end as HasChildren
                               FROM [EMS].[ems].[locations] as loc
                               where ParentId ";
                query = parentId == 0 ? query + " is null" : query + "=" + parentId;
                return await connection.QueryAsync<LocationDto>(query);
            }
        }


        public async Task<IEnumerable<ItemTypeDto>> GetItemTypeChildren(int parentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT iType.id, iType.ParentId, iType.Name,
	                           case when exists (select null from [EMS].[ems].[itemTypes] u2 where u2.ParentId = iType.Id)
                               then 1
                               else 0
                               end as HasChildren
                               FROM [EMS].[ems].[itemTypes] as iType
                               where ParentId ";
                query = parentId == 0 ? query + " is null" : query + "=" + parentId;
                return await connection.QueryAsync<ItemTypeDto>(query);
            }
        }


        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            using (var connection = new SqlConnection(_identityConnectionString))
            {
                var query = $@"select id,name,username,role,LockoutEnabled from [EMSIdentityDB].[dbo].[AspNetUsers] ";
                return await connection.QueryAsync<UserDto>(query);
            }
        }
        public async Task<UserDto> GetUser(string userIdentity)
        {
            using (var connection = new SqlConnection(_identityConnectionString))
            {
                var query = $@"select id,name,username,role from [EMSIdentityDB].[dbo].[AspNetUsers] where id = '{userIdentity}'";
                return await connection.QueryFirstAsync<UserDto>(query);
            }
        }
        public async Task<bool> IsExistItemWithFaultType(int fautTypeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select count (id) from [EMS].[ems].[faults] where fautTypeId = {fautTypeId}";
                return await connection.QuerySingleAsync<int>(query) > 0;
            }
        }
        public async Task<bool> IsExistItemWithPart(int partId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select count(a.id) from [EMS].[ems].[faults] a join [EMS].[ems].[consumeParts] b on a.id = b.FaultId
where b.PartId=  {partId}";
                return await connection.QuerySingleAsync<int>(query) > 0;
            }
        }
        public async Task<IEnumerable<PartDto>> GetParts()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select id,name ,isArchive from [EMS].[ems].[parts] ";
                return await connection.QueryAsync<PartDto>(query);
            }
        }
        public async Task<PartDto> GetPart(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select id,name ,isArchive from [EMS].[ems].[parts] where id = {id} ";
                return await connection.QueryFirstAsync<PartDto>(query);
            }
        }

        public async Task<IEnumerable<UserDto>> SearchUser(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select id,name,username,role from [EMSIdentityDB].[dbo].[AspNetUsers] WHERE name LIKE '%{name}%'";
                var r = await connection.QueryAsync<UserDto>(query);
                return r;
            }
        }

        public async Task<bool> FaultsHasRecordWithSpecialLocationId(int locationId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select Count(Id) from [EMS].[faults] where LocationId";
                query = locationId == 0 ? query + " is null" : query + "=" + locationId;
                return await connection.QuerySingleAsync<int>(query) > 0;
            }
        }

        public async Task<bool> FixUnitsHasRecordWithSpecialLocationId(int locationId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select Count(Id) from [EMS].[fixUnits] where LocationId";
                query = locationId == 0 ? query + " is null" : query + "=" + locationId;
                return await connection.QuerySingleAsync<int>(query) > 0;
            }
        }

        public async Task<bool> DoesLocationHasChild(int locationId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select Count(Id) from [EMS].[locations] where ParentId";
                query = locationId == 0 ? query + " is null" : query + "=" + locationId;
                return await connection.QuerySingleAsync<int>(query) > 0;
            }
        }

        public async Task<IEnumerable<LocationDto>> GetAllLocations()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select Id,Name,ParentId,IsArchive from [EMS].[locations]";
                return await connection.QueryAsync<LocationDto>(query);
            }
        }

        public async Task<IEnumerable<ItemTypeDto>> GetAllItemTypes()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select Id,Name,ParentId,IsArchive from [EMS].[itemTypes]";
                return await connection.QueryAsync<ItemTypeDto>(query);
            }
        }

        public async Task<bool> FaultsHasRecordWithSpecialItemTypeId(int itemTypeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select Count(Id) from [EMS].[faults] where ItemTypeId";
                query = itemTypeId == 0 ? query + " is null" : query + "=" + itemTypeId;
                return await connection.QuerySingleAsync<int>(query) > 0;
            }
        }

        public async Task<bool> FixUnitsHasRecordWithSpecialItemTypeId(int itemTypeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select Count(Id) from [EMS].[fixUnits] where ItemTypeId";
                query = itemTypeId == 0 ? query + " is null" : query + "=" + itemTypeId;
                return await connection.QuerySingleAsync<int>(query) > 0;
            }
        }
        public async Task<bool> IsExistItems(int fixUnitId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select count(id) from [EMS].[ems].[faults] where FixUnitId=  {fixUnitId}";
                return await connection.QuerySingleAsync<int>(query) > 0;
            }
        }
        public async Task<bool> DoesItemTypeHasChild(int itemTypeId){
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select Count(Id) from [EMS].[itemTypes] where ParentId";
                query = itemTypeId == 0 ? query + " is null" : query + "=" + itemTypeId;
                return await connection.QuerySingleAsync<int>(query) > 0;
            }
        }
    }
}

