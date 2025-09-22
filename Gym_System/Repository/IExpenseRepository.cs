using Gym_System.Models;

namespace Gym_System.Repository
{
    public interface IExpenseRepository
    {
        IEnumerable<Expense> GetAll();
        Expense? GetById(int id);
        void Add(Expense expense);
        void Update(Expense expense);
        void Delete(Expense expense);
        void Save();
    }
}
