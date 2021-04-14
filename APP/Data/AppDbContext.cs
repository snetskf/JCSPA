using APP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<SPAHeader> SPAHeader { get; set; }

        public DbSet<Attachments> SPAAttachments { get; set; }

        public DbSet<SPARouting> SPARouting { get; set; }

        public DbSet<SPARequestRouting> SPARequestRouting { get; set; }


    }
}
