using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using EMS.WebSPA.Application.Dtos;
using Microsoft.Extensions.Options;
namespace EMS.WebSPA.Application.Queries{
    public class FaultQuery:IFaultQuery
    {
        private readonly string _connectionString = string.Empty;
        public FaultQuery(IOptions<AppSettings> settings)
        {
            _connectionString = !string.IsNullOrWhiteSpace(settings.Value.ConnectionString)
                ? settings.Value.ConnectionString
                : throw new ArgumentNullException(nameof(settings.Value.ConnectionString));
        }
        public async Task<IEnumerable<FaultPartDto>> GetFaultsParts(int faultId){
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select id,PartName ,PartId,Count from [EMS].[ems].[consumeParts] where FaultId = '{faultId}'";
                return await connection.QueryAsync<FaultPartDto>(query);
            }
        }
        public async Task<bool> IsExistFaultResult(string userGuid){
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"select count(id) from [EMS].[ems].[faultResults] where userid = '{userGuid}'";
                return await connection.QueryFirstAsync<int>(query) >0;
            }
        }
        public async Task<IEnumerable<FaultDto>> MyFaultsAsync(string userGuid, int pageSize, int pageIndex){
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
                             where [OwnerId]= '{userGuid}' and FaultStatusId <> 5 
                             ORDER BY f.[Id]  OFFSET  {pageSize * pageIndex} rows  FETCH NEXT {pageSize} rows only";
                return await connection.QueryAsync<FaultDto>(query);
            }
        }
        public async Task<int> MyFaultsCountAsync(string userGuid){
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT  count(f.[Id])
                             FROM [EMS].[ems].[faults] f
                             where [OwnerId]= '{userGuid}' and FaultStatusId <> 5";
                return await connection.QueryFirstAsync<int>(query);
            }
        }
        public async Task<IEnumerable<FaultDto>> MyArchiveFaultsAsync(string userGuid, int pageSize, int pageIndex)
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
                             where [OwnerId]= '{userGuid}' and FaultStatusId = 5 
                             ORDER BY f.[Id]  OFFSET  {pageSize * pageIndex} rows  FETCH NEXT {pageSize} rows only";
                return await connection.QueryAsync<FaultDto>(query);
            }
        }
        public async Task<int> MyArchiveFaultsCountAsync(string userGuid){
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $@"SELECT  count(f.[Id])
                             FROM [EMS].[ems].[faults] f
                             where [OwnerId]= '{userGuid}' and FaultStatusId = 5 ";
                return await connection.QueryFirstAsync<int>(query);
            }
        }
    }
}