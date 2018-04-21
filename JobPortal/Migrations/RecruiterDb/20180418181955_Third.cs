using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JobPortal.Migrations.RecruiterDb
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recruiters",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountType = table.Column<string>(nullable: true),
                    Achivements = table.Column<string>(nullable: true),
                    CompanyAddress = table.Column<string>(nullable: true),
                    CompanyCountry = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    CompanyState = table.Column<string>(nullable: true),
                    CompanywebsiteLink = table.Column<string>(nullable: true),
                    ConfirmPassword = table.Column<string>(nullable: false),
                    CurrentDesignation = table.Column<string>(nullable: true),
                    CurrentLocation = table.Column<string>(nullable: true),
                    EmailID = table.Column<string>(nullable: false),
                    ExperienceInHiring = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    Industry = table.Column<string>(nullable: true),
                    JobsPosted = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    MobileNumber = table.Column<string>(maxLength: 10, nullable: false),
                    NumberOfEmployees = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruiters", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recruiters");
        }
    }
}
