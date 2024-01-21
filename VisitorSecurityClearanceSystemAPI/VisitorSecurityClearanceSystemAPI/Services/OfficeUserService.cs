using VisitorSecurityClearanceSystemAPI.CosmosDBServices;
using VisitorSecurityClearanceSystemAPI.Entities;
using VisitorSecurityClearanceSystemAPI.Interfaces;

namespace VisitorSecurityClearanceSystemAPI.Services
{
    public class OfficeUserService : IOfficeUserService
    {
        public ICosmosService _cosmosService;
        public OfficeUserService(ICosmosService cosmosService)
        {
            _cosmosService = cosmosService;
        }
        public async Task<OfficeUser> AddOfficeUser(OfficeUser user)
        {
            user.Id = Guid.NewGuid().ToString();
            user.UId = user.Id;
            user.DocumentType = "officeUser";

            user.CreatedOn = DateTime.Now;
            user.CreatedByName = "Kumar";
            user.CreatedBy = "Kumar's UId";

            user.UpdatedOn = DateTime.Now;
            user.UpdatedByName = "";
            user.UpdatedBy = "";

            user.Version = 1;
            user.Active = true;
            user.Archieved = false;

            OfficeUser response = await _cosmosService.AddOfficeUser(user);
            return response;
        }

        public async Task<OfficeUser> LoginOfficeUser(string username, string password)
        {
            var response = await _cosmosService.LoginOfficeUser(username, password);
            return response;
        }
    }
}
