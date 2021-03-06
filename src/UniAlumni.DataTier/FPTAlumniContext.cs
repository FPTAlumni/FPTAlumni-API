using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class FPTAlumniContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public FPTAlumniContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public FPTAlumniContext(DbContextOptions<FPTAlumniContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlumniGroup> AlumniGroups { get; set; }
        public virtual DbSet<Alumnus> Alumni { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassMajor> ClassMajors { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventRegistration> EventRegistrations { get; set; }
        public virtual DbSet<FptstudentInfo> FptstudentInfos { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Recruitment> Recruitments { get; set; }
        public virtual DbSet<Referral> Referrals { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TagNews> TagNews { get; set; }
        public virtual DbSet<University> Universities { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("UniAlumni"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AlumniGroup>(entity =>
            {
                entity.HasKey(e => new { e.AlumniId, e.GroupId });

                entity.Property(e => e.RegisteredDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Alumni)
                    .WithMany(p => p.AlumniGroups)
                    .HasForeignKey(d => d.AlumniId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AlumniGro__Alumn__47DBAE45");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.AlumniGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AlumniGro__Group__48CFD27E");
            });

            modelBuilder.Entity<Alumnus>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.Uid).IsUnicode(false);

                entity.HasOne(d => d.ClassMajor)
                    .WithMany(p => p.Alumni)
                    .HasForeignKey(d => d.ClassMajorId)
                    .HasConstraintName("FK_Alumni_ClassMajor");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Alumni)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__Alumni__CompanyI__3C69FB99");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasOne(d => d.University)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.UniversityId)
                    .HasConstraintName("FK_Class_University");
            });

            modelBuilder.Entity<ClassMajor>(entity =>
            {
                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassMajors)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_ClassMajor_Class");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.ClassMajors)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_ClassMajor_Major");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyName).IsUnicode(false);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Event__GroupId__4D94879B");
            });

            modelBuilder.Entity<EventRegistration>(entity =>
            {
                entity.HasKey(e => new { e.AlumniId, e.EventId })
                    .HasName("PK__EventReg__84A268EBD1435ABC");

                entity.Property(e => e.RegisteredDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Alumni)
                    .WithMany(p => p.EventRegistrations)
                    .HasForeignKey(d => d.AlumniId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EventRegi__Alumn__5070F446");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventRegistrations)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EventRegi__Event__5165187F");
            });

            modelBuilder.Entity<FptstudentInfo>(entity =>
            {
                entity.Property(e => e.Class).IsUnicode(false);

                entity.Property(e => e.StudentId).IsUnicode(false);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.GroupLeader)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.GroupLeaderId)
                    .HasConstraintName("FK_Group_Alumni");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_Group_Major");

                entity.HasOne(d => d.ParentGroup)
                    .WithMany(p => p.InverseParentGroup)
                    .HasForeignKey(d => d.ParentGroupId)
                    .HasConstraintName("FK__Group__ParentGro__44FF419A");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.UniversityId)
                    .HasConstraintName("FK_Group_University");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__News__CategoryId__68487DD7");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__News__GroupId__6754599E");
            });

            modelBuilder.Entity<Recruitment>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.HasOne(d => d.Alumni)
                    .WithMany(p => p.Recruitments)
                    .HasForeignKey(d => d.AlumniId)
                    .HasConstraintName("FK__Recruitme__Alumn__5EBF139D");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Recruitments)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__Recruitme__Compa__60A75C0F");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.RecruitmentGroups)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Recruitme__Group__5FB337D6");

                entity.HasOne(d => d.GroupOrigin)
                    .WithMany(p => p.RecruitmentGroupOrigins)
                    .HasForeignKey(d => d.GroupOriginId)
                    .HasConstraintName("FK__Recruitme__Group__619B8048");
            });

            modelBuilder.Entity<Referral>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ParentPhone).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.VoucherCode).IsUnicode(false);

                entity.HasOne(d => d.Nominator)
                    .WithMany(p => p.Referrals)
                    .HasForeignKey(d => d.NominatorId)
                    .HasConstraintName("FK__Referral__Nomina__59FA5E80");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.Referrals)
                    .HasForeignKey(d => d.VoucherId)
                    .HasConstraintName("FK__Referral__Vouche__5AEE82B9");
            });

            modelBuilder.Entity<TagNews>(entity =>
            {
                entity.HasKey(e => new { e.NewsId, e.TagId })
                    .HasName("PK__TagNews__431972694EF07ADF");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.TagNews)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TagNews__NewsId__6D0D32F4");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TagNews)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TagNews__TagId__6E01572D");
            });

            modelBuilder.Entity<University>(entity =>
            {
                entity.Property(e => e.Logo).IsUnicode(false);
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK__Voucher__MajorId__5629CD9C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
