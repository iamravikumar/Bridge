using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JobPortal.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(nullable: true),
                    JobDescription = table.Column<string>(nullable: false),
                    JobLocation = table.Column<string>(nullable: false),
                    JobSeekersID = table.Column<string>(nullable: true),
                    JobTitle = table.Column<string>(nullable: false),
                    MaxuimumAnnualSalaryInRupees = table.Column<double>(nullable: false),
                    MinimumAnnualSalaryInRupees = table.Column<double>(nullable: false),
                    PrimaryRole = table.Column<string>(nullable: false),
                    RecruiterID = table.Column<int>(nullable: false),
                    Skills = table.Column<string>(nullable: false),
                    WorkExperience = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
