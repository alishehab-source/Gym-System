using Gym_System.Models;
using Gym_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym_System.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly IMemberRepository _memberRepo;
        private readonly ISubscriptionRepository _subscriptionRepo;
        private readonly IExpenseRepository _expenseRepo;
      

        public MemberController(IMemberRepository memberRepo, ISubscriptionRepository subscriptionRepo, 
            IExpenseRepository expenseRepo)
        {
            _memberRepo = memberRepo;
            _subscriptionRepo = subscriptionRepo;
            _expenseRepo = expenseRepo;
       
        }

        public IActionResult Index(string search)
        {
            var members = _memberRepo.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                members = members.Where(m => m.FullName.ToLower().Contains(search.ToLower()));
            }
            else
            {
                members = members.ToList();
            }

            return View(members);
        }



        public IActionResult Details(int id)
        {
            var Member = _memberRepo.GetById(id);
            if (Member == null)
            {
                return NotFound();
            }
            return View(Member);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Subscriptions = new SelectList(_subscriptionRepo.GetAll(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Member member)
        {
           
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new {
                        Field = x.Key,
                        Errors = string.Join(", ", x.Value.Errors.Select(e => e.ErrorMessage))
                    })
                    .ToList();

               
                ViewBag.Errors = errors;
                ViewBag.Subscriptions = new SelectList(_subscriptionRepo.GetAll(), "Id", "Name", member.SubscriptionId);
                return View(member);
            }

            var subscription = _subscriptionRepo.GetById(member.SubscriptionId);
            if (subscription != null)
            {
                member.EndDate = member.StartDate.AddDays(subscription.DurationInDay);
                member.SubscriptionPrice = subscription.Price;
            }

            _memberRepo.Add(member);
            _memberRepo.Save();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Member = _memberRepo.GetById(id);
            if (Member == null)
            {
                return NotFound();
            }
            ViewBag.Subscriptions = new SelectList(_subscriptionRepo.GetAll(), "Id", "Name", Member.SubscriptionId);
            return View(Member);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Member member)
        {
            if (ModelState.IsValid)
            {
                _memberRepo.Update(member);
                _memberRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Subscriptions = new SelectList(_subscriptionRepo.GetAll(), "Id", "Name", member.SubscriptionId);
            return View(member);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var member = _memberRepo.GetById(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleletConfirmed(int id)
        {
            var member = _memberRepo.GetById(id);
            if (member == null)
            {
                return NotFound();
            }
            _memberRepo.Delete(member);
            _memberRepo.Save();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Report(DateTime? from, DateTime? to)
        {
            var members = _memberRepo.GetAll();

            DateTime fromDate, toDate;

            if (from.HasValue && to.HasValue)
            {
                fromDate = from.Value.Date;
                toDate = to.Value.Date;
                members = members.Where(m => m.StartDate.Date >= fromDate && m.StartDate.Date <= toDate);
            }
            else
            {
                var today = DateTime.Today;
                fromDate = new DateTime(today.Year, today.Month, 1);
                toDate = fromDate.AddMonths(1).AddDays(-1);
                members = members.Where(m => m.StartDate.Date >= fromDate && m.StartDate.Date <= toDate);
            }

            var totalPaid = members.Sum(m => m.TotalPaid);
            var totalMembers = members.Count();

            var expenses = _expenseRepo.GetAll()
                .Where(e => e.Date.Date >= fromDate && e.Date.Date <= toDate)
                .ToList();

            var totalExpenses = expenses.Sum(e => e.Amount);

            ViewBag.TotalPaid = totalPaid;
            ViewBag.TotalMembers = totalMembers;
            ViewBag.TotalExpenses = totalExpenses;
            ViewBag.NetIncome = totalPaid - totalExpenses;

            ViewBag.From = fromDate.ToString("yyyy-MM-dd");
            ViewBag.To = toDate.ToString("yyyy-MM-dd");

            return View(members);
        }
    }
}
