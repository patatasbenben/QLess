using Qless.Entities.Enums;
using Qless.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qless.Data.Interfaces
{
    public interface ICardTransactionRepository
    {
        CardTransaction CreateTransaction(Guid cardId, DateTime? addedDate,TransactionType transactionType, decimal amount);

        string ProceedTransaction(Guid cardId, TransactionType transactionType, decimal amount);

        decimal CurrentLoad(Guid cardId);
    }
}
