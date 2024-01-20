using VisitorSecurityClearanceSystemAPI.Entities;

namespace VisitorSecurityClearanceSystemAPI.CosmosDBServer
{
    public interface ICosmosService
    {
        Task<Manager> SignUpManager(Manager manager);
        Task<Manager> LoginManager(string username, string password);
    }
}
