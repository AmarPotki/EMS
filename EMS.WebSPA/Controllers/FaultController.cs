using BuildingBlocks.Infrastructure.Services.Commands;
using BuildingBlocks.Infrastructure.Services.Identity;
using EMS.Domain.AggregatesModel.FaultAggregate;
using EMS.Domain.Exceptions;
using EMS.WebSPA.Application.Commands.FaultCommands;
using EMS.WebSPA.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EMS.Domain.AggregatesModel.UserProfileAggregate;
using EMS.WebSPA.Application.Dtos;
namespace EMS.WebSPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaultController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFaultRepository _faultRepository;
        private readonly IIdentityService _identityService;
        private readonly IFixUnitQuery _fixUnitQuery;
        private readonly IFaultQuery _faultQuery;
        public FaultController(IMediator mediator, IFaultRepository faultRepository, IIdentityService identityService, IFixUnitQuery fixUnitQuery, IFaultQuery faultQuery)
        {
            _mediator = mediator;
            _faultRepository = faultRepository;
            _identityService = identityService;
            _fixUnitQuery = fixUnitQuery;
            _faultQuery = faultQuery;
        }

        [Route("addFault")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddFault([FromBody] AddFaultCommand command,
            [FromHeader(Name = "x-requestid")] string requestId)
        {
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty)
            {
                var request = new IdentifiedCommand<AddFaultCommand, bool>(command, guid);
                commandResult = await _mediator.Send(request);
            }
            return commandResult ? Ok() : (IActionResult)BadRequest();
        }
        [Route("assignFault")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AssignFault([FromBody] AssignCommand command,
            [FromHeader(Name = "x-requestid")] string requestId)
        {
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty)
            {
                var request = new IdentifiedCommand<AssignCommand, bool>(command, guid);
                commandResult = await _mediator.Send(request);
            }
            return commandResult ? Ok() : (IActionResult)BadRequest();
        }
        [Route("moveFault")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> MoveFault([FromBody] MoveFaultCommand command,
            [FromHeader(Name = "x-requestid")] string requestId)
        {
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty)
            {
                var request = new IdentifiedCommand<MoveFaultCommand, bool>(command, guid);
                commandResult = await _mediator.Send(request);
            }
            return commandResult ? Ok() : (IActionResult)BadRequest();
        }
        [Route("fixFault")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> FixFault([FromBody] FixFaultCommand command,
            [FromHeader(Name = "x-requestid")] string requestId)
        {
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty)
            {
                var request = new IdentifiedCommand<FixFaultCommand, bool>(command, guid);
                commandResult = await _mediator.Send(request);
            }
            return commandResult ? Ok() : (IActionResult)BadRequest();
        }
        [Route("addPartToFault")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddPartToFault([FromBody] AddPartToFaultCommand command,
            [FromHeader(Name = "x-requestid")] string requestId)
        {
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty)
            {
                var request = new IdentifiedCommand<AddPartToFaultCommand, bool>(command, guid);
                commandResult = await _mediator.Send(request);
            }
            return commandResult ? Ok() : (IActionResult)BadRequest();
        }
        [Route("deletePartFromFault")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeletePartFromFault([FromBody] DeletePartFromFaultCommand command,
            [FromHeader(Name = "x-requestid")] string requestId)
        {
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty)
            {
                var request = new IdentifiedCommand<DeletePartFromFaultCommand, bool>(command, guid);
                commandResult = await _mediator.Send(request);
            }
            return commandResult ? Ok() : (IActionResult)BadRequest();
        }
        [Route("getFaultParts")]
        [HttpGet]
        [ProducesResponseType(typeof(FaultPartDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetFaultParts(int faultId)
        {
            try
            {
                var userGuid = _identityService.GetUserIdentity();
                if (string.IsNullOrEmpty(userGuid)) throw new EMSDomainException("خطا در گرفتن اطلاعات کاربر");
                var fault = await _faultRepository.FindAsync(faultId);
                if (fault == null) throw new EMSDomainException("شناسه نامعتبر است");
                if (!await _fixUnitQuery.IsValid(userGuid, fault.GetFixUnitId()))
                    throw new EMSDomainException("شما به این واحد دسترسی ندارید");
                var model =await _faultQuery.GetFaultsParts(faultId);
                return Ok(model);
            }
            catch (EMSDomainException) { throw; }
            catch { return NotFound(); }
        }
        
        //normal user
        [Route("myFaults")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FaultDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> MyFaults([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            try
            {
                var userGuid = _identityService.GetUserIdentity();
                if (string.IsNullOrEmpty(userGuid)) throw new EMSDomainException("خطا در گرفتن اطلاعات کاربر");
                var total = await _faultQuery.MyFaultsCountAsync(userGuid);
                var faults = await _faultQuery.MyFaultsAsync(userGuid,pageSize,pageIndex);
                var model = new PaginatedItemsViewModel<FaultDto>(pageIndex, pageSize, total, faults);
                return Ok(model);
            }
            catch (EMSDomainException) { throw; }
            catch { return NotFound(); }
        }

        [Route("myArchiveFaults")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FaultDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> MyArchiveFaults([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            try
            {
                var userGuid = _identityService.GetUserIdentity();
                if (string.IsNullOrEmpty(userGuid)) throw new EMSDomainException("خطا در گرفتن اطلاعات کاربر");
                var total = await _faultQuery.MyArchiveFaultsCountAsync(userGuid);
                var faults = await _faultQuery.MyArchiveFaultsAsync(userGuid, pageSize, pageIndex);
                var model = new PaginatedItemsViewModel<FaultDto>(pageIndex, pageSize, total, faults);
                return Ok(model);
            }
            catch (EMSDomainException) { throw; }
            catch { return NotFound(); }
        }

        [Route("getMyFault/{id:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(Fault), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetMyFault(int id)
        {
           
                if (id == 0) throw new EMSDomainException("شناسه نامعتبر است");
                try{
                    var fault = await _faultRepository.GetFaultWithFlowAndFaultResult(id);
                    if (fault == null) throw new EMSDomainException("شناسه معتبر نمی باشد");
                    var userGuid = _identityService.GetUserIdentity();
                    if (fault.GetOwnerId() != userGuid)
                        throw new EMSDomainException("شناسه معتبر نمی باشد");
                    return Ok(fault);
                }
                catch (EMSDomainException){ throw; }
                catch { return NotFound(); }
          
        }

    }
}