using Church.Models;

namespace Church.ServiceInterfaces
{
    public interface IVisitorService
    {
        Task<IEnumerable<Visitor>> GetAllVisitors();
        Task<Visitor> GetVisitor(string id);
        Task<Visitor> AddVisitor(Visitor visitor);
        Task<Visitor> UpdateVisitor(Visitor visitor);
        Task DeleteVisitor(string id);
        IEnumerable<Visitor> GetVisitorsByDate(DateTime date);
    }
}
