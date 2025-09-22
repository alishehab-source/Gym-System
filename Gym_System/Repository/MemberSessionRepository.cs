using Gym_System.Data;
using Gym_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_System.Repository
{
    public class MemberSessionRepository : IMemberSessionRepository
    {
        private readonly GymDbContext _context;

        public MemberSessionRepository(GymDbContext context)
        {
            _context = context;
        }

        public IEnumerable<MemberSession> GetAll()
        {
            return _context.MemberSessions.Include(ms => ms.Member).ToList();
        }

        public MemberSession GetById(int id)
        {
            return _context.MemberSessions.FirstOrDefault(x => x.Id == id);
        }

        public void Add(MemberSession session)
        {
            _context.MemberSessions.Add(session);
            _context.SaveChanges();
        }

        public void Update(MemberSession session)
        {
            _context.MemberSessions.Update(session);
           
        }

        public void Delete(int id)
        {
            var session = _context.MemberSessions.Find(id);
            if (session != null)
            {
                _context.MemberSessions.Remove(session);
                _context.SaveChanges();
            }
        }
    }
}
