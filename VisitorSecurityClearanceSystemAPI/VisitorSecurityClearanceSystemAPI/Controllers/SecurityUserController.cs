using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using VisitorSecurityClearanceSystemAPI.DTO;
using VisitorSecurityClearanceSystemAPI.Interfaces;
using VisitorSecurityClearanceSystemAPI.Services;

namespace VisitorSecurityClearanceSystemAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityUserController : ControllerBase
    {
        public readonly Container _container;
        public ISecurityUserService _securityUserService;
        public IVisitorService _visitorService;
        public IMapper _mapper;

        public SecurityUserController(ISecurityUserService securityUserService, IVisitorService visitorService, IMapper mapper)
        {
            _container = GetContainer();
            _securityUserService = securityUserService;
            _visitorService = visitorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                username = username.ToLower();
                var user = await _securityUserService.LoginSecurityUser(username, password);

                if (user != null)
                {
                    return Ok($" UId : {user.UId}  \n Login Successfully !!! ");
                }
                else
                {
                    return Unauthorized("Invalid Credentials !!!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Login Get Failed");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CheckRequestStatus(string MobileNo)
        {
            var visitor = await _visitorService.GetVisitorByMobileNo(MobileNo);

            if (visitor != null)
            {
                var model = _mapper.Map<RequestModel>(visitor);
                return Ok("Visitor Got the Pass !!!\n"+model);
            }
            else
            {
                return Ok("Visitor Does Not Get Permission !!!");
            }
        }

        private Container GetContainer()
        {
            string URI = Environment.GetEnvironmentVariable("cosmos-url");
            string PrimaryKey = Environment.GetEnvironmentVariable("auth-token");
            string DatabaseName = Environment.GetEnvironmentVariable("database-name");
            string ContainerName = Environment.GetEnvironmentVariable("container-name");

            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosclient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }
    }
}
