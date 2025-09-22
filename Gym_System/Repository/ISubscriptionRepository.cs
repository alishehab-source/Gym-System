using Gym_System.Models;

namespace Gym_System.Repository
{
    public interface ISubscriptionRepository
    {
        IEnumerable<Subscription> GetAll();
        Subscription? GetById(int id);
        void Add(Subscription subscription);
        void Update(Subscription subscription);
        void Delete(Subscription subscription);
        void Save();
    }
}
