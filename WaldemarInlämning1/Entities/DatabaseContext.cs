﻿
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaldemarInlämning1.Entities
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> context) : base(context)
        {

        }
    }
}