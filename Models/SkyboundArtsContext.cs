using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace buttoncheckDevAPI.Models
{
    public partial class SkyboundArtsContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public SkyboundArtsContext()
        {
        }

        public SkyboundArtsContext(DbContextOptions<SkyboundArtsContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<Characters> Characters { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<MapVideoTag> MapVideoTag { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<VideoTags> VideoTags { get; set; }
        public virtual DbSet<Videos> Videos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration["Data:ConnectionString"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Characters>(entity =>
            {
                entity.HasKey(e => e.CharacterId)
                    .HasName("PK__Characte__757BCA404A43B10D");

                entity.HasIndex(e => e.CharacterName)
                    .HasName("UQ__Characte__7417733A1B1CC96F")
                    .IsUnique();

                entity.Property(e => e.CharacterId).HasColumnName("CharacterID");

                entity.Property(e => e.CharacterName)
                    .IsRequired()
                    .HasColumnName("Character_Name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.HasKey(e => e.EventId)
                    .HasName("PK__Events__7944C87043B8455F");

                entity.HasIndex(e => e.EventName)
                    .HasName("UQ__Events__C46EAE6E77319A46")
                    .IsUnique();

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasColumnName("Event_Name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<MapVideoTag>(entity =>
            {
                entity.HasKey(e => e.MapId)
                    .HasName("PK__MapVideo__3265E2FB6DC8E541");

                entity.Property(e => e.MapId).HasColumnName("MapID");

                entity.Property(e => e.TagId).HasColumnName("Tag_ID");

                entity.Property(e => e.VideoId).HasColumnName("Video_ID");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.MapVideoTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MapVideoT__Tag_I__5DCAEF64");

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.MapVideoTag)
                    .HasForeignKey(d => d.VideoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MapVideoT__Video__5CD6CB2B");
            });

            modelBuilder.Entity<Players>(entity =>
            {
                entity.HasKey(e => e.PlayerId)
                    .HasName("PK__Players__4A4E74A8C999F68A");

                entity.HasIndex(e => e.PlayerName)
                    .HasName("UQ__Players__41ECEB32E3DE5057")
                    .IsUnique();

                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.PlayerName)
                    .IsRequired()
                    .HasColumnName("Player_Name")
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<VideoTags>(entity =>
            {
                entity.HasKey(e => e.TagId)
                    .HasName("PK__VideoTag__657CFA4C76C1C575");

                entity.HasIndex(e => e.TagName)
                    .HasName("UQ__VideoTag__344C2E03D969E82A")
                    .IsUnique();

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasColumnName("Tag_Name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Videos>(entity =>
            {
                entity.HasKey(e => e.VideoId)
                    .HasName("PK__Videos__BAE5124A3547EC7F");

                entity.Property(e => e.VideoId).HasColumnName("VideoID");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.P1Character)
                    .IsRequired()
                    .HasColumnName("P1_Character")
                    .HasMaxLength(50);

                entity.Property(e => e.P1Player)
                    .IsRequired()
                    .HasColumnName("P1_Player")
                    .HasMaxLength(60);

                entity.Property(e => e.P2Character)
                    .IsRequired()
                    .HasColumnName("P2_Character")
                    .HasMaxLength(50);

                entity.Property(e => e.P2Player)
                    .IsRequired()
                    .HasColumnName("P2_Player")
                    .HasMaxLength(60);

                entity.Property(e => e.VideoLink)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(e => e.WinnerCharacter)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.WinnerPlayer)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.HasOne(d => d.P1CharacterNavigation)
                    .WithMany(p => p.VideosP1CharacterNavigation)
                    .HasPrincipalKey(p => p.CharacterName)
                    .HasForeignKey(d => d.P1Character)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Videos__P1_Chara__398D8EEE");

                entity.HasOne(d => d.P2CharacterNavigation)
                    .WithMany(p => p.VideosP2CharacterNavigation)
                    .HasPrincipalKey(p => p.CharacterName)
                    .HasForeignKey(d => d.P2Character)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Videos__P2_Chara__3A81B327");

                entity.HasOne(d => d.WinnerCharacterNavigation)
                    .WithMany(p => p.VideosWinnerCharacterNavigation)
                    .HasPrincipalKey(p => p.CharacterName)
                    .HasForeignKey(d => d.WinnerCharacter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Videos__WinnerCh__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
