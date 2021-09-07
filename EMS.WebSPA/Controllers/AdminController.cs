using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BuildingBlocks.Infrastructure.Services.Commands;
using EMS.Domain.Exceptions;
using EMS.WebSPA.Application.Commands.AccountCommands;
using EMS.WebSPA.Application.Commands.FaultTypeCommands;
using EMS.WebSPA.Application.Commands.FixUnitCommands;
using EMS.WebSPA.Application.Commands.ItemTypeCommands;
using EMS.WebSPA.Application.Commands.LocationCommands;
using EMS.WebSPA.Application.Commands.PartCommands;
using EMS.WebSPA.Application.Dtos;
using EMS.WebSPA.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace EMS.WebSPA.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase{
        private readonly IEmsQuery _emsQuery;
        private readonly IMediator _mediator;
        public AdminController(IEmsQuery emsQuery, IMediator mediator){
            _emsQuery = emsQuery;
            _mediator = mediator;
        }

        #region Part

        [Route("createPart")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreatePart([FromBody] CreatePartCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<CreatePartCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("editPart")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EditPart([FromBody] EditPartCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<EditPartCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("deletePart")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeletePart([FromBody] DeletePartCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<DeletePartCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("disablePart")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DisablePart([FromBody] DisablePartCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<DisablePartCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("getParts")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PartDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetParts(){
            try{
                var parts = await _emsQuery.GetParts();
                return Ok(parts);
            }
            catch (Exception e){ return NotFound(); }
        }
        [Route("getPart/{id:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(PartDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetPart(int id){
            if (id == 0) throw new EMSDomainException("شناسه نامعتبر است");
            try{
                var part = await _emsQuery.GetPart(id);
                return Ok(part);
            }
            catch{ return NotFound(); }
        }

        #endregion

        #region Location

        [Route("createLocation")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateLocation([FromBody] CreateLocationCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<CreateLocationCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("getLocations/{parrentId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LocationDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetLocations(int parrentId){
            try{
                var locations = await _emsQuery.GetLocationChildren(parrentId);
                return Ok(locations);
            }
            catch{ return NotFound(); }
        }
        [Route("getAllLocations")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LocationDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetAllLocations(){
            try{
                var locations = await _emsQuery.GetAllLocations();
                return Ok(locations);
            }
            catch{ return NotFound(); }
        }
        [Route("deleteLocation")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteLocation([FromBody] DeleteLocationCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<DeleteLocationCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("updateLocation")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateLocation([FromBody] UpdateLocationCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<UpdateLocationCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("disableLocation")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DisableLocation([FromBody] DisableLocationCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<DisableLocationCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("enableLocation")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EnableLocation([FromBody] EnableLocationCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<EnableLocationCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }

        #endregion

        #region FixUnit

        [Route("createFixUnit")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateFixUnit([FromBody] CreateFixUnitCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<CreateFixUnitCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("deleteFixUnit")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteFixUnit([FromBody] DeleteFixUnitCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<DeleteFixUnitCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("editFixUnit")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EditFixUnit([FromBody] EditFixUnitCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var fixUnit = new IdentifiedCommand<EditFixUnitCommand, bool>(command, guid);
                commandResult = await _mediator.Send(fixUnit);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("addMember")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddMember([FromBody] AddMemberCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var request = new IdentifiedCommand<AddMemberCommand, bool>(command, guid);
                commandResult = await _mediator.Send(request);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("removeMember")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveMember([FromBody] DeleteMemberCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var request = new IdentifiedCommand<DeleteMemberCommand, bool>(command, guid);
                commandResult = await _mediator.Send(request);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }

        #endregion

        #region ItemType

        [Route("createItemType")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateItemType([FromBody] CreateItemTypeCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<CreateItemTypeCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("editItemType")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EditItemType([FromBody] EditItemTypeCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<EditItemTypeCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        
        [Route("getAllItemTypes")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemTypeDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetAllItemTypes(){
            try{
                var itemTypes = await _emsQuery.GetAllItemTypes();
                return Ok(itemTypes);
            }
            catch{ return NotFound(); }
        }

       
        [Route("getItemTypes/{parrentId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemTypeDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetItemTypes(int parrentId){
            try{
                var itemTypes = await _emsQuery.GetItemTypeChildren(parrentId);
                return Ok(itemTypes);
            }
            catch{ return NotFound(); }
        }
        [Route("deleteItemType")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteItemType([FromBody] DeleteItemTypeCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var itemType = new IdentifiedCommand<DeleteItemTypeCommand, bool>(command, guid);
                commandResult = await _mediator.Send(itemType);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        
        [Route("disableItemType")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DisableItemType([FromBody] DisableItemTypeCommand command,
            [FromHeader(Name = "x-requestid")] string requestId)
        {
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty)
            {
                var location = new IdentifiedCommand<DisableItemTypeCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult)BadRequest();
        }
        [Route("enableItemType")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EnableLocation([FromBody] EnableItemTypeCommand command,
            [FromHeader(Name = "x-requestid")] string requestId)
        {
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty)
            {
                var location = new IdentifiedCommand<EnableItemTypeCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult)BadRequest();
        }
        #endregion

        #region FaultType

        [Route("createFaultType")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateFaultType([FromBody] CreateFaultTypeCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<CreateFaultTypeCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("editFaultType")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EditFaultType([FromBody] EditFaultTypeCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<EditFaultTypeCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("deleteFaultType")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteFaultType([FromBody] DeleteFaultTypeCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<DeleteFaultTypeCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("disableFaultType")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DisableFaultType([FromBody] DisableFaultTypeCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var location = new IdentifiedCommand<DisableFaultTypeCommand, bool>(command, guid);
                commandResult = await _mediator.Send(location);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("getFaultTypes")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FaultTypeDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetFaultTypes(){
            try{
                var itemTypes = await _emsQuery.GetFaultTypes();
                return Ok(itemTypes);
            }
            catch (Exception e){ return NotFound(); }
        }
        [Route("getFaultType/{id:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(FaultTypeDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetFaultType(int id){
            if (id == 0) throw new EMSDomainException("شناسه نامعتبر است");
            try{
                var itemTypes = await _emsQuery.GetFaultType(id);
                return Ok(itemTypes);
            }
            catch{ return NotFound(); }
        }

        #endregion

        #region User

        [Route("createUser")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] RegisterCommand registerAgentCommand,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var createAgentRequest = new IdentifiedCommand<RegisterCommand, bool>(registerAgentCommand, guid);
                commandResult = await _mediator.Send(createAgentRequest);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("lockOutUser")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LockOutUser([FromBody] LockOutCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var request = new IdentifiedCommand<LockOutCommand, bool>(command, guid);
                commandResult = await _mediator.Send(request);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("unLockOutUser")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UnLockOutUser([FromBody] UnLockOutCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var request = new IdentifiedCommand<UnLockOutCommand, bool>(command, guid);
                commandResult = await _mediator.Send(request);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }
        [Route("resetPassword")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command,
            [FromHeader(Name = "x-requestid")] string requestId){
            var commandResult = false;
            if (Guid.TryParse(requestId, out var guid) && guid != Guid.Empty){
                var request = new IdentifiedCommand<ResetPasswordCommand, bool>(command, guid);
                commandResult = await _mediator.Send(request);
            }

            return commandResult ? Ok() : (IActionResult) BadRequest();
        }

        #endregion
    }
}