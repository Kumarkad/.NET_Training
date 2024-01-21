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
    public class VisitorController : ControllerBase
    {
        public readonly Container _container;
        public IVisitorService _visitorService;
        public IMapper _mapper;

        public VisitorController(IVisitorService visitorService, IMapper mapper)
        {
            _container = GetContainer();
            _visitorService = visitorService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> VisitRequest(VisitorModel visitorModel)
        {
            try
            {
                Visitor visitor = new Visitor();
                visitor.Name = visitorModel.Name;
                visitor.EmailId = visitorModel.EmailId;
                visitor.MobileNo = visitorModel.MobileNo;
                visitor.Purpose = visitorModel.Purpose;
                visitor.EntryTime = visitorModel.EntryTime;
                visitor.ExitTime = visitorModel.ExitTime;

                var response = await _visitorService.AddVisitor(visitor);

                var model = _mapper.Map<VisitorModel>(response);
                return Ok($"Request Sent Sucessfully !!!\n Your VisitorId is {model.VisitorId}");
            }
            catch (Exception ex)
            {
                return BadRequest("Data Adding Failed " + ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CheckRequestStatus(string VisitorId)
        {
            var visitor = await _visitorService.GetVisitorByVisitorId(VisitorId);

            if (visitor != null)
            {
                var model = _mapper.Map<RequestModel>(visitor);
                return Ok(model);
            }
            else
            {
                return Ok("No Student Found !!!");
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
