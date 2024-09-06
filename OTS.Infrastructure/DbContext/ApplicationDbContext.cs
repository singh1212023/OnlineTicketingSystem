using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OTS.Core.Entities;
using OTS.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Infrastructure.Data
{
    

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("AspNetUser").Property(p => p.Id).HasColumnName("Id");
            builder.Entity<ApplicationUserRole>().ToTable("AspNetUserRole");
            builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogin");
            builder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaim");
            builder.Entity<ApplicationRole>().ToTable("AspNetRole");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaim");

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Conversation>()
            .HasOne(c => c.Ticket)
            .WithMany(t => t.Conversations)
            .HasForeignKey(c => c.TicketId)
            .OnDelete(DeleteBehavior.Cascade); 

            builder.Entity<Conversation>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.RequestorId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Attachment>()
                .HasOne(a => a.Ticket)
                .WithMany()
                .HasForeignKey(a => a.TicketId)
                .OnDelete(DeleteBehavior.Cascade); 

            
            builder.Entity<Attachment>()
                .HasOne(a => a.Conversation)
                .WithMany(t => t.Attchments) 
                .HasForeignKey(a => a.ConversationId)
                .OnDelete(DeleteBehavior.NoAction);

            //this.SeedRoles(builder);
        }





        //private void SeedRoles(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<IdentityRole>().HasData
        //        (
        //        new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
        //        new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
        //        );
        //}
        public DbSet<ApplicationUser> AspNetUser { get; set; }
        public DbSet<ApplicationUserRole> AspNetUserRole { get; set; }
        public DbSet<ApplicationRole> AspNetRole { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Conversation> Conversation { get; set; }
        public DbSet<Attachment> Attachment { get; set; }
        //public DbSet<IdentityRole> Attachment { get; set; }
    }
}

//IdentityDbContext<ApplicationUser, ApplicationUserRole, ApplicationRole, string, IdentityUserClaim<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>