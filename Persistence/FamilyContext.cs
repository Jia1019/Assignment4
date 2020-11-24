using Microsoft.EntityFrameworkCore;
using Models;

namespace WebApplication.Persistence
{
    public class FamilyContext : DbContext
    {
        public DbSet<Adult> Adults { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Pet> Pets { get; set; }
        
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // name of database
            optionsBuilder.UseSqlite("Data Source = Families.db");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ChildInterest>()
                .HasKey(sc => 
                    new
                    {
                        sc.ChildID, 
                        sc.InterestType
                    }
                );

            modelBuilder.Entity<ChildInterest>()
                .HasOne(childInterest => childInterest.Child)
                .WithMany(child => child.ChildInterests)
                .HasForeignKey(childInterest => childInterest.ChildID);
            
            modelBuilder.Entity<ChildInterest>()
                .HasOne(childInterest => childInterest.Interest)
                .WithMany(interest => interest.ChildInterests)
                .HasForeignKey(childInterest => childInterest.InterestType);
            
            modelBuilder.Entity<FamilyAdult>()
                .HasKey(sc => 
                    new
                    {
                        sc.FamilyID, 
                        sc.AdultID
                    }
                );

            modelBuilder.Entity<FamilyAdult>()
                .HasOne(familyAdult => familyAdult.Family)
                .WithMany(family => family.Adults)
                .HasForeignKey(familyAdult => familyAdult.FamilyID);
            
            modelBuilder.Entity<FamilyAdult>()
                .HasOne(familyAdult => familyAdult.Adult)
                .WithMany(adult => adult.FamilyAdults)
                .HasForeignKey(familyAdult => familyAdult.AdultID);
            
            modelBuilder.Entity<FamilyChild>()
                .HasKey(sc => 
                    new
                    {
                        sc.FamilyID, 
                        sc.ChildID
                    }
                );

            modelBuilder.Entity<FamilyChild>()
                .HasOne(familyChild => familyChild.Family)
                .WithMany(family => family.Children)
                .HasForeignKey(familyChild => familyChild.FamilyID);
            
            modelBuilder.Entity<FamilyChild>()
                .HasOne(familyChild => familyChild.Child)
                .WithMany(child => child.FamilyChildren)
                .HasForeignKey(familyChild => familyChild.ChildID);
        }
    }
}