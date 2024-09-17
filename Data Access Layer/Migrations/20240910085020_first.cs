using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseGroup",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseGroup", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpenseGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseGroup_ExpenseGroupId",
                        column: x => x.ExpenseGroupId,
                        principalTable: "ExpenseGroup",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserExpenseGroup",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExpenseGroup", x => new { x.GroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserExpenseGroup_ExpenseGroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ExpenseGroup",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserExpenseGroup_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseShare",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseShare", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseShare_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("2c5dbe1c-7c76-4092-9646-044590a03fb4"), "swapnil@mail.com", false, "Swapnil", "deShkG1VmAgpwRunldaazYxyZ4qaSZL4fDID74wjHhUaOLkv" },
                    { new Guid("56228dd4-476f-4b78-8ff2-02fe0d19605c"), "madhur@mail.com", false, "Madhur", "deShkG1VmAgpwRunldaazYxyZ4qaSZL4fDID74wjHhUaOLkv" },
                    { new Guid("6da84bd1-877f-4d9c-b724-e6438bce5036"), "ryan@mail.com", false, "Ryan", "deShkG1VmAgpwRunldaazYxyZ4qaSZL4fDID74wjHhUaOLkv" },
                    { new Guid("78fbedff-11bd-4891-882c-3bc160bf1077"), "shivansh@mail.com", false, "Shivansh", "deShkG1VmAgpwRunldaazYxyZ4qaSZL4fDID74wjHhUaOLkv" },
                    { new Guid("bc8ca678-2dce-43c9-a568-72d53485c0f2"), "rishhav@mail.com", false, "Rishav", "deShkG1VmAgpwRunldaazYxyZ4qaSZL4fDID74wjHhUaOLkv" },
                    { new Guid("cfe0f938-ed43-4f6e-ba79-50b020ad48d1"), "sukhman@mail.com", true, "Sukhman", "deShkG1VmAgpwRunldaazYxyZ4qaSZL4fDID74wjHhUaOLkv" },
                    { new Guid("d536f330-75e0-40bb-85f0-1382e52068fb"), "pratham@mail.com", false, "Pratham", "deShkG1VmAgpwRunldaazYxyZ4qaSZL4fDID74wjHhUaOLkv" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseGroupId",
                table: "Expenses",
                column: "ExpenseGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseShare_ExpenseId",
                table: "ExpenseShare",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExpenseGroup_UserId",
                table: "UserExpenseGroup",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseShare");

            migrationBuilder.DropTable(
                name: "UserExpenseGroup");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "ExpenseGroup");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
