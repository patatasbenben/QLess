using Qless.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Qless.Entities.Models
{
    public class CardTransaction : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransactionId { get; set; }

        public Guid CardId { get; set; }

        public TransactionType Type { get; set; }

        public decimal Value { get; set; }
    }
}
