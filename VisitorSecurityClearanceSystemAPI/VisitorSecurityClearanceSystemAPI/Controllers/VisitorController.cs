using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Net;
using System.Net.Mail;
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
                string subject = "  Visit Request Acknowledgment";
                string message = $@"
Hello {response.Name},
       Thank you for submitting your visit pass request to Centralogic. Your Visitor ID is {response.VisitorId}. Kindly keep this ID for future reference.

We acknowledge receipt of your request and want to assure you that our team is in the process of reviewing and taking necessary actions. You can use the provided Visitor ID to check the status of your request.

If you have any immediate concerns or require further information, please feel free to contact our dedicated manager at manager@gmail.com . We appreciate your patience and look forward to welcoming you soon.

Best regards,

Kumar Kad
Developer
Centralogic    
";
                SendMail(response.EmailId, subject, message);
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

        private void SendMail(string mail, string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("kumarkad03@gmail.com", "fuhalslpuadsaawz"),
                EnableSsl = true,
            };
            smtpClient.Send("kumarkad03@gmail.com", mail, subject, body);
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
