using Gym_System.Models;

namespace Gym_System.Repository
{
    public interface IVisitorSessionRepository
    {
        IEnumerable<VisitorSession> GetAll();
        VisitorSession GetById(int id);
        void Add(VisitorSession session);
        void Update(VisitorSession session);
        void Delete(VisitorSession session);
        void Save();
    }
}
