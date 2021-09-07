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
    public class FixUnitQuery : IFixUnitQuery
    {
        private readonly string _connectionString = string.Empty;
        private readonly string _identityConnectionString = string.Empty;
        public FixUnitQuery(IOptions<AppSettings> settings)
        {
            _connectionString = !string.IsNullOrWhiteSpace(settings.Value.ConnectionString)
                ? settings.Value.ConnectionString
                : throw new ArgumentNullException(nameof(settings.Value.ConnectionString));
            _identityConnectionString = !string.IsNullOrWhiteSpace(settings.Value.IdentityConnectionString)
                ? settings.Value.IdentityConnectionString
                : throw new ArgumentNullException(nameof(settings.Value.IdentityConnectionString));
        }
        public async Task<IEnumerable<FixUnitDto>> GetFixUnits(string userIdentity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT  e.[Id] ,e.[Owner],e.[Title],e.[Description]
<<<<<<< HEAD
                        FROM [EMS].[ems].[fixUnits] as e " +
                         $@"$where e.[Owner] ='{userIdentity}' or  EXISTS (
=======
                        FROM [EMS].[ems].[fixUnits] as e 
                         where e.[Owner] ='{userIdentity}' or  EXISTS (
>>>>>>> dev
                         SELECT 1
                         FROM [EMS].[ems].members AS m " +
                          $"WHERE e.[Id] = m.[FixUnitId] AND m.[IdentityGuid] ='{userIdentity}' )  ";
                return await connection.QueryAsync<FixUnitDto>(query);
            }
        }
        public async Task<bool> IsValid(string userGuid, int fixUnitId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT  count(e.id)
                 FROM [EMS].[ems].[fixUnits] as e 
                 where (e.[Owner] ='{userGuid}' and e.id={fixUnitId} ) or  EXISTS (
                 SELECT 1
                 FROM [EMS].[ems].members AS m
                WHERE m.[FixUnitId] = {fixUnitId} AND m.[IdentityGuid] ='{userGuid}' ) ";
                return await connection.QueryFirstAsync<int>(query) > 0;
            }
        }
        public async Task<IEnumerable<FixUnitDto>> GetFixUnits()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                SELECT f.Id
                      ,f.Owner
                      ,it.Name as ItemType
                      ,l.Name as Location
                      ,f.Title
                      ,f.Description
                	  
                      FROM [EMS].[ems].[fixUnits] f
                      left join [EMS].[ems].itemTypes it on it.Id = f.ItemTypeId
                       left join [EMS].[ems].locations l on l.Id = f.LocationId";
                var r = await connection.QueryAsync<FixUnitDto>(query);
                return r;
            }
        }

        public async Task<FixUnitDto> GetFixUnit(int fixUnitId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"   SELECT a.Id
                      ,a.Owner
                      ,l.Name as Location   
                      ,l.Id as LocationId
                      ,it.Name as ItemType
                      ,it.Id as ItemTypeId
                      ,a.Title
                      ,a.Description
                       FROM [EMS].[ems].[fixUnits] a
                      left join [EMS].[ems].itemTypes it on it.Id = a.ItemTypeId
                       left join [EMS].[ems].locations l on l.Id = a.LocationId where a.Id={fixUnitId}";
                var r = await connection.QuerySingleOrDefaultAsync<FixUnitDto>(query);
                return r;
                //                var query = $@"
                //                SELECT a.Id
                //                      ,a.ItemTypeId
                //                      ,a.Owner
                //                      ,a.LocationId
                //                      ,a.Title
                //                      ,a.Description
                //                	  ,b.[IdentityGuid] as id
                //                      ,b.Name
                //                  FROM [EMS].[ems].[fixUnits] a join [EMS].[ems].[members] b on a.Id=b.FixUnitId where a.Id = {fixUnitId}";
                //                var lookup = new Dictionary<int, FixUnitDto>();
                //
                //                var r = (await connection.QueryAsync<FixUnitDto,MemberDto,FixUnitDto>(query,(f,memberDto)=>
                //                {
                //                    FixUnitDto fixUnitDto;
                //                    if (!lookup.TryGetValue(f.Id, out fixUnitDto))
                //                    {
                //                        lookup.Add(f.Id, fixUnitDto = f);
                //                    }
                //                    fixUnitDto.MemberDtos = fixUnitDto.MemberDtos ?? new List<MemberDto>();
                //                    fixUnitDto.MemberDtos.Add(memberDto);
                //                    return fixUnitDto;
                //                })).FirstOrDefault();
                //
                //                using (var iConnection = new SqlConnection(_identityConnectionString))
                //                {
                //                    var q = $@"select id,name from [EMSIdentityDB].[dbo].[AspNetUsers] where id = '{r.Owner}'";
                //                    var user =await iConnection.QueryFirstAsync<UserDto>(q);
                //                    r.OwnerName = user.Name;
                //                    return r;
                //                }

            }
        }
        public async Task<IEnumerable<MemberDto>> GetMembers(IEnumerable<string> userGuids)
        {
            var ids = userGuids.Aggregate("", (current, key) => current + $"'{key}',");
            ids = ids.Remove(ids.Length - 1);

            using (var connection = new SqlConnection(_identityConnectionString))
            {
                var query = $@"select id,name from [EMSIdentityDB].[dbo].[AspNetUsers] where id IN ({ids})";
                return await connection.QueryAsync<MemberDto>(query);
            }
        }
        public async Task<int> GetArchiveFaultsCountAsync(int fixUnitId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT  count(id)
                FROM [EMS].[ems].[faults]
                 where [FixUnitId]= 1 and FaultStatusId = 5";
                return await connection.QueryFirstAsync<int>(query);
            }
        }
        public async Task<IEnumerable<FaultDto>> GetArchiveFaultsAsync(int fixUnitId, int pageSize, int pageIndex)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT  f.[Id]
                          ,fs.Name
                          ,[Title]
                          ,[Description]
                          ,ft.Name as FaultType
                          ,it.Name as ItemType
                          ,l.Name as Location
                          ,[OwnerId]
                          ,[Assign_Time] as AssingTime
                          ,[Assign_UserDisplayName] as AssignUser
	                      ,CreatedDate as CreatedTime
                             FROM [EMS].[ems].[faults] f
                             left join [EMS].[ems].[faultStatus] fs on fs.id =[FaultStatusId]
                             left join [EMS].[ems].faultTypes ft on ft.Id = f.FaultTypeId
                             left join [EMS].[ems].itemTypes it on it.Id = f.ItemTypeId
                             left join [EMS].[ems].locations l on l.Id = f.LocationId
                             where [FixUnitId]= {fixUnitId} and FaultStatusId = 5 
                             ORDER BY f.[Id]  OFFSET  {pageSize * pageIndex} rows  FETCH NEXT {pageSize} rows only";
                return await connection.QueryAsync<FaultDto>(query);
            }
        }
        public async Task<int> GetFaultsCountAsync(int fixUnitId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT  count(id)
                FROM [EMS].[ems].[faults]
                 where [FixUnitId]= {fixUnitId} and FaultStatusId <> 5";
                return await connection.QueryFirstAsync<int>(query);
            }
        }
        public async Task<IEnumerable<FaultDto>> GetFaultsAsync(int fixUnitId, int pageSize, int pageIndex)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT  f.[Id]
                          ,fs.Name
                          ,[Title]
                          ,[Description]
                          ,ft.Name as FaultType
                          ,it.Name as ItemType
                          ,l.Name as Location
                          ,[OwnerId]
                          ,[Assign_Time] as AssingTime
                          ,[Assign_UserDisplayName] as AssignUser
	                      ,CreatedDate as CreatedTime
                             FROM [EMS].[ems].[faults] f
                             left join [EMS].[ems].[faultStatus] fs on fs.id =[FaultStatusId]
                             left join [EMS].[ems].faultTypes ft on ft.Id = f.FaultTypeId
                             left join [EMS].[ems].itemTypes it on it.Id = f.ItemTypeId
                             left join [EMS].[ems].locations l on l.Id = f.LocationId
                             where [FixUnitId]= {fixUnitId} and FaultStatusId <> 5 
                             ORDER BY f.[Id]  OFFSET  {pageSize * pageIndex} rows  FETCH NEXT {pageSize} rows only";
                return await connection.QueryAsync<FaultDto>(query);
            }
        }
    }
}