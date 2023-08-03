using Church.Models;
using Church.ServiceInterfaces;
using Church.RepositoryInterfaces;

namespace Church.Services
{
    // Visitor Service
    public class VisitorService : IVisitorService
    {
        private readonly IVisitorRepository _visitorRepository;

        public VisitorService(IVisitorRepository visitorRepository)
        {
            _visitorRepository = visitorRepository;
        }

        public async Task<Visitor> AddVisitor(Visitor visitor)
        {
            return await _visitorRepository.AddVisitor(visitor);
        }

        public async Task<Visitor> GetVisitor(string id)
        {
            return await _visitorRepository.GetVisitor(id);
        }

        public async Task<IEnumerable<Visitor>> GetAllVisitors()
        {
            return await _visitorRepository.GetAllVisitors();
        }

        public async Task<Visitor> UpdateVisitor(Visitor visitor)
        {
            return await _visitorRepository.UpdateVisitor(visitor);
        }

        public async Task DeleteVisitor(string id)
        {
            await _visitorRepository.DeleteVisitor(id);
        }

        public IEnumerable<Visitor> GetVisitorsByDate(DateTime date)
        {
            return _visitorRepository.GetVisitorsByDate(date);
        }

    }
}
