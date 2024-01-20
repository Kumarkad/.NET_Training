using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using VisitorSecurityClearanceSystemAPI.DTO;
using VisitorSecurityClearanceSystemAPI.Entities;
using VisitorSecurityClearanceSystemAPI.Interfaces;

namespace VisitorSecurityClearanceSystemAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {

        public readonly Container _container;
        public IManagerService _managerService;
        public IMapper _mapper;

        public ManagerController(IManagerService managerService,IMapper mapper)
        {
            _container = GetContainer();
            _managerService = managerService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> ManagerSignUp(ManagerModel managerModel)
        {
            try
            {
                Manager manager = new Manager();

                manager.Name = managerModel.Name;
                manager.EmailId = managerModel.EmailId;
                manager.MobileNo = managerModel.MobileNo;
                manager.UserName = managerModel.UserName.ToLower();
                manager.Password = managerModel.Password;

                var response = await _managerService.SignUpManager(manager);

                var model = _mapper.Map<ManagerModel>(response);
                return Ok("SignUp Sucessfully !!!");
            }
            catch(Exception ex) 
            {
                return BadRequest("Data Adding Failed "+ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> ManagerLogin(string username, string password)
        {
            try
            {
                username = username.ToLower();
                var manager = await _managerService.LoginManager(username, password);

                if (manager != null)
                {
                    return Ok($" UId : {manager.UId}  \n Login Successfully !!! ");
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
