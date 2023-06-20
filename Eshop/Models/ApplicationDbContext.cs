﻿using Microsoft.EntityFrameworkCore;

namespace Eshop.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ClientOrder> ClientOrders { get; set; }
    public DbSet<Product> Products { get; set; }
}