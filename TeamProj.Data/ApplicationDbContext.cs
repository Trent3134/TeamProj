using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<PostEntity> Posts{get; set;}
        
    }
