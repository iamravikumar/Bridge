using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JobPortal.Migrations.JobSeekerDb
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobSeekers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountType = table.Column<string>(nullable: true),
                    Achivements = table.Column<string>(nullable: true),
                    AppliedJobs = table.Column<string>(nullable: true),
                    ConfirmPassword = table.Column<string>(nullable: false),
                    CurrentLocation = table.Column<string>(nullable: true),
                    EmailID = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    HighestQualification = table.Column<string>(nullable: true),
                    HighestQualificationCollege = table.Column<string>(nullable: true),
                    HighestQualificationCompletionTime = table.Column<DateTime>(nullable: false),
                    HighestQualificationStartingTime = table.Column<DateTime>(nullable: false),
                    HighestQualificationSubject = table.Column<string>(nullable: true),
                    Industry = table.Column<string>(nullable: true),
                    JobLocation = table.Column<string>(nullable: true),
                    JobRole = table.Column<string>(nullable: true),
                    KeySkills = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    LinkedInLink = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(maxLength: 10, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    ResumeLink = table.Column<string>(nullable: true),
                    ShortBio = table.Column<string>(nullable: true),
                    TotalExperience = table.Column<string>(nullable: true),
                    WebsiteLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekers", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobSeekers");
        }
    }
}
