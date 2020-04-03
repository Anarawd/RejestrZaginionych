using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zaginionki.Models;

namespace Zaginionki.Data
{
    public class ZaginionkiContext : IdentityDbContext <User>
    {
        public ZaginionkiContext (DbContextOptions<ZaginionkiContext> options)
            : base(options)
        {
        }

        public DbSet<Zaginionki.Models.Zaginiony> Zaginiony { get; set; }
    }
}
