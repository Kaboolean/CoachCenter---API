using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhoojol.Infrastructure.Migrations
{
    public partial class AddCoach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CoachId",
                table: "Clients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Grades = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coaches_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CoachId",
                table: "Clients",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_UserId",
                table: "Coaches",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Coaches_CoachId",
                table: "Clients",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Coaches_CoachId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CoachId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Clients");
        }
    }
}
