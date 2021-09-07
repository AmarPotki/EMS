using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Identity;
using EMS.Domain.AggregatesModel.FaultAggregate;
using EMS.Domain.AggregatesModel.FixUnitAggregate;
using EMS.Domain.Exceptions;
using EMS.WebSPA.Application.Commands.FixUnitCommands;
using EMS.WebSPA.Application.Dtos;
using EMS.WebSPA.Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.WebSPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixUnitController : ControllerBase{
        private readonly IFixUnitQuery _fixUnitQuery;
        private readonly IIdentityService _identityService;
        private readonly IFixUnitRepository _fixUnitRepository;
        private readonly IFaultRepository _faultRepository;

        public FixUnitController(IFixUnitQuery fixUnitQuery, IIdentityService identityService, IFixUnitRepository fixUnitRepository, IFaultRepository faultRepository){
            _fixUnitQuery = fixUnitQuery;
            _identityService = identityService;
            _fixUnitRepository = fixUnitRepository;
            _faultRepository = faultRepository;
        }
        [Route("getFixUnits")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FixUnitDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetFixUnits()
        {
            try
            {
                var fixUnits = await _fixUnitQuery.GetFixUnits();
                return Ok(fixUnits);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("getFixUnit/{id:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(FixUnit), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetFixUnit(int id)
        {
            if (id == 0) throw new EMSDomainException("شناسه نامعتبر است");
            try
            {
                var fixUnit = await _fixUnitQuery.GetFixUnit(id);
                if (fixUnit is null) throw new EMSDomainException("شناسه نامعتبر است");

                return Ok(fixUnit);
            }
            catch { return NotFound(); }
        }
        [Route("getMyFixUnits")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FixUnitDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetUsers()
        {
            try{
                var userGuid =  _identityService.GetUserIdentity();
                if(string.IsNullOrEmpty(userGuid)) throw new EMSDomainException("خطا در گرفتن اطلاعات کاربر");
                var fixUnits = await _fixUnitQuery.GetFixUnits(userGuid);
                return Ok(fixUnits);
            }
            catch { return NotFound(); }
        }
        [Route("getMembers")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MemberDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetMembers(int fixUnitId)
        {
            try
            {
            
                var fixUnits = await _fixUnitRepository.GetFixUnitWithMembers(fixUnitId);
                if(fixUnits is null) throw new EMSDomainException("شناسه معتبر نیست");
                var members = await _fixUnitQuery.GetMembers(fixUnits.Members.Select(x => x.IdentityGuid));
                return Ok(members);
            }
            catch { return NotFound(); }
        }
        [Route("getFault/{id:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(Fault), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetFault(int id)
        {
            if (id == 0) throw new EMSDomainException("شناسه نامعتبر است");
            try
            {
                var fault = await _faultRepository.GetFaultWithFaultResultAndPart(id);
                if (fault == null) throw new EMSDomainException("شناسه معتبر نمی باشد");
                var userGuid = _identityService.GetUserIdentity();
                if (!await _fixUnitQuery.IsValid(userGuid, fault.GetFixUnitId())) throw new EMSDomainException("شناسه معتبر نمی باشد");
                return Ok(fault);
            }
            catch (EMSDomainException) { throw; }
            catch { return NotFound(); }
        }
        [Route("getFaults")]
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<FaultDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetFaults(int fixUnitId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            try
            {
                var userGuid = _identityService.GetUserIdentity();
                if (string.IsNullOrEmpty(userGuid)) throw new EMSDomainException("خطا در گرفتن اطلاعات کاربر");
                 if (!await _fixUnitQuery.IsValid(userGuid, fixUnitId))
                     throw new EMSDomainException("شما به این واحد دسترسی ندارید");
                var total = await _fixUnitQuery.GetFaultsCountAsync(fixUnitId);
                var faults = await _fixUnitQuery.GetFaultsAsync(fixUnitId,pageSize,pageIndex);
                var model = new PaginatedItemsViewModel<FaultDto>(pageIndex, pageSize, total, faults);
                return Ok(model);
            }
            catch (EMSDomainException) { throw; }
            catch { return NotFound(); }
        }

        [Route("getArchiveFaults")]
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<FaultDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetArchiveFaults(int fixUnitId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            try
            {
                var userGuid = _identityService.GetUserIdentity();
                if (string.IsNullOrEmpty(userGuid)) throw new EMSDomainException("خطا در گرفتن اطلاعات کاربر");
                if (!await _fixUnitQuery.IsValid(userGuid, fixUnitId))
                    throw new EMSDomainException("شما به این واحد دسترسی ندارید");
                var total = await _fixUnitQuery.GetArchiveFaultsCountAsync(fixUnitId);
                var faults = await _fixUnitQuery.GetArchiveFaultsAsync(fixUnitId, pageSize, pageIndex);
                var model = new PaginatedItemsViewModel<FaultDto>(pageIndex, pageSize, total, faults);
                return Ok(model);
            }
            catch (EMSDomainException) { throw; }
            catch { return NotFound(); }
        }

    }
}