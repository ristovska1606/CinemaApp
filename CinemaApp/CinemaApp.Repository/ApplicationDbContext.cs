using CinemaApp.Domain.DomainModels;
using CinemaApp.Domain.IdentityModel;
using CinemaApp.Domain.Relationships;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace CinemaApp.Repository
{ 
    public class ApplicationDbContext : IdentityDbContext<CinemaAppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public virtual DbSet<TicketInOrder> TicketInOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ticket>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Movie>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ShoppingCart>()
                .HasOne<CinemaAppUser>(z => z.Owner)
                .WithOne(z => z.ShoppingCart)
                .HasForeignKey<ShoppingCart>(z => z.OwnerId);

            modelBuilder.Entity<TicketInShoppingCart>().HasKey(psc => new { psc.TicketId, psc.ShoppingCartId });

            modelBuilder.Entity<TicketInShoppingCart>()
                .HasOne(p => p.Ticket)
                .WithMany(sc => sc.TicketInShoppingCarts)
                .HasForeignKey(z => z.TicketId);

            modelBuilder.Entity<TicketInShoppingCart>()
              .HasOne(sc => sc.ShoppingCart)
              .WithMany(sc => sc.TicketsInShoppingCart)
              .HasForeignKey(z => z.ShoppingCartId);

            modelBuilder.Entity<TicketInOrder>().HasKey(psc => new { psc.TicketId, psc.OrderId });

            modelBuilder.Entity<TicketInOrder>()
                .HasOne(p => p.Ticket)
                .WithMany(sc => sc.TicketInOrders)
                .HasForeignKey(z => z.TicketId);

            modelBuilder.Entity<TicketInOrder>()
              .HasOne(sc => sc.UserOrder)
              .WithMany(sc => sc.TicketsInOrder)
              .HasForeignKey(z => z.OrderId);

            modelBuilder.Entity<Movie>()
                .HasMany(e => e.Tickets)
                .WithOne(e => e.Movie)
                .HasForeignKey(e => e.MovieId)
                .IsRequired();
        }

    }
}
