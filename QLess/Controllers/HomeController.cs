using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Qless.Data.Interfaces;
using Qless.Entities.Enums;
using Qless.Entities.ViewModels;

namespace QLess.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICardTransactionRepository _cardTransactionRepository;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICardRepository cardRepository, ICardTransactionRepository cardTransactionRepository)
        {
            _logger = logger;
            _cardRepository = cardRepository;
            _cardTransactionRepository = cardTransactionRepository;
        }

        public IActionResult Index()
        {
            if (TempData["shortMessage"] != null)
            {
                ViewBag.Message = TempData["shortMessage"].ToString();
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Load(Guid cardId, decimal amount)
        {
            var result = _cardTransactionRepository.ProceedTransaction(cardId, TransactionType.Load, amount);

            TempData["shortMessage"] = result;

            return RedirectToAction("Index", "Home");
        }

        public IActionResult SetCardToDiscounted(Guid CardId, string controlNumber, string cardProvided)
        {
            //if (cardProvided == "PWD"
            //        && (controlNumber.Count(x => x != '-')) < 12)
            //{
            //    return "Control number for PWD ID is invalid.";
            //}

            //if (cardProvided == "SC"
            //    && (controlNumber.Count(x => x != '-')) < 10)
            //{
            //    return "Control number for PWD ID is invalid.";
            //}

            var result = _cardRepository.SetCardToDiscounted(CardId);

            TempData["shortMessage"] = result;

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
