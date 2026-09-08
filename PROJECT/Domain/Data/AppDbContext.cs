using System;
using System.Collections.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppliedJob> AppliedJobs { get; set; }

    public virtual DbSet<AuthUser> AuthUsers { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyUser> CompanyUsers { get; set; }

    public virtual DbSet<Experience> Experiences { get; set; }

    public virtual DbSet<Industry> Industries { get; set; }

    public virtual DbSet<InterviewSchedule> InterviewSchedules { get; set; }

    public virtual DbSet<JobApplication> JobApplications { get; set; }

    public virtual DbSet<JobCategory> JobCategories { get; set; }

    public virtual DbSet<JobPost> JobPosts { get; set; }

    public virtual DbSet<JobProvider> JobProviders { get; set; }

    public virtual DbSet<JobSeeker> JobSeekers { get; set; }

    public virtual DbSet<JobSeekerProfile> JobSeekerProfiles { get; set; }

    public virtual DbSet<JobType> JobTypes { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Qualification> Qualifications { get; set; }

    public virtual DbSet<SavedJob> SavedJobs { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<SystemUser> SystemUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ASTA;Initial Catalog=Jobportalsystem;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppliedJob>(entity =>
        {
            entity.HasKey(e => e.AppliedJobId).HasName("PK__AppliedJ__AF19BB6B4633EC01");

            entity.ToTable("AppliedJob");

            entity.Property(e => e.AppliedJobId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("AppliedJobID");
            entity.Property(e => e.AppliedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.JobPost).WithMany(p => p.AppliedJobs)
                .HasForeignKey(d => d.JobPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppliedJob_JobPost");

            entity.HasOne(d => d.JobSeekerProfile).WithMany(p => p.AppliedJobs)
                .HasForeignKey(d => d.JobSeekerProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppliedJob_JobSeekerProfile");
        });

        modelBuilder.Entity<AuthUser>(entity =>
        {
            entity.HasKey(e => e.AuthUserId).HasName("PK__AuthUser__7CD892D45BEA25B7");

            entity.ToTable("AuthUser");

            entity.Property(e => e.AuthUserId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("AuthUserID");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.SystemUser).WithMany(p => p.AuthUsers)
                .HasForeignKey(d => d.SystemUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuthUser_SystemUser");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Companie__2D971C4CF65B7287");

            entity.Property(e => e.CompanyId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CompanyID");
            entity.Property(e => e.ComapnyName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.IndustryId).HasColumnName("IndustryID");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Website)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Industry).WithMany(p => p.Companies)
                .HasForeignKey(d => d.IndustryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Companies_Industry");

            entity.HasOne(d => d.Location).WithMany(p => p.Companies)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Companies_Location");
        });

        modelBuilder.Entity<CompanyUser>(entity =>
        {
            entity.HasKey(e => e.CompanyUserId).HasName("PK__CompanyU__97221D13131A5E26");

            entity.ToTable("CompanyUser");

            entity.Property(e => e.CompanyUserId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CompanyUserID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyUsers)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyUser_Companies");

            entity.HasOne(d => d.JobProvider).WithMany(p => p.CompanyUsers)
                .HasForeignKey(d => d.JobProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyUser_JobProvider");
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.HasKey(e => e.ExperienceId).HasName("PK__Experien__2F4E3469B88382B4");

            entity.ToTable("Experience");

            entity.Property(e => e.ExperienceId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ExperienceID");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Industry>(entity =>
        {
            entity.HasKey(e => e.IndustryId).HasName("PK__Industry__808DEC2C1A76650D");

            entity.ToTable("Industry");

            entity.Property(e => e.IndustryId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("IndustryID");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<InterviewSchedule>(entity =>
        {
            entity.HasKey(e => e.InterviewScheduleId).HasName("PK__Intervie__A9EEDAB582218D17");

            entity.ToTable("InterviewSchedule");

            entity.Property(e => e.InterviewScheduleId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("InterviewScheduleID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MeetingLinkOrvenue)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("MeetingLinkORvenue");
            entity.Property(e => e.ScheduledEndTime).HasColumnType("datetime");
            entity.Property(e => e.ScheduledStartTime).HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.JobApplication).WithMany(p => p.InterviewSchedules)
                .HasForeignKey(d => d.JobApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InterviewSchedule_JobApplications");

            entity.HasOne(d => d.JobProvider).WithMany(p => p.InterviewSchedules)
                .HasForeignKey(d => d.JobProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InterviewSchedule_JobProvider");
        });

        modelBuilder.Entity<JobApplication>(entity =>
        {
            entity.HasKey(e => e.JobApplicationId).HasName("PK__JobAppli__BD557FE59A82D195");

            entity.Property(e => e.JobApplicationId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("JobApplicationID");
            entity.Property(e => e.ApplicationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AppliedJob).WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.AppliedJobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobApplication_Appliedjob");

            entity.HasOne(d => d.JobPost).WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.JobPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobApplication_JobPost");
        });

        modelBuilder.Entity<JobCategory>(entity =>
        {
            entity.HasKey(e => e.JobCategoryId).HasName("PK__JobCateg__302BAD2DFBF66B05");

            entity.ToTable("JobCategory");

            entity.Property(e => e.JobCategoryId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<JobPost>(entity =>
        {
            entity.HasKey(e => e.JobPostId).HasName("PK__JobPost__57689C5A6950E97B");

            entity.ToTable("JobPost");

            entity.Property(e => e.JobPostId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("JobPostID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeadLine).HasColumnType("datetime");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.JobPosts)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobPost_Companies");

            entity.HasOne(d => d.JobProvider).WithMany(p => p.JobPosts)
                .HasForeignKey(d => d.JobProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobPost_JobProvider");

            entity.HasOne(d => d.JobType).WithMany(p => p.JobPosts)
                .HasForeignKey(d => d.JobTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobPost_JobType");

            entity.HasOne(d => d.Jobcategory).WithMany(p => p.JobPosts)
                .HasForeignKey(d => d.JobcategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobPost_JobCategory");

            entity.HasOne(d => d.Location).WithMany(p => p.JobPosts)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobPost_Location");
        });

        modelBuilder.Entity<JobProvider>(entity =>
        {
            entity.HasKey(e => e.JobProviderId).HasName("PK__JobProvi__A7DA6C44C42F7DB3");

            entity.ToTable("JobProvider");

            entity.Property(e => e.JobProviderId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("JobProviderID");
            entity.Property(e => e.SystemUserId).HasColumnName("SystemUserID");

            entity.HasOne(d => d.SystemUser).WithMany(p => p.JobProviders)
                .HasForeignKey(d => d.SystemUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobProvider_SystemUser");
        });

        modelBuilder.Entity<JobSeeker>(entity =>
        {
            entity.HasKey(e => e.JobSeekerId).HasName("PK__JobSeeke__89113A8CA7A8827E");

            entity.ToTable("JobSeeker");

            entity.Property(e => e.JobSeekerId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("JobSeekerID");
            entity.Property(e => e.SystemUserId).HasColumnName("SystemUserID");

            entity.HasOne(d => d.SystemUser).WithMany(p => p.JobSeekers)
                .HasForeignKey(d => d.SystemUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobSeeker_SystemUser");
        });

        modelBuilder.Entity<JobSeekerProfile>(entity =>
        {
            entity.HasKey(e => e.JobSeekerProfileId).HasName("PK__JobSeeke__AB9350DC3CF7A2D2");

            entity.ToTable("JobSeekerProfile");

            entity.Property(e => e.JobSeekerProfileId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("JobSeekerProfileID");
            entity.Property(e => e.About).IsUnicode(false);
            entity.Property(e => e.JobSeekerId).HasColumnName("JobSeekerID");
            entity.Property(e => e.ResumeUrl)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Experience).WithMany(p => p.JobSeekerProfiles)
                .HasForeignKey(d => d.ExperienceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobSeekerProfile_Experience");

            entity.HasOne(d => d.JobSeeker).WithMany(p => p.JobSeekerProfiles)
                .HasForeignKey(d => d.JobSeekerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobSeekerProfile_JobSeeker");

            entity.HasOne(d => d.Location).WithMany(p => p.JobSeekerProfiles)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobSeekerProfile_Location");

            entity.HasOne(d => d.Qualification).WithMany(p => p.JobSeekerProfiles)
                .HasForeignKey(d => d.QualificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobSeekerProfile_Qualification");

            entity.HasOne(d => d.Skill).WithMany(p => p.JobSeekerProfiles)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobSeekerProfile_Skill");
        });

        modelBuilder.Entity<JobType>(entity =>
        {
            entity.HasKey(e => e.JobTypeId).HasName("PK__JobType__E1F4624D2C4B381B");

            entity.ToTable("JobType");

            entity.Property(e => e.JobTypeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("JobTypeID");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA4775C8CD05F");

            entity.ToTable("Location");

            entity.Property(e => e.LocationId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("LocationID");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Qualification>(entity =>
        {
            entity.HasKey(e => e.QualificationId).HasName("PK__Qualific__C95C128A54885D72");

            entity.ToTable("Qualification");

            entity.Property(e => e.QualificationId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("QualificationID");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SavedJob>(entity =>
        {
            entity.HasKey(e => e.SavedJobId).HasName("PK__SavedJob__B7E5F37741BE66E4");

            entity.ToTable("SavedJob");

            entity.Property(e => e.SavedJobId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("SavedJobID");
            entity.Property(e => e.SavedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.JobPost).WithMany(p => p.SavedJobs)
                .HasForeignKey(d => d.JobPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SavedJob_JobPost");

            entity.HasOne(d => d.JobSeekerProfile).WithMany(p => p.SavedJobs)
                .HasForeignKey(d => d.JobSeekerProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SavedJob_JobSeekerProfile");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("PK__Skill__DFA091E76A225C6D");

            entity.ToTable("Skill");

            entity.Property(e => e.SkillId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("SkillID");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.SkillName)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SystemUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemUs__3214EC27995361F0");

            entity.ToTable("SystemUser");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
