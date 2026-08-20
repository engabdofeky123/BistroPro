using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurant_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReservationSlotId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ReservationSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationSlots", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ReservationSlots",
                columns: new[] { "Id", "EndTime", "IsActive", "StartTime" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 13, 30, 0, 0), true, new TimeSpan(0, 12, 0, 0, 0) },
                    { 2, new TimeSpan(0, 15, 0, 0, 0), true, new TimeSpan(0, 13, 30, 0, 0) },
                    { 3, new TimeSpan(0, 16, 30, 0, 0), true, new TimeSpan(0, 15, 0, 0, 0) },
                    { 4, new TimeSpan(0, 18, 30, 0, 0), true, new TimeSpan(0, 17, 0, 0, 0) },
                    { 5, new TimeSpan(0, 20, 0, 0, 0), true, new TimeSpan(0, 18, 30, 0, 0) },
                    { 6, new TimeSpan(0, 21, 30, 0, 0), true, new TimeSpan(0, 20, 0, 0, 0) },
                    { 7, new TimeSpan(0, 23, 0, 0, 0), true, new TimeSpan(0, 21, 30, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationSlotId",
                table: "Reservations",
                column: "ReservationSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationSlots_ReservationSlotId",
                table: "Reservations",
                column: "ReservationSlotId",
                principalTable: "ReservationSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationSlots_ReservationSlotId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "ReservationSlots");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationSlotId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationSlotId",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
