using VisitorSecurityClearanceSystemAPI.Entities;

namespace VisitorSecurityClearanceSystemAPI.Interfaces
{
    public interface IOfficeUserService
    {
        Task<OfficeUser> AddOfficeUser(OfficeUser user);
        Task<OfficeUser> LoginOfficeUser(string username, string password);
        Task<OfficeUser> GetOfficeUserbyUId(string UId);
        Task<OfficeUser> DeleteUser(OfficeUser user);
    }
}
