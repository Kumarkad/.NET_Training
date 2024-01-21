using VisitorSecurityClearanceSystemAPI.CosmosDBServices;
using VisitorSecurityClearanceSystemAPI.Entities;
using VisitorSecurityClearanceSystemAPI.Interfaces;

namespace VisitorSecurityClearanceSystemAPI.Services
{
    public class ManagerService :IManagerService
    {
        public ICosmosService _cosmosService;
        public ManagerService(ICosmosService cosmosService)
        {
            _cosmosService = cosmosService;
        }

        public async Task<Manager> LoginManager(string username, string password)
        {
            var response = await _cosmosService.LoginManager(username, password);
            return response;
        }

        public async Task<Manager> SignUpManager(Manager manager)
        {
            manager.Id = Guid.NewGuid().ToString();
            manager.UId = manager.Id;
            manager.DocumentType = "manager";

            manager.CreatedOn = DateTime.Now;
            manager.CreatedByName = "Kumar";
            manager.CreatedBy = "Kumar's UId";

            manager.UpdatedOn = DateTime.Now;
            manager.UpdatedByName = "";
            manager.UpdatedBy = "";

            manager.Version = 1;
            manager.Active = true;
            manager.Archieved = false;

            Manager response = await _cosmosService.SignUpManager(manager);
            return response;

        }
    }
}
