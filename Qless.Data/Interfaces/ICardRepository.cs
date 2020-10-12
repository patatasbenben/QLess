using Qless.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qless.Data.Interfaces
{
    public interface ICardRepository
    {
        string CreateCard();

        string SetCardToDiscounted(Guid cardId);

    }
}
