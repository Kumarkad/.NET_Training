using VisitorSecurityClearanceSystemAPI.CosmosDBServices;
using VisitorSecurityClearanceSystemAPI.Entities;
using VisitorSecurityClearanceSystemAPI.Interfaces;

namespace VisitorSecurityClearanceSystemAPI.Services
{
    public class SecurityUserService : ISecurityUserService
    {
        public ICosmosService _cosmosService;
        public SecurityUserService(ICosmosService cosmosService)
        {
            _cosmosService = cosmosService;
        }

        public async Task<SecurityUser> AddSecurityUser(SecurityUser user)
        {
            user.Id = Guid.NewGuid().ToString();
            user.UId = user.Id;
            user.DocumentType = "securityUser";

            user.CreatedOn = DateTime.Now;
            user.CreatedByName = "Kumar";
            user.CreatedBy = "Kumar's UId";

            user.UpdatedOn = DateTime.Now;
            user.UpdatedByName = "";
            user.UpdatedBy = "";

            user.Version = 1;
            user.Active = true;
            user.Archieved = false;

            SecurityUser response = await _cosmosService.AddSecurityUser(user);
            return response;
        }

        public async Task<SecurityUser> DeleteUser(SecurityUser user)
        {
            return await _cosmosService.DeleteSecurityUser(user);
        }

        public async Task<SecurityUser> GetSecurityUserbyUId(string UId)
        {
            return await _cosmosService.GetSecurityUserbyUId(UId);
        }

        public async Task<SecurityUser> LoginSecurityUser(string username, string password)
        {
            var response = await _cosmosService.LoginSecurityUser(username, password);
            return response;
        }
    }
}
