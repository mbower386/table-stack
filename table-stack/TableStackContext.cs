using System;
using Microsoft.EntityFrameworkCore;
using table_stack.Controllers;

namespace table_stack
{
    public class TableStackContext : DbContext
    {
        public TableStackContext (DbContextOptions<TableStackContext> options) : base (options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}