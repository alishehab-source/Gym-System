using Gym_System.Models;
using Gym_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym_System.Controllers
{
    public class MemberSessionController : Controller
    {
        private readonly IMemberSessionRepository _sessionRepo;
        private readonly IMemberRepository _memberRepo;

        public MemberSessionController(IMemberSessionRepository sessionRepo, IMemberRepository memberRepo)
        {
            _sessionRepo = sessionRepo;
            _memberRepo = memberRepo;
        }

        public IActionResult Index()
        {
            var sessions = _sessionRepo.GetAll();
            return View(sessions);
        }

        public IActionResult Details(int id)
        {
            var session = _sessionRepo.GetById(id);
            if (session == null)
                return NotFound();

            return View(session);
        }

        public IActionResult Create()
        {
            ViewBag.Members = new SelectList(_memberRepo.GetAll(), "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MemberSession session)
        {
            if (ModelState.IsValid)
            {
                _sessionRepo.Add(session);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Members = new SelectList(_memberRepo.GetAll(), "Id", "FullName", session.MemberId);
            return View(session);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var session = _sessionRepo.GetById(id);
            if (session == null)
                return NotFound();

            ViewBag.Members = new SelectList(_memberRepo.GetAll(), "Id", "FullName", session.MemberId);
            return View(session);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MemberSession session)
        {
            if (ModelState.IsValid)
            {
                _sessionRepo.Update(session);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Members = new SelectList(_memberRepo.GetAll(), "Id", "FullName", session.MemberId);
            return View(session);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var session = _sessionRepo.GetById(id);
            if (session == null)
                return NotFound();

            return View(session);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _sessionRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ForMember(int memberId)
        {
            var sessions = _sessionRepo.GetAll()
                .Where(s => s.MemberId == memberId)
                .ToList();

            return View(sessions);
        }
    }
}
