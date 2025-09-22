using Gym_System.Data;
using Gym_System.Models;

namespace Gym_System.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly GymDbContext _context;

        public ExpenseRepository(GymDbContext context)
        {
            _context = context;
        }

        public void Add(Expense expense)
        {
            _context.Expenses.Update(expense);
        }

        public void Delete(Expense expense)
        {
            _context.Expenses.Remove(expense);
        }

        public IEnumerable<Expense> GetAll()
        {
            return _context.Expenses.OrderByDescending(e => e.Date).ToList();
        }

        public Expense? GetById(int id)
        {
            return _context.Expenses.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Expense expense)
        {
            _context.Expenses.Update(expense);
        }
    }
}
