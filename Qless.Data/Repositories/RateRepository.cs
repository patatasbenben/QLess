using Qless.Data.DataContexts;
using Qless.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qless.Data.Repositories
{
    public class RateRepository : IRateRepository
    {
        private readonly QlessDbContext _context;

        public RateRepository(QlessDbContext context)
        {
            _context = context;
        }
    }
}
