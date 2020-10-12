using Qless.Data.DataContexts;
using Qless.Data.Interfaces;
using Qless.Entities.Enums;
using Qless.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qless.Data.Repositories
{
    public class CardTransactionRepository : ICardTransactionRepository
    {
        private readonly QlessDbContext _context;

        public CardTransactionRepository(QlessDbContext context)
        {
            _context = context;
        }

        public CardTransaction CreateTransaction(Guid cardId, DateTime? addedDate,TransactionType transactionType, decimal amount)
        {
            try
            {
                var transaction = new CardTransaction()
                {
                    CardId = cardId,
                    AddedDate = transactionType == TransactionType.Initial ? addedDate : DateTime.Now,
                    Type = transactionType,
                    Value = transactionType == TransactionType.Initial ? 100m : amount //Q-LESS Transport Cards will have an initial load of P100
                };

                _context.CardTransactions.Add(transaction);
                _context.SaveChanges();

                return transaction;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ProceedTransaction(Guid cardId, TransactionType transactionType, decimal amount)
        {
            try
            {
                var card = _context.Cards.Where(x => x.CardId == cardId).FirstOrDefault();

                if (card is null)
                {
                    return "Card does not exist.";
                }

                if (DateTime.Now > card.AddedDate.Value.AddYears(5)) //Q-LESS Transport Card is valid up to 5 years after the last used date
                {
                    return "Card is not valid due to 5 years validity policy.";
                }

                if (transactionType == TransactionType.Load && (amount < 100 || amount > 10000)) //Q-LESS Transport Cardholder should be able to load their card with a starting value of P100 up toP10,000
                {
                    return "Amount should be between P100 - P10,000.";
                }

                var rateDiscount = amount;

                if (transactionType == TransactionType.Expense)
                {
                    var transactions = _context.CardTransactions.Where(x => x.CardId == card.CardId
                    && x.Type == TransactionType.Expense && x.AddedDate.Value.Date == DateTime.Now.Date).ToList();

                    if (card.isDisCounted) //Discounted Card Types should apply 20% discounts
                    {
                        rateDiscount = rateDiscount - (rateDiscount * 0.2m);
                    }

                    if (transactions.Count < 5) //3% discount will be applied with a maximum of 4 discounts applied for the day
                    {
                        rateDiscount = rateDiscount - (rateDiscount * 0.03m);
                    }
                }

                CreateTransaction(card.CardId, card.AddedDate, transactionType, rateDiscount);

                var totalAmount = CurrentLoad(card.CardId);

                if (transactionType == TransactionType.Expense)
                {
                    return "Transaction complete. Fair Rate: " + rateDiscount + ". Current Amount: " + totalAmount + ".";
                }
                else
                {
                    return "Transaction complete. Added amount: " + amount + ". Current Amount: " + totalAmount + ".";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public decimal CurrentLoad(Guid cardId)
        {
            try
            {
                var allTransactions = _context.CardTransactions.Where(x => x.CardId == cardId).ToList();
                var totalAmount = (allTransactions.Where(x => x.Type != TransactionType.Expense).Sum(x => x.Value) -
                    allTransactions.Where(x => x.Type == TransactionType.Expense).Sum(x => x.Value));

                return totalAmount;
            }
            catch (Exception ex )
            {
                throw ex;
            }
        }
    }
}
