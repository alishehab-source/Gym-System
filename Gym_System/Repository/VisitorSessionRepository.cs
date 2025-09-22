using Gym_System.Data;
using Gym_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_System.Repository
{
    public class VisitorSessionRepository : IVisitorSessionRepository
    {
        private readonly GymDbContext _context;

        public VisitorSessionRepository(GymDbContext context)
        {
            _context = context;
        }
        public void Add(VisitorSession session)
        {
            _context.VisitorSessions.Add(session);
        }

        public void Delete(VisitorSession session)
        {
            _context.VisitorSessions.Remove(session);
        }

        public IEnumerable<VisitorSession> GetAll()
        {
            return _context.VisitorSessions.Include(v => v.SessionType).ToList();
        }

        public VisitorSession GetById(int id)
        {
            return _context.VisitorSessions.Include(v => v.SessionType).FirstOrDefault(v => v.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(VisitorSession session)
        {
            _context.VisitorSessions.Update(session);
        }
    }
}
