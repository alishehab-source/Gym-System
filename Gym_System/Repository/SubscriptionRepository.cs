using Gym_System.Data;
using Gym_System.Models;

namespace Gym_System.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly GymDbContext _context;

        public SubscriptionRepository(GymDbContext context)
        {
            _context = context;
        }
        public void Add(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
        }

        public void Delete(Subscription subscription)
        {

            _context.Subscriptions.Remove(subscription);


        }

        public IEnumerable<Subscription> GetAll()
        {
            return _context.Subscriptions.ToList();
        }

        public Subscription? GetById(int id)
        {
            return _context.Subscriptions.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
        }
    }
}
