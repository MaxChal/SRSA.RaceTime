using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RaceTime.CoreAPI.Models
{
    public partial class RaceTimeContext : DbContext
    {
        public virtual DbSet<Collisions> Collisions { get; set; }
        public virtual DbSet<Competitors> Competitors { get; set; }
        public virtual DbSet<Drivers> Drivers { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Laps> Laps { get; set; }
        public virtual DbSet<Pitstops> Pitstops { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<Sessions> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=RaceTime;Data Source=MAXCHAL\SQLEXPRESS");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Collisions>(entity =>
            {
                entity.HasKey(e => e.CollisionId)
                    .HasName("PK_Collisions");

                entity.Property(e => e.CollisionId)
                    .HasColumnName("CollisionID")
                    .HasMaxLength(36);

                entity.Property(e => e.Competitor1).HasMaxLength(36);

                entity.Property(e => e.Competitor2).HasMaxLength(36);

                entity.Property(e => e.SessionId)
                    .HasColumnName("SessionID")
                    .HasMaxLength(36);
            });

            modelBuilder.Entity<Competitors>(entity =>
            {
                entity.HasKey(e => e.CompetitorId)
                    .HasName("PK_Competitors");

                entity.Property(e => e.CompetitorId)
                    .HasColumnName("CompetitorID")
                    .HasMaxLength(36);

                entity.Property(e => e.BallastKg).HasColumnName("BallastKG");

                entity.Property(e => e.CarId).HasColumnName("CarID");

                entity.Property(e => e.CarModel).HasMaxLength(50);

                entity.Property(e => e.CarSkin).HasMaxLength(50);

                entity.Property(e => e.ConnectionId).HasColumnName("ConnectionID");

                entity.Property(e => e.CurrentLapStart).HasColumnType("datetime");

                entity.Property(e => e.CurrentTyreCompound).HasMaxLength(50);

                entity.Property(e => e.DriverGuid).HasMaxLength(36);

                entity.Property(e => e.DriverId)
                    .HasColumnName("DriverID")
                    .HasMaxLength(36);

                entity.Property(e => e.DriverTeam).HasMaxLength(50);

                entity.Property(e => e.Gap).HasMaxLength(50);

                entity.Property(e => e.SessionId)
                    .HasColumnName("SessionID")
                    .HasMaxLength(36);
            });

            modelBuilder.Entity<Drivers>(entity =>
            {
                entity.HasKey(e => e.DriverId)
                    .HasName("PK_Drivers");

                entity.Property(e => e.DriverId)
                    .HasColumnName("DriverID")
                    .HasMaxLength(36);

                entity.Property(e => e.DriverLevel).HasMaxLength(50);

                entity.Property(e => e.DriverName).HasMaxLength(50);
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.HasKey(e => e.EventId)
                    .HasName("PK_Events");

                entity.Property(e => e.EventId)
                    .HasColumnName("EventID")
                    .HasMaxLength(36);

                entity.Property(e => e.EventDescription).HasMaxLength(50);

                entity.Property(e => e.EventName).HasMaxLength(50);

                entity.Property(e => e.EventType).HasMaxLength(50);

                entity.Property(e => e.SeriesId)
                    .HasColumnName("SeriesID")
                    .HasMaxLength(36);
            });

            modelBuilder.Entity<Laps>(entity =>
            {
                entity.HasKey(e => e.LapId)
                    .HasName("PK_Laps");

                entity.Property(e => e.LapId)
                    .HasColumnName("LapID")
                    .HasMaxLength(36);

                entity.Property(e => e.CompetitorId)
                    .HasColumnName("CompetitorID")
                    .HasMaxLength(36);

                entity.Property(e => e.ConnectionId).HasColumnName("ConnectionID");

                entity.Property(e => e.TyreCompound).HasMaxLength(50);
            });

            modelBuilder.Entity<Pitstops>(entity =>
            {
                entity.HasKey(e => e.PitstopId)
                    .HasName("PK_Pitstops");

                entity.Property(e => e.PitstopId)
                    .HasColumnName("PitstopID")
                    .HasMaxLength(36);

                entity.Property(e => e.CompetitorId)
                    .HasColumnName("CompetitorID")
                    .HasMaxLength(36);
            });

            modelBuilder.Entity<Series>(entity =>
            {
                entity.Property(e => e.SeriesId)
                    .HasColumnName("SeriesID")
                    .HasMaxLength(36);

                entity.Property(e => e.SeriesDescription).HasMaxLength(50);

                entity.Property(e => e.SeriesName).HasMaxLength(50);

                entity.Property(e => e.SeriesType).HasMaxLength(50);
            });

            modelBuilder.Entity<Sessions>(entity =>
            {
                entity.HasKey(e => e.SessionId)
                    .HasName("PK_Sessions");

                entity.Property(e => e.SessionId)
                    .HasColumnName("SessionID")
                    .HasMaxLength(36);

                entity.Property(e => e.ElapsedMs).HasColumnName("ElapsedMS");

                entity.Property(e => e.EventId)
                    .HasColumnName("EventID")
                    .HasMaxLength(36);

                entity.Property(e => e.ServerName).HasMaxLength(50);

                entity.Property(e => e.SessionName).HasMaxLength(50);

                entity.Property(e => e.SessionTrack).HasMaxLength(50);

                entity.Property(e => e.Weather).HasMaxLength(50);
            });
        }
    }
}