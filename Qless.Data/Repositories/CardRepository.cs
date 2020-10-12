using EFCore.BulkExtensions;
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
    public class CardRepository : ICardRepository
    {
        private readonly QlessDbContext _context;

        private readonly ICardTransactionRepository _cardTransactionRepository;

        public CardRepository(QlessDbContext context, ICardTransactionRepository cardTransactionRepository)
        {
            _context = context;
            _cardTransactionRepository = cardTransactionRepository;
        }

        public string CreateCard()
        {
            try
            {
                var card = new Card()
                {
                    AddedDate = DateTime.Now
                };

                _context.Cards.Add(card);
                _context.SaveChanges();

                var transaction = _cardTransactionRepository.CreateTransaction(card.CardId, card.AddedDate, TransactionType.Initial, 0);

                return "Card has been created. Current load:" + transaction.Value + "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SetCardToDiscounted(Guid cardId)
        {
            try
            {
                var card = _context.Cards.Where(x => x.CardId == cardId && !x.isDeleted).FirstOrDefault();

                if (card is null)
                {
                    return "Card does not exist.";
                }

                if (card.isDisCounted) //A Q-LESS Transport Card can only be registered once and non-reversible.
                {
                    return "Card is already discounted.";
                }

                if (DateTime.Now > card.AddedDate.Value.AddMonths(6)) //Q-LESS Transport Card can be registered within 6 months upon purchase
                {
                    return "Card can be registered for discount within 6 months upon purchase only.";
                }

                var diff = card.AddedDate.Value.AddMonths(6);

                card.isDisCounted = true;
                card.DiscountedRegisterDate = DateTime.Now;
                card.ModifiedDate = DateTime.Now;

                _context.Cards.Update(card);
                _context.SaveChanges();

                return "Your card is now discounted. Thank you.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
