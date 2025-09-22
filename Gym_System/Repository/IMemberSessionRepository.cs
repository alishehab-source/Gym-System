using Gym_System.Models;

namespace Gym_System.Repository
{
    public interface IMemberSessionRepository
    {
        IEnumerable<MemberSession> GetAll();
        MemberSession GetById(int id);
        void Add(MemberSession session);
        void Update(MemberSession session);
        void Delete(int id);

    }
}
