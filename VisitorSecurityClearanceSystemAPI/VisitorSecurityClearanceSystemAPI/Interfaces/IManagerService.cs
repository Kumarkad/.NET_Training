using VisitorSecurityClearanceSystemAPI.Entities;

namespace VisitorSecurityClearanceSystemAPI.Interfaces
{
    public interface IManagerService
    {
        Task<Manager> SignUpManager(Manager manager);
        Task<Manager> LoginManager(string username, string password);
    }
}
