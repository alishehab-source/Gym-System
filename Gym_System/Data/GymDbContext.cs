using Gym_System.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Gym_System.Data
{
    public class GymDbContext : IdentityDbContext<ApplicationUser>

    {

        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<MemberSession> MemberSessions { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<SessionType> SessionTypes { get; set; }

        public DbSet<VisitorSession> VisitorSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Payment>()

                .HasOne(p => p.Member)

                .WithMany(m => m.Payments)

                .HasForeignKey(p => p.MemberId)

                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<MemberSession>()

                .HasOne(s => s.Member)

                .WithMany(m => m.MemberSessions)

                .HasForeignKey(s => s.MemberId)

                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
