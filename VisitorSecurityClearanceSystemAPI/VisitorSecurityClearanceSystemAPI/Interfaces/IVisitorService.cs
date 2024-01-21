using VisitorSecurityClearanceSystemAPI.Entities;

namespace VisitorSecurityClearanceSystemAPI.Interfaces
{
    public interface IVisitorService
    {
        Task<Visitor> AddVisitor(Visitor visitor);
        Task<Visitor> GetVisitorByVisitorId(string visitorId);
        Task<Visitor> GetVisitorByMobileNo(string MobileNo);
        Task<List<Visitor>> GetAllVisitor();
        Task<List<Visitor>> GetAllPendingVisitor();
        Task<List<Visitor>> GetAllApprovedVisitor();
        Task<List<Visitor>> GetAllRejectedVisitor();
        Task<Visitor> UpdateVisitor(Visitor visitor);
    }
}
