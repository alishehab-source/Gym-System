using Gym_System.Models;
using Gym_System.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Gym_System.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionRepository _subscriptionRepo;

        public SubscriptionController(ISubscriptionRepository subscriptionRepo)
        {
            _subscriptionRepo = subscriptionRepo;
        }
        public IActionResult Index()
        {
            var subscription = _subscriptionRepo.GetAll();
            return View(subscription);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                _subscriptionRepo.Add(subscription);
                _subscriptionRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(subscription);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var subscription = _subscriptionRepo.GetById(id);
            if (subscription == null)
            {
                return NotFound();
            }
            return View(subscription);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                _subscriptionRepo.Update(subscription);
                _subscriptionRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(subscription);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var subscription = _subscriptionRepo.GetById(id);
            if (subscription == null)
            {
                return NotFound();
            }
            return View(subscription);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var subscription = _subscriptionRepo.GetById(id);
            if (subscription == null)
            {
                return NotFound();
            }

            _subscriptionRepo.Delete(subscription);
            _subscriptionRepo.Save();

            return RedirectToAction(nameof(Index));

        }
    }
}
