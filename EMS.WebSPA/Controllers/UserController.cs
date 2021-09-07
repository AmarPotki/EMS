using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EMS.WebSPA.Application.Dtos;
using EMS.WebSPA.Application.Queries;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Mvc;
namespace EMS.WebSPA.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase{
        private readonly IEmsQuery _emsQuery;
        public UserController(IEmsQuery emsQuery){
    
            _emsQuery = emsQuery;
        }
        [Route("searchUser")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> SearchUser(string name){
            var ti = HttpContext.GetMultiTenantContext()?.TenantInfo;
            try
            {
                var users = await _emsQuery.SearchUser(name);
                return Ok(users);
            }
            catch{ return NotFound(); }
        }
        [Route("getUsers")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetUsers(){
            try{
                var itemTypes = await _emsQuery.GetUsers();
                return Ok(itemTypes);
            }
            catch{ return NotFound(); }
        }
        [Route("getUser")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetUsers(string userIdentity){
            try{
                var itemTypes = await _emsQuery.GetUser(userIdentity);
                return Ok(itemTypes);
            }
            catch{ return NotFound(); }
        }
    }
}