using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace MyWebApi
{
    public partial class MyWebDbContext : DbContext
    {
        public MyWebDbContext()
        {
        }


        public MyWebDbContext(SqlConnection connection, string accesstocken)
        {
            connection.AccessToken = accesstocken;          

        }

        public MyWebDbContext(DbContextOptions<MyWebDbContext> options)
            : base(options)
        {
            var conn = (Microsoft.Data.SqlClient.SqlConnection)Database.GetDbConnection();
            var prov = new Microsoft.Azure.Services.AppAuthentication.AzureServiceTokenProvider();
            conn.AccessToken = prov.GetAccessTokenAsync("https://database.windows.net/").Result;
        }

        public virtual DbSet<Line> Line { get; set; }
        public virtual DbSet<Section> Section { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=akazuresql.database.windows.net;Database=MyWebDb;User Id=alexanderKa;Password=GErjzkeNU9D&;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Line>(entity =>
            {
                entity.ToTable("line");

                entity.Property(e => e.LineId)
                    .HasColumnName("line_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Linetext)
                    .HasColumnName("linetext")
                    .HasMaxLength(4000);

                entity.Property(e => e.Linetitel)
                    .HasColumnName("linetitel")
                    .HasMaxLength(512);

                entity.Property(e => e.SectionId).HasColumnName("section_id");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Line)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_line_section");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("section");

                entity.Property(e => e.SectionId)
                    .HasColumnName("section_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Section1)
                    .HasColumnName("section")
                    .HasMaxLength(4000);

                entity.Property(e => e.Timeintervall)
                    .HasColumnName("timeintervall")
                    .HasMaxLength(512);

                entity.Property(e => e.TopicId).HasColumnName("topic_id");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Section)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_section_topic");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("topic");

                entity.Property(e => e.TopicId)
                    .HasColumnName("topic_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Topic1)
                    .HasColumnName("topic")
                    .HasMaxLength(512);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
