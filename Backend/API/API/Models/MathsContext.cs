using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace API.Models
{
    public partial class MathsContext : DbContext
    {
        public MathsContext()
        {
        }

        public MathsContext(DbContextOptions<MathsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Detail> Details { get; set; }
        public virtual DbSet<Institutii> Institutiis { get; set; }
        public virtual DbSet<Rezolvari> Rezolvaris { get; set; }
        public virtual DbSet<Subiecte> Subiectes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:math-seerver.database.windows.net,1433;Initial Catalog=Maths;Persist Security Info=False;User ID=raulvega;Password=@Vegihno123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Detail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Experience).IsRequired();

                entity.Property(e => e.Me).IsRequired();

                entity.Property(e => e.Work).IsRequired();
            });

            modelBuilder.Entity<Institutii>(entity =>
            {
                entity.ToTable("Institutii");

                entity.Property(e => e.Adresa)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Categorie).HasMaxLength(100);

                entity.Property(e => e.Descriere)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.Imagine)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Nume)
                    .IsRequired()
                    .HasMaxLength(400);
            });

            modelBuilder.Entity<Rezolvari>(entity =>
            {
                entity.ToTable("Rezolvari");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.FileType).HasMaxLength(500);

                entity.Property(e => e.IdInstitutie).HasColumnName("Id_Institutie");

                entity.Property(e => e.Nume).HasMaxLength(500);

                entity.HasOne(d => d.IdInstitutieNavigation)
                    .WithMany(p => p.Rezolvaris)
                    .HasForeignKey(d => d.IdInstitutie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rezolvari_Institutii");
            });

            modelBuilder.Entity<Subiecte>(entity =>
            {
                entity.ToTable("Subiecte");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FileType).HasMaxLength(500);

                entity.Property(e => e.IdInstitutie).HasColumnName("Id_Institutie");

                entity.Property(e => e.Nume).HasMaxLength(500);

                entity.HasOne(d => d.IdInstitutieNavigation)
                    .WithMany(p => p.Subiectes)
                    .HasForeignKey(d => d.IdInstitutie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subiecte_Institutii");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
