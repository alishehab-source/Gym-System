using Gym_System.Models;

namespace Gym_System.Repository
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAll();
        Payment? GetById(int id);
        void Add(Payment payment);
        void Update(Payment payment);
        void Delete(Payment payment);
        void Save();
    }
}
