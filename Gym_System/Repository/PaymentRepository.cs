using Gym_System.Data;
using Gym_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_System.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly GymDbContext _context;

        public PaymentRepository(GymDbContext context)
        {
            _context = context;
        }
        public void Add(Payment payment)
        {
            _context.Payments.Add(payment);
        }

        public void Delete(Payment payment)
        {

            _context.Payments.Remove(payment);

        }

        public IEnumerable<Payment> GetAll()
        {
            return _context.Payments.Include(p => p.Member).ToList();
        }

        public Payment? GetById(int id)
        {
            return _context.Payments.Include(p => p.Member).FirstOrDefault(p => p.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Payment payment)
        {
            _context.Payments.Update(payment);
        }
    }
}
