﻿using Microsoft.EntityFrameworkCore;
using MyRPGList.Models;

namespace MyRPGList.Data;

public class MyRpgListDbContext : DbContext
{
    public MyRpgListDbContext(DbContextOptions<MyRpgListDbContext> options) : base(options)
    {
        
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Dev> Developers { get; set; }
    public DbSet<User> Users { get; set; }
}
