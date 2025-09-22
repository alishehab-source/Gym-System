using Gym_System.Models.ViewModel;
using Gym_System.Models;
using Gym_System.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Gym_System.Controllers
{
    public class SessionTypeController : Controller
    {

        private readonly ISessionTypeRepository _sessionTypeRepo;

        public
            SessionTypeController(ISessionTypeRepository sessionTypeRepo)
        {
            _sessionTypeRepo = sessionTypeRepo;
        }

        public IActionResult Index()
        {
            var sessionTypes = _sessionTypeRepo.GetAll();
            return View(sessionTypes);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var sessionTypes = _sessionTypeRepo.GetAll();
            ViewBag.SessionTypes = new SelectList(sessionTypes, "Id", "Name");

            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(SessionTypeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var entity = new SessionType
                {
                    Name = vm.Name,
                    Price = vm.Price
                };
                _sessionTypeRepo.Add(entity);
                _sessionTypeRepo.Save();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var sessionType = _sessionTypeRepo.GetById(id);
            if (sessionType == null)
                return NotFound();

            return View(sessionType);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SessionType sessionType)
        {
            if (ModelState.IsValid)
            {
                _sessionTypeRepo.Update(sessionType);
                _sessionTypeRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(sessionType);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var sessionType = _sessionTypeRepo.GetById(id);
            if (sessionType == null)
                return NotFound();

            return View(sessionType);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var sessionType = _sessionTypeRepo.GetById(id);
            if (sessionType == null)
                return NotFound();

            _sessionTypeRepo.Delete(sessionType);
            _sessionTypeRepo.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
