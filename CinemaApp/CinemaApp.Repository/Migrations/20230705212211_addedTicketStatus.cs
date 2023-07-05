using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaApp.Repository.Migrations
{
    public partial class addedTicketStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_OwnerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketInOrders_Order_OrderId",
                table: "TicketInOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_Order_OwnerId",
                table: "Orders",
                newName: "IX_Orders_OwnerId");

            migrationBuilder.AddColumn<int>(
                name: "TicketStatus",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_OwnerId",
                table: "Orders",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInOrders_Orders_OrderId",
                table: "TicketInOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_OwnerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketInOrders_Orders_OrderId",
                table: "TicketInOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TicketStatus",
                table: "Tickets");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OwnerId",
                table: "Order",
                newName: "IX_Order_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_OwnerId",
                table: "Order",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInOrders_Order_OrderId",
                table: "TicketInOrders",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
