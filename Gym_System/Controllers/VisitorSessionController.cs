using Gym_System.Models;
using Gym_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym_System.Controllers
{
    public class VisitorSessionController : Controller
    {
        private readonly IVisitorSessionRepository _visitorSessionRepo;
        private readonly ISessionTypeRepository _sessionTypeRepo;

        public VisitorSessionController(IVisitorSessionRepository visitorSessionRepo, ISessionTypeRepository sessionTypeRepo)
        {
            _visitorSessionRepo = visitorSessionRepo;
            _sessionTypeRepo = sessionTypeRepo;
        }

        public IActionResult Index()
        {
            var visitorSessions = _visitorSessionRepo.GetAll();
            return View(visitorSessions);
        }

        public IActionResult Create()
        {
            var sessionTypes = _sessionTypeRepo.GetAll().ToList();
            ViewBag.SessionTypes = sessionTypes;
            ViewBag.SessionTypeCount = sessionTypes.Count();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VisitorSession visitorSession)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _visitorSessionRepo.Add(visitorSession);
                    _visitorSessionRepo.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
            }
            else
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                ViewBag.ValidationErrors = errors;
            }

            // إعادة تحميل البيانات
            ViewBag.SessionTypes = _sessionTypeRepo.GetAll().ToList();
            return View(visitorSession);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var visitorSession = _visitorSessionRepo.GetById(id);
            if (visitorSession == null)
                return NotFound();

            ViewBag.SessionTypes = new SelectList(_sessionTypeRepo.GetAll(), "Id", "Name", visitorSession.SessionTypeId);
            return View(visitorSession);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VisitorSession visitorSession)
        {
            if (ModelState.IsValid)
            {
                _visitorSessionRepo.Update(visitorSession);
                _visitorSessionRepo.Save();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.SessionTypes = new SelectList(_sessionTypeRepo.GetAll(), "Id", "Name", visitorSession.SessionTypeId);
            return View(visitorSession);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var visitorSession = _visitorSessionRepo.GetById(id);
            if (visitorSession == null)
                return NotFound();

            return View(visitorSession);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var visitorSession = _visitorSessionRepo.GetById(id);
            if (visitorSession == null)
                return NotFound();

            _visitorSessionRepo.Delete(visitorSession);
            _visitorSessionRepo.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
