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
        public IOfficeUserService _officeUserService;
        public ISecurityUserService _securityUserService;
        public IMapper _mapper;

        public ManagerController(IManagerService managerService, IOfficeUserService officeUserService, ISecurityUserService securityUserService, IMapper mapper)
        {
            _container = GetContainer();
            _managerService = managerService;
            _officeUserService = officeUserService;
            _securityUserService = securityUserService;
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
            catch (Exception ex)
            {
                return BadRequest("Data Adding Failed " + ex);
            }

        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> AddOfficeUser(OfficeUserModel officeUserModel)
        {
            try
            {
                OfficeUser officeUser = new OfficeUser();
                officeUser.Name = officeUserModel.Name;
                officeUser.EmailId = officeUserModel.EmailId;
                officeUser.MobileNo = officeUserModel.MobileNo;
                officeUser.UserName = officeUserModel.UserName.ToLower();
                officeUser.Password = officeUserModel.Password;
                officeUser.Role = officeUserModel.Role;

                var response = await _officeUserService.AddOfficeUser(officeUser);

                var model = _mapper.Map<OfficeUserModel>(response);
                return Ok("User Added Sucessfully !!!");
            }
            catch (Exception ex)
            {
                return BadRequest("Data Adding Failed " + ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSecurityUser(SecurityUserModel securityUserModel)
        {
            try
            {
                SecurityUser securityUser = new SecurityUser();
                securityUser.Name = securityUserModel.Name;
                securityUser.EmailId = securityUserModel.EmailId;
                securityUser.MobileNo = securityUserModel.MobileNo;
                securityUser.UserName = securityUserModel.UserName.ToLower();
                securityUser.Password = securityUserModel.Password;
                securityUser.Role = securityUserModel.Role;

                var response = await _securityUserService.AddSecurityUser(securityUser);

                var model = _mapper.Map<SecurityUserModel>(response);
                return Ok("User Added Sucessfully !!!");
            }
            catch (Exception ex)
            {
                return BadRequest("Data Adding Failed " + ex);
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
