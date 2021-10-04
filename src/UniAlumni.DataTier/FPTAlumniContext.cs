using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class FPTAlumniContext : DbContext
    {
        private IConfiguration _config;
        public FPTAlumniContext(IConfiguration configuration)
        {
            _config = configuration;
        }

        public FPTAlumniContext(DbContextOptions<FPTAlumniContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlumniGroup> AlumniGroups { get; set; }
        public virtual DbSet<Alumnus> Alumni { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
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
        public virtual DbSet<UniversityMajor> UniversityMajors { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=52.230.81.77; Database=FPTAlumni; Trusted_Connection = False;User Id=sa;Password=FPT@123");
                optionsBuilder.UseSqlServer(_config.GetConnectionString("UniAlumni"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AlumniGroup>(entity =>
            {
                entity.HasKey(e => new { e.AlumniId, e.GroupId })
                    .HasName("PK__AlumniGr__227F8B5CCBD17A38");

                entity.ToTable("AlumniGroup");

                entity.Property(e => e.RegisteredDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

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
                entity.HasIndex(e => e.Phone, "UQ__Alumni__5C7E359E27655E6B")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Alumni__A9D10534D1C935CC")
                    .IsUnique();

                entity.Property(e => e.AboutMe).HasMaxLength(500);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DoB).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.Job).HasMaxLength(70);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Alumni)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__Alumni__CompanyI__3C69FB99");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Alumni)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Alumni_Major");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.Business)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.Banner).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EventContent).IsRequired();

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.RegistrationEndDate).HasColumnType("datetime");

                entity.Property(e => e.RegistrationStartDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Event__GroupId__4D94879B");
            });

            modelBuilder.Entity<EventRegistration>(entity =>
            {
                entity.HasKey(e => new { e.AlumniId, e.EventId })
                    .HasName("PK__EventReg__84A268EBD1435ABC");

                entity.ToTable("EventRegistration");

                entity.Property(e => e.RegisteredDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

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
                entity.ToTable("FPTStudentInfo");

                entity.Property(e => e.Class)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.EduMail).HasMaxLength(35);

                entity.Property(e => e.Major).HasMaxLength(50);

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.Banner).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.GroupLeader)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.GroupLeaderId)
                    .HasConstraintName("FK__Group__GroupLead__4316F928");

                entity.HasOne(d => d.ParentGroup)
                    .WithMany(p => p.InverseParentGroup)
                    .HasForeignKey(d => d.ParentGroupId)
                    .HasConstraintName("FK__Group__ParentGro__44FF419A");

                entity.HasOne(d => d.UniversityMajor)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.UniversityMajorId)
                    .HasConstraintName("FK_Group_UniversityMajor");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.ToTable("Major");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.VietnameseName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.Banner).HasMaxLength(200);

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

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
                entity.ToTable("Recruitment");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ExperienceLevel).HasMaxLength(70);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

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
                entity.ToTable("Referral");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.VoucherCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Nominator)
                    .WithMany(p => p.Referrals)
                    .HasForeignKey(d => d.NominatorId)
                    .HasConstraintName("FK__Referral__Nomina__59FA5E80");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.Referrals)
                    .HasForeignKey(d => d.VoucherId)
                    .HasConstraintName("FK__Referral__Vouche__5AEE82B9");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.Tagname)
                    .IsRequired()
                    .HasMaxLength(70);
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
                entity.ToTable("University");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Logo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<UniversityMajor>(entity =>
            {
                entity.ToTable("UniversityMajor");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.UniversityMajors)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UniversityMajor_Major");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.UniversityMajors)
                    .HasForeignKey(d => d.UniversityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UniversityMajor_University");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.ToTable("Voucher");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DiscountValue).HasColumnType("decimal(3, 2)");

                entity.Property(e => e.RelationshipName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

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
