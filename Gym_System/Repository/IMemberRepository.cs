using Gym_System.Models;

namespace Gym_System.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetAll();
        Member? GetById(int id);
        void Add(Member member);
        void Update(Member member);
        void Delete(Member member);
        void Save();
    }
}
