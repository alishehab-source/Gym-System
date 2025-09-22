using Gym_System.Models;

namespace Gym_System.Repository
{
    public interface ISessionTypeRepository
    {
        IEnumerable<SessionType> GetAll();
        SessionType GetById(int id);
        void Add(SessionType sessionType);
        void Update(SessionType sessionType);
        void Delete(SessionType sessionType);
        void Save();
    }
}
