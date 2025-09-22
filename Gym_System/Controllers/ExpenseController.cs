using Gym_System.Models;
using Gym_System.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Gym_System.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseRepository _expenseRepo;

        public ExpenseController(IExpenseRepository expenseRepo)
        {
            _expenseRepo = expenseRepo;
        }
        public IActionResult Index()
        {
            var Expense = _expenseRepo.GetAll();
            return View(Expense);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                _expenseRepo.Add(expense);
                _expenseRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var expense = _expenseRepo.GetById(id);
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var Expense = _expenseRepo.GetById(id);
            if (Expense != null)
            {
                _expenseRepo.Delete(Expense);
                _expenseRepo.Save();

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
