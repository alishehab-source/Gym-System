using Gym_System.Models;
using Gym_System.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Gym_System.Controllers
{
    public class PaymentController : Controller
    {

        private readonly IPaymentRepository _paymentRepo;
        private readonly IMemberRepository _memberRepo;

        public PaymentController(IPaymentRepository paymentRepo, IMemberRepository memberRepo)
        {
            _paymentRepo = paymentRepo;
            _memberRepo = memberRepo;
        }

        [HttpGet]
        public IActionResult Index(int memberId)
        {
            var member = _memberRepo.GetById(memberId);
            if (member == null)
                return NotFound();

            ViewBag.MemberName = member.FullName;
            ViewBag.MemberId = memberId;

            var payments = _paymentRepo.GetAll()
                .Where(p => p.MemberId == memberId)
                .OrderByDescending(p => p.PaymentDate)
                .ToList();

            return View(payments);
        }

        [HttpGet]
        public IActionResult Create(int memberId)
        {
            var member = _memberRepo.GetById(memberId);
            if (member == null)
                return NotFound();

            var payment = new Payment
            {
                MemberId = member.Id,
                PaymentDate = DateTime.Now
            };

            ViewBag.MemberName = member.FullName;

            return View(payment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Payment payment)
        {
            if (ModelState.IsValid)
            {

                _paymentRepo.Add(payment);


                var member = _memberRepo.GetById(payment.MemberId);
                if (member != null)
                {
                    member.TotalPaid += payment.Amount;
                    _memberRepo.Update(member);
                }

                _paymentRepo.Save();
                _memberRepo.Save();

                return RedirectToAction("Index", "Member");
            }


            var memberInfo = _memberRepo.GetById(payment.MemberId);
            ViewBag.MemberName = memberInfo?.FullName ?? "Unknown";

            return View(payment);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var payment = _paymentRepo.GetById(id);
            if (payment == null)
                return NotFound();

            return View(payment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var payment = _paymentRepo.GetById(id);
            if (payment == null)
                return NotFound();

            var member = _memberRepo.GetById(payment.MemberId);
            if (member != null)
            {
                member.TotalPaid -= payment.Amount;
                if (member.TotalPaid < 0) member.TotalPaid = 0;

                _memberRepo.Update(member);
            }

            _paymentRepo.Delete(payment);
            _paymentRepo.Save();
            _memberRepo.Save();

            return RedirectToAction("Index", new { memberId = payment.MemberId });
        }
    }
}
