using EventManagamentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Attendee> Attendees { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Feedback>()
        .HasOne(f => f.User)
        .WithMany()
        .HasForeignKey(f => f.UserId)
        .OnDelete(DeleteBehavior.NoAction);  // Change to NoAction or SetNull if desired

        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.Event)
            .WithMany()
            .HasForeignKey(f => f.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure composite key for Attendee
        modelBuilder.Entity<Attendee>()
            .HasKey(a => new { a.UserId, a.EventId });

        // Configure relationships for Attendee
        modelBuilder.Entity<Attendee>()
            .HasOne(a => a.User)
            .WithMany(u => u.Attendees)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.NoAction); // Set delete behavior to prevent cycles

        modelBuilder.Entity<Attendee>()
            .HasOne(a => a.Event)
            .WithMany(e => e.Attendees)
            .HasForeignKey(a => a.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }



}