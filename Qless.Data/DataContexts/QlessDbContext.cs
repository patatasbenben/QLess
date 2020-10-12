using Microsoft.EntityFrameworkCore;
using Qless.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qless.Data.DataContexts
{
    public class QlessDbContext : DbContext
    {
        public QlessDbContext(DbContextOptions<QlessDbContext> options) : base(options)
        {

        }


        public DbSet<Card> Cards { get; set; }

        public DbSet<CardTransaction> CardTransactions { get; set; }

        public DbSet<Rates> Rates { get; set; }
    }
}
