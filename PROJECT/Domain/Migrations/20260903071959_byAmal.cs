using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class byAmal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Experience",
                columns: table => new
                {
                    ExperienceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Experien__2F4E3469B88382B4", x => x.ExperienceID);
                });

            migrationBuilder.CreateTable(
                name: "Industry",
                columns: table => new
                {
                    IndustryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Industry__808DEC2C1A76650D", x => x.IndustryID);
                });

            migrationBuilder.CreateTable(
                name: "JobCategory",
                columns: table => new
                {
                    JobCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__JobCateg__302BAD2DFBF66B05", x => x.JobCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "JobType",
                columns: table => new
                {
                    JobTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__JobType__E1F4624D2C4B381B", x => x.JobTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Location__E7FEA4775C8CD05F", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "Qualification",
                columns: table => new
                {
                    QualificationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Qualific__C95C128A54885D72", x => x.QualificationID);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SkillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    SkillName = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Skill__DFA091E76A225C6D", x => x.SkillID);
                });

            migrationBuilder.CreateTable(
                name: "SystemUser",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    FirstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Phone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Roles = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SystemUs__3214EC27995361F0", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IndustryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComapnyName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Website = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Companie__2D971C4CF65B7287", x => x.CompanyID);
                    table.ForeignKey(
                        name: "FK_Companies_Industry",
                        column: x => x.IndustryID,
                        principalTable: "Industry",
                        principalColumn: "IndustryID");
                    table.ForeignKey(
                        name: "FK_Companies_Location",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationID");
                });

            migrationBuilder.CreateTable(
                name: "AuthUser",
                columns: table => new
                {
                    AuthUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    PasswordHash = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    SystemUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AuthUser__7CD892D45BEA25B7", x => x.AuthUserID);
                    table.ForeignKey(
                        name: "FK_AuthUser_SystemUser",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUser",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "JobProvider",
                columns: table => new
                {
                    JobProviderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    SystemUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__JobProvi__A7DA6C44C42F7DB3", x => x.JobProviderID);
                    table.ForeignKey(
                        name: "FK_JobProvider_SystemUser",
                        column: x => x.SystemUserID,
                        principalTable: "SystemUser",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "JobSeeker",
                columns: table => new
                {
                    JobSeekerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    SystemUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__JobSeeke__89113A8CA7A8827E", x => x.JobSeekerID);
                    table.ForeignKey(
                        name: "FK_JobSeeker_SystemUser",
                        column: x => x.SystemUserID,
                        principalTable: "SystemUser",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CompanyUser",
                columns: table => new
                {
                    CompanyUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CompanyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Roles = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CompanyU__97221D13131A5E26", x => x.CompanyUserID);
                    table.ForeignKey(
                        name: "FK_CompanyUser_Companies",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK_CompanyUser_JobProvider",
                        column: x => x.JobProviderId,
                        principalTable: "JobProvider",
                        principalColumn: "JobProviderID");
                });

            migrationBuilder.CreateTable(
                name: "JobPost",
                columns: table => new
                {
                    JobPostID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    JobProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobcategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeadLine = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__JobPost__57689C5A6950E97B", x => x.JobPostID);
                    table.ForeignKey(
                        name: "FK_JobPost_Companies",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK_JobPost_JobCategory",
                        column: x => x.JobcategoryId,
                        principalTable: "JobCategory",
                        principalColumn: "JobCategoryId");
                    table.ForeignKey(
                        name: "FK_JobPost_JobProvider",
                        column: x => x.JobProviderId,
                        principalTable: "JobProvider",
                        principalColumn: "JobProviderID");
                    table.ForeignKey(
                        name: "FK_JobPost_JobType",
                        column: x => x.JobTypeId,
                        principalTable: "JobType",
                        principalColumn: "JobTypeID");
                    table.ForeignKey(
                        name: "FK_JobPost_Location",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationID");
                });

            migrationBuilder.CreateTable(
                name: "JobSeekerProfile",
                columns: table => new
                {
                    JobSeekerProfileID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    JobSeekerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    About = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ResumeUrl = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QualificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__JobSeeke__AB9350DC3CF7A2D2", x => x.JobSeekerProfileID);
                    table.ForeignKey(
                        name: "FK_JobSeekerProfile_Experience",
                        column: x => x.ExperienceId,
                        principalTable: "Experience",
                        principalColumn: "ExperienceID");
                    table.ForeignKey(
                        name: "FK_JobSeekerProfile_JobSeeker",
                        column: x => x.JobSeekerID,
                        principalTable: "JobSeeker",
                        principalColumn: "JobSeekerID");
                    table.ForeignKey(
                        name: "FK_JobSeekerProfile_Location",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationID");
                    table.ForeignKey(
                        name: "FK_JobSeekerProfile_Qualification",
                        column: x => x.QualificationId,
                        principalTable: "Qualification",
                        principalColumn: "QualificationID");
                    table.ForeignKey(
                        name: "FK_JobSeekerProfile_Skill",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "SkillID");
                });

            migrationBuilder.CreateTable(
                name: "AppliedJob",
                columns: table => new
                {
                    AppliedJobID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    JobPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobSeekerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AppliedJ__AF19BB6B4633EC01", x => x.AppliedJobID);
                    table.ForeignKey(
                        name: "FK_AppliedJob_JobPost",
                        column: x => x.JobPostId,
                        principalTable: "JobPost",
                        principalColumn: "JobPostID");
                    table.ForeignKey(
                        name: "FK_AppliedJob_JobSeekerProfile",
                        column: x => x.JobSeekerProfileId,
                        principalTable: "JobSeekerProfile",
                        principalColumn: "JobSeekerProfileID");
                });

            migrationBuilder.CreateTable(
                name: "SavedJob",
                columns: table => new
                {
                    SavedJobID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    JobPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobSeekerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SavedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SavedJob__B7E5F37741BE66E4", x => x.SavedJobID);
                    table.ForeignKey(
                        name: "FK_SavedJob_JobPost",
                        column: x => x.JobPostId,
                        principalTable: "JobPost",
                        principalColumn: "JobPostID");
                    table.ForeignKey(
                        name: "FK_SavedJob_JobSeekerProfile",
                        column: x => x.JobSeekerProfileId,
                        principalTable: "JobSeekerProfile",
                        principalColumn: "JobSeekerProfileID");
                });

            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    JobApplicationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    JobPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppliedJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationStatus = table.Column<int>(type: "int", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__JobAppli__BD557FE59A82D195", x => x.JobApplicationID);
                    table.ForeignKey(
                        name: "FK_JobApplication_Appliedjob",
                        column: x => x.AppliedJobId,
                        principalTable: "AppliedJob",
                        principalColumn: "AppliedJobID");
                    table.ForeignKey(
                        name: "FK_JobApplication_JobPost",
                        column: x => x.JobPostId,
                        principalTable: "JobPost",
                        principalColumn: "JobPostID");
                });

            migrationBuilder.CreateTable(
                name: "InterviewSchedule",
                columns: table => new
                {
                    InterviewScheduleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    JobApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    ScheduledStartTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ScheduledEndTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    InterviewMode = table.Column<int>(type: "int", nullable: false),
                    MeetingLinkORvenue = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Intervie__A9EEDAB582218D17", x => x.InterviewScheduleID);
                    table.ForeignKey(
                        name: "FK_InterviewSchedule_JobApplications",
                        column: x => x.JobApplicationId,
                        principalTable: "JobApplications",
                        principalColumn: "JobApplicationID");
                    table.ForeignKey(
                        name: "FK_InterviewSchedule_JobProvider",
                        column: x => x.JobProviderId,
                        principalTable: "JobProvider",
                        principalColumn: "JobProviderID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedJob_JobPostId",
                table: "AppliedJob",
                column: "JobPostId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedJob_JobSeekerProfileId",
                table: "AppliedJob",
                column: "JobSeekerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthUser_SystemUserId",
                table: "AuthUser",
                column: "SystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_IndustryID",
                table: "Companies",
                column: "IndustryID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LocationId",
                table: "Companies",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUser_CompanyID",
                table: "CompanyUser",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUser_JobProviderId",
                table: "CompanyUser",
                column: "JobProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedule_JobApplicationId",
                table: "InterviewSchedule",
                column: "JobApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedule_JobProviderId",
                table: "InterviewSchedule",
                column: "JobProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_AppliedJobId",
                table: "JobApplications",
                column: "AppliedJobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobPostId",
                table: "JobApplications",
                column: "JobPostId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPost_CompanyID",
                table: "JobPost",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_JobPost_JobcategoryId",
                table: "JobPost",
                column: "JobcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPost_JobProviderId",
                table: "JobPost",
                column: "JobProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPost_JobTypeId",
                table: "JobPost",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPost_LocationId",
                table: "JobPost",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobProvider_SystemUserID",
                table: "JobProvider",
                column: "SystemUserID");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeeker_SystemUserID",
                table: "JobSeeker",
                column: "SystemUserID");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerProfile_ExperienceId",
                table: "JobSeekerProfile",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerProfile_JobSeekerID",
                table: "JobSeekerProfile",
                column: "JobSeekerID");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerProfile_LocationId",
                table: "JobSeekerProfile",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerProfile_QualificationId",
                table: "JobSeekerProfile",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerProfile_SkillId",
                table: "JobSeekerProfile",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedJob_JobPostId",
                table: "SavedJob",
                column: "JobPostId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedJob_JobSeekerProfileId",
                table: "SavedJob",
                column: "JobSeekerProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthUser");

            migrationBuilder.DropTable(
                name: "CompanyUser");

            migrationBuilder.DropTable(
                name: "InterviewSchedule");

            migrationBuilder.DropTable(
                name: "SavedJob");

            migrationBuilder.DropTable(
                name: "JobApplications");

            migrationBuilder.DropTable(
                name: "AppliedJob");

            migrationBuilder.DropTable(
                name: "JobPost");

            migrationBuilder.DropTable(
                name: "JobSeekerProfile");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "JobCategory");

            migrationBuilder.DropTable(
                name: "JobProvider");

            migrationBuilder.DropTable(
                name: "JobType");

            migrationBuilder.DropTable(
                name: "Experience");

            migrationBuilder.DropTable(
                name: "JobSeeker");

            migrationBuilder.DropTable(
                name: "Qualification");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "Industry");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "SystemUser");
        }
    }
}
