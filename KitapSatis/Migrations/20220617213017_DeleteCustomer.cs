using Microsoft.EntityFrameworkCore.Migrations;

namespace KitapSatis.Migrations
{
    public partial class DeleteCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communications_Customers_CustomerDetailCustomerId",
                table: "Communications");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Customers_CustomerId",
                table: "CreditCards");

            migrationBuilder.DropTable(
                name: "FavoriteCustomer");

            migrationBuilder.DropTable(
                name: "WeekProducts");

            migrationBuilder.DropTable(
                name: "CustomerDetails");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_CreditCards_CustomerId",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_Communications_CustomerDetailCustomerId",
                table: "Communications");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "CustomerDetailCustomerId",
                table: "Communications");

            migrationBuilder.AlterColumn<int>(
                name: "Stock",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "KindName",
                table: "Kinds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Stock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KindName",
                table: "Kinds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "CreditCards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerDetailCustomerId",
                table: "Communications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirtName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "WeekProducts",
                columns: table => new
                {
                    WeekProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekProducts", x => x.WeekProductId);
                    table.ForeignKey(
                        name: "FK_WeekProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDetails",
                columns: table => new
                {
                    CustomerDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommunicationId = table.Column<int>(type: "int", nullable: false),
                    CreditCardId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryCountry = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDetails", x => x.CustomerDetailId);
                    table.ForeignKey(
                        name: "FK_CustomerDetails_Communications_CommunicationId",
                        column: x => x.CommunicationId,
                        principalTable: "Communications",
                        principalColumn: "CommunicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerDetails_CreditCards_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "CreditCardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerDetails_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteCustomer",
                columns: table => new
                {
                    FavoriteId = table.Column<int>(type: "int", nullable: false),
                    CustomerDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteCustomer", x => new { x.FavoriteId, x.CustomerDetailId });
                    table.ForeignKey(
                        name: "FK_FavoriteCustomer_CustomerDetails_CustomerDetailId",
                        column: x => x.CustomerDetailId,
                        principalTable: "CustomerDetails",
                        principalColumn: "CustomerDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteCustomer_Favorites_FavoriteId",
                        column: x => x.FavoriteId,
                        principalTable: "Favorites",
                        principalColumn: "FavoriteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_CustomerId",
                table: "CreditCards",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Communications_CustomerDetailCustomerId",
                table: "Communications",
                column: "CustomerDetailCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDetails_CommunicationId",
                table: "CustomerDetails",
                column: "CommunicationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDetails_CreditCardId",
                table: "CustomerDetails",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDetails_CustomerId",
                table: "CustomerDetails",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteCustomer_CustomerDetailId",
                table: "FavoriteCustomer",
                column: "CustomerDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekProducts_ProductId",
                table: "WeekProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Communications_Customers_CustomerDetailCustomerId",
                table: "Communications",
                column: "CustomerDetailCustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Customers_CustomerId",
                table: "CreditCards",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
