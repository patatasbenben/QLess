using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Qless.Entities.Models
{
    public class Card : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CardId { get; set; }

        public bool isDisCounted { get; set; }

        public DateTime? DiscountedRegisterDate { get; set; }
    }
}
