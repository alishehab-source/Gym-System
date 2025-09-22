using Gym_System.Data;
using Gym_System.Models;

namespace Gym_System.Repository
{
    public class SessionTypeRepository : ISessionTypeRepository
    {
        private readonly GymDbContext _context;

        public SessionTypeRepository(GymDbContext context)
        {
            _context = context;
        }
        public void Add(SessionType sessionType)
        {
            _context.SessionTypes.Add(sessionType);
        }

        public void Delete(SessionType sessionType)
        {

            _context.SessionTypes.Remove(sessionType);
        }

        public IEnumerable<SessionType> GetAll()
        {
            return _context.SessionTypes.ToList();
        }

        public SessionType GetById(int id)
        {
            return _context.SessionTypes.FirstOrDefault(s => s.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(SessionType sessionType)
        {
            _context.SessionTypes.Update(sessionType);
        }
    }
}
