using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShopWebApi.Migrations
{
    public partial class AddReservationBank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Reservations_ReservationId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ReservationId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Books");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDateTime",
                table: "Reservations",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ExpireDateTime",
                table: "Reservations",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "ReservationBank",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationBank", x => new { x.ReservationId, x.BookId });
                    table.ForeignKey(
                        name: "FK_ReservationBank_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationBank_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationBank_BookId",
                table: "ReservationBank",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationBank");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ExpireDateTime",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_ReservationId",
                table: "Books",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Reservations_ReservationId",
                table: "Books",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }
    }
}
