using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
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
        public IMapper _mapper;

        public SecurityUserController(ISecurityUserService securityUserService, IMapper mapper)
        {
            _container = GetContainer();
            _securityUserService = securityUserService;
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
