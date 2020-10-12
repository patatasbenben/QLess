using System;
using System.Collections.Generic;
using System.Text;

namespace Qless.Entities.Models
{
    public class BaseEntity
    {
        public DateTime? AddedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool isDeleted { get; set; }
    }
}
