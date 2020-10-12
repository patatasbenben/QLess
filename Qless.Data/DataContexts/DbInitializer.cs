using Qless.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qless.Data.DataContexts
{
    public class DbInitializer
    {
        public void Initialize(QlessDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Rates.Any())
            {
                return;
            }

            var rates = new Rates[]
            {
            new Rates{From="location 1",To="location 1",Value=11},
            new Rates{From="location 1",To="location 2",Value=12},
            new Rates{From="location 1",To="location 3",Value=13},
            new Rates{From="location 2",To="location 1",Value=12},
            new Rates{From="location 2",To="location 2",Value=11},
            new Rates{From="location 2",To="location 3",Value=12},
            new Rates{From="location 3",To="location 1",Value=13},
            new Rates{From="location 3",To="location 2",Value=12},
            new Rates{From="location 3",To="location 3",Value=11},
            };

            foreach (var rate in rates)
            {
                context.Rates.Add(rate);
            }

            context.SaveChanges();
        }
    }
}
