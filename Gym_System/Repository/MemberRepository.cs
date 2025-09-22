namespace Gym_System.Repository
{
    using global::Gym_System.Data;
    using global::Gym_System.Models;
    using Microsoft.EntityFrameworkCore;

    namespace Gym_System.Repository
    {
        public class MemberRepository : IMemberRepository
        {
            private readonly GymDbContext _context;

            public MemberRepository(GymDbContext context)
            {
                _context = context;
            }
            public void Add(Member member)
            {
                _context.Members.Add(member);

            }

            public void Delete(Member member)
            {

                if (member != null)
                {
                    _context.Members.Remove(member);
                }

            }

            public IEnumerable<Member> GetAll()
            {
                return _context.Members
                    .Include(m => m.Subscription)
                    .Include(m => m.MemberSessions)
                    .ToList();
            }

            public Member? GetById(int id)
            {
                return _context.Members.Include(m => m.Subscription).Include(m => m.Payments)
                    .Include(m => m.MemberSessions).FirstOrDefault(m => m.Id == id);
            }

            public void Save()
            {
                _context.SaveChanges();
            }

            public void Update(Member member)
            {
                _context.Members.Update(member);
            }
        }
    }
} 
