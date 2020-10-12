using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Qless.Data.DataContexts;
using Qless.Data.Interfaces;
using Qless.Entities.Enums;
using Qless.Entities.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLess.Api
{
    [Route("api/[controller]/[action]")]
    public class CardController : Controller
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICardTransactionRepository _cardTransactionRepository;
        private readonly QlessDbContext _context;

        public CardController(ICardRepository cardRepository, ICardTransactionRepository cardTransactionRepository, QlessDbContext context)
        {
            _cardRepository = cardRepository;
            _cardTransactionRepository = cardTransactionRepository;
            _context = context;
        }

        [HttpPost]
        public JsonResult SetCardToDiscounted(Guid CardId, string controlNumber, string cardProvided)
        {
            try
            {
                if (cardProvided == "PWD" 
                    && (controlNumber.Count(x => x != '-')) < 12)
                {
                    return new JsonResult("Control number for PWD ID is invalid.");
                }

                if (cardProvided == "SC"
                    && (controlNumber.Count(x => x != '-')) < 10)
                {
                    return new JsonResult("Control number for PWD ID is invalid.");
                }

                return new JsonResult(_cardRepository.SetCardToDiscounted(CardId));
            }
            catch (Exception ex)
            {

                return new JsonResult(ex);
            }
        }

        [HttpPost]
        public JsonResult CreateCard()
        {
            try
            {
                return new JsonResult(_cardRepository.CreateCard());
            }
            catch (Exception ex)
            {

                return new JsonResult(ex);
            }
        }

        [HttpPost]
        public JsonResult Expense(Guid cardId, int rateId)
        {
            try
            {
                var amount = _context.Rates.Where(x => x.Id == rateId).FirstOrDefault().Value;

                return new JsonResult(_cardTransactionRepository.ProceedTransaction(cardId, TransactionType.Expense, amount));
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        [HttpPost]
        public JsonResult Load(Guid cardId, decimal amount)
        {
            try
            {
                return new JsonResult(_cardTransactionRepository.ProceedTransaction(cardId, TransactionType.Load, amount));
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        [HttpPost]
        public JsonResult CurrentLoad(Guid cardId)
        {
            try
            {
                return new JsonResult(_cardTransactionRepository.CurrentLoad(cardId));
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }
    }
}
