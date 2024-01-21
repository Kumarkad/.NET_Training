using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Net;
using System.Net.Mail;
using VisitorSecurityClearanceSystemAPI.DTO;
using VisitorSecurityClearanceSystemAPI.Entities;
using VisitorSecurityClearanceSystemAPI.Interfaces;
using VisitorSecurityClearanceSystemAPI.Services;

namespace VisitorSecurityClearanceSystemAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfficeUserController : ControllerBase
    {
        public readonly Container _container;
        public IOfficeUserService _officeUserService;
        public IVisitorService _visitorService;
        public IMapper _mapper;

        public OfficeUserController(IOfficeUserService officeUserService, IVisitorService visitorService, IMapper mapper)
        {
            _container = GetContainer();
            _officeUserService = officeUserService;
            _visitorService = visitorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                username = username.ToLower();
                var user = await _officeUserService.LoginOfficeUser(username, password);

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
        public async Task<IActionResult> GetAllRequest()
        {
            try
            {
                var requestList = await _visitorService.GetAllVisitor();

                if (requestList != null)
                {
                    List<RequestModel> requests = new List<RequestModel>();
                    foreach (var request in requestList)
                    {
                        RequestModel requestModel = _mapper.Map<RequestModel>(request);
                        requests.Add(requestModel);
                    }
                    return Ok(requests);
                }
                else
                {
                    return NotFound("No Request Found !!!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Data Get Failed");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPendingRequest()
        {
            try
            {
                var requestList = await _visitorService.GetAllPendingVisitor();

                if (requestList != null)
                {
                    List<RequestModel> requests = new List<RequestModel>();
                    foreach (var request in requestList)
                    {
                        RequestModel requestModel = _mapper.Map<RequestModel>(request);
                        requests.Add(requestModel);
                    }
                    return Ok(requests);
                }
                else
                {
                    return NotFound("No Request Found !!!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Data Get Failed");
            }
        }

        [HttpPut]
        public async Task<IActionResult> ApproveRequest(string VisitorId)
        {
            Visitor visitor=await _visitorService.GetVisitorByVisitorId(VisitorId);
            
            if (visitor != null && visitor.ApprovedStatus == "pending")
            {
                visitor.ApprovedStatus = "approved";


                try
                {
                    var response = await _visitorService.UpdateVisitor(visitor);
                    if (response != null)
                    {
                        string subject = " Acceptance of Visit Pass Request";
                        string message = $@"
Hello {response.Name},
       We are pleased to inform you that your visit pass request to our company has been accepted. We appreciate your interest in visiting and look forward to welcoming you on {response.EntryTime}. Please ensure that you bring a valid ID for verification purposes.

If you have any specific requirements or need additional information, feel free to contact our manager at manager@gmail.com. We hope your visit proves to be insightful and beneficial.

Best regards,

Kumar Kad
Developer
Centralogic    
";
                        SendMail(response.EmailId, subject, message);
                        return Ok("Request Approved Successfully !!!");
                    }
                    else
                    {
                        return BadRequest("Failed to Approve Request !!!");
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }

            return BadRequest("Request Not Found !!!");
        }

        [HttpPut]
        public async Task<IActionResult> RejectRequest(string VisitorId)
        {
            Visitor visitor = await _visitorService.GetVisitorByVisitorId(VisitorId);

            if (visitor != null && visitor.ApprovedStatus == "pending")
            {
                visitor.ApprovedStatus = "rejected";


                try
                {
                    var response = await _visitorService.UpdateVisitor(visitor);
                    if (response != null)
                    {
                        string subject = " Rejection of Visit Pass Request";
                        string message = $@"
Hello {response.Name},
       Thank you for your interest in visiting our company. We have carefully reviewed your visit pass request, and unfortunately, we are unable to approve it at this time.

We understand the importance of such visits and regret any inconvenience this decision may cause. If there are specific reasons for the rejection or if you have any questions, please don't hesitate to reach out to our [contact person] at [contact email/phone number].

We appreciate your understanding and hope to have the opportunity to welcome you in the future.

Best regards,

Kumar Kad
Developer
Centralogic       
";
                        SendMail(response.EmailId,subject,message);
                        return Ok("Request Rejected Successfully !!!");
                    }
                    else
                    {
                        return BadRequest("Failed to Reject Request !!!");
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }

            return BadRequest("Request Not Found !!!");
        }

        private void SendMail(string mail,string subject,string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port=587,
                Credentials=new NetworkCredential("kumarkad03@gmail.com", "fuhalslpuadsaawz"),
                EnableSsl=true,
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
