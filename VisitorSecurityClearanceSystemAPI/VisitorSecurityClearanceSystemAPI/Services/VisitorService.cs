using VisitorSecurityClearanceSystemAPI.CosmosDBServices;
using VisitorSecurityClearanceSystemAPI.Entities;
using VisitorSecurityClearanceSystemAPI.Interfaces;

namespace VisitorSecurityClearanceSystemAPI.Services
{
    public class VisitorService : IVisitorService
    {
        public ICosmosService _cosmosService;
        public VisitorService(ICosmosService cosmosService)
        {
            _cosmosService = cosmosService;
        }
        public async Task<Visitor> AddVisitor(Visitor visitor)
        {
            visitor.Id = Guid.NewGuid().ToString();
            visitor.UId = visitor.Id;
            visitor.DocumentType = "visitor";

            visitor.CreatedOn = DateTime.Now;
            visitor.CreatedByName = "Kumar";
            visitor.CreatedBy = "Kumar's UId";

            visitor.UpdatedOn = DateTime.Now;
            visitor.UpdatedByName = "";
            visitor.UpdatedBy = "";

            visitor.Version = 1;
            visitor.Active = true;
            visitor.Archieved = false;

            visitor.VisitorId = visitor.Id;
            visitor.ApprovedStatus = "pending";

            Visitor response = await _cosmosService.AddVisitor(visitor);
            return response;
        }

        public async Task<List<Visitor>> GetAllApprovedVisitor()
        {
            return await _cosmosService.GetAllApprovedVisitor();
        }

        public async Task<List<Visitor>> GetAllPendingVisitor()
        {
            return await _cosmosService.GetAllPendingVisitor();
        }

        public async Task<List<Visitor>> GetAllRejectedVisitor()
        {
            return await _cosmosService.GetAllRejectedVisitor();
        }

        public async Task<List<Visitor>> GetAllVisitor()
        {
            return await _cosmosService.GetAllVisitor();
        }

        public async Task<Visitor> GetVisitorByMobileNo(string MobileNo)
        {
            return await _cosmosService.GetVisitorByMobileNo(MobileNo);
        }

        public async Task<Visitor> GetVisitorByVisitorId(string visitorId)
        {
            return await _cosmosService.GetVisitorByVisitorId(visitorId);
        }

        public async Task<Visitor> UpdateVisitor(Visitor visitor)
        {
            visitor.Version++;
            visitor.UpdatedOn = DateTime.Now;
            visitor.UpdatedByName = "Kumar";
            visitor.UpdatedBy = "Kumar's UId";
            return await _cosmosService.UpdateVisitor(visitor);
        }
    }
}
