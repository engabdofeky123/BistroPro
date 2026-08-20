using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant_Management.Identity;
using Restaurant_Management.Models;
using System.Reflection.Emit;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}
        
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RestaurantTable> RestaurantTables { get; set; }
        public DbSet<ReservationSlot> ReservationSlots { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ReservationSlot>().HasData(
                    new ReservationSlot
                    {
                        Id = 1,
                        StartTime = new TimeSpan(12, 0, 0),
                        EndTime = new TimeSpan(13, 30, 0),
                        IsActive = true
                    },

                    new ReservationSlot
                    {
                        Id = 2,
                        StartTime = new TimeSpan(13, 30, 0),
                        EndTime = new TimeSpan(15, 0, 0),
                        IsActive = true
                    },

                    new ReservationSlot
                    {
                        Id = 3,
                        StartTime = new TimeSpan(15, 0, 0),
                        EndTime = new TimeSpan(16, 30, 0),
                        IsActive = true
                    },

                    new ReservationSlot
                    {
                        Id = 4,
                        StartTime = new TimeSpan(17, 0, 0),
                        EndTime = new TimeSpan(18, 30, 0),
                        IsActive = true
                    },

                    new ReservationSlot
                    {
                        Id = 5,
                        StartTime = new TimeSpan(18, 30, 0),
                        EndTime = new TimeSpan(20, 0, 0),
                        IsActive = true
                    },

                    new ReservationSlot
                    {
                        Id = 6,
                        StartTime = new TimeSpan(20, 0, 0),
                        EndTime = new TimeSpan(21, 30, 0),
                        IsActive = true
                    },

                    new ReservationSlot
                    {
                        Id = 7,
                        StartTime = new TimeSpan(21, 30, 0),
                        EndTime = new TimeSpan(23, 0, 0),
                        IsActive = true
                    }
            );

            // Customer -> Orders
            builder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Customer -> Reservations
            builder.Entity<Customer>()
                .HasMany(c => c.Reservations)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order -> OrderItems
            builder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Meal -> OrderItems
            builder.Entity<Meal>()
                .HasMany(m => m.OrderItems)
                .WithOne(oi => oi.Meal)
                .HasForeignKey(oi => oi.MealId)
                .OnDelete(DeleteBehavior.Restrict);

            // RestaurantTable -> Reservations
            builder.Entity<RestaurantTable>()
                .HasMany(t => t.Reservations)
                .WithOne(r => r.Table)
                .HasForeignKey(r => r.TableId)
                .OnDelete(DeleteBehavior.Restrict);

            // Decimal Precision
            builder.Entity<Meal>()
                .Property(m => m.Price)
                .HasPrecision(18, 2);

            builder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            builder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasPrecision(18, 2);
        }
    }
}
