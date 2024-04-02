using Microsoft.EntityFrameworkCore;
using DatabaseEntityLib;
using System.Reflection.Emit;

namespace DataBaseContext
{
    public class DB_Context_Class : DbContext
    {
        public DbSet<Predmet> Predmet { get; set; }
        public DbSet<Pitanje> Pitanje { get; set; }
        public DbSet<Oblast> Oblast { get; set; }
        public DB_Context_Class(DbContextOptions<DB_Context_Class> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Predmet>()
                .Property(p => p.NazivPredmeta)
                .IsRequired();


            modelBuilder.Entity<Pitanje>()
                .Property(u => u.NivoTezine)
                .IsRequired();
            modelBuilder.Entity<Pitanje>()
                .Property(u => u.PitanjeSlika)
                .IsRequired();
            modelBuilder.Entity<Pitanje>()
                .Property(u => u.OdgovorSlika)
                .IsRequired();
            modelBuilder.Entity<Pitanje>()
               .Property(u => u.OdgovorTekst)
               .IsRequired();
            modelBuilder.Entity<Pitanje>()
              .Property(u => u.OblastID)
              .IsRequired();
            modelBuilder.Entity<Pitanje>()
               .Property(u => u.PredmetID)
               .IsRequired();


            modelBuilder.Entity<Oblast>()
               .Property(o => o.Name)
               .IsRequired();
            modelBuilder.Entity<Oblast>()
               .Property(o => o.PredmetID)
               .IsRequired();


            modelBuilder.Entity<Predmet>()
                .HasMany(s => s.Pitanja).WithOne(p => p.Predmet).HasForeignKey(p => p.PredmetID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Predmet>()
                .HasMany(s => s.Oblasti).WithOne(p => p.Predmet).HasForeignKey(p => p.PredmetID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Oblast>()
                .HasMany(s => s.OblastPredmeta).WithOne(p => p.Oblast).HasForeignKey(p => p.OblastID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}