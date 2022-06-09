using Microsoft.EntityFrameworkCore.Migrations;

namespace KitapSatis.Migrations
{
    public partial class customerdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Communications_CommunicationId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CreditCards_CreditCardId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteCustomer_Customers_CustomerId",
                table: "FavoriteCustomer");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CommunicationId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CreditCardId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CommunicationId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreditCardId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeliveryCity",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeliveryCountry",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Favorites",
                newName: "CustomerDetailId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "FavoriteCustomer",
                newName: "CustomerDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteCustomer_CustomerId",
                table: "FavoriteCustomer",
                newName: "IX_FavoriteCustomer_CustomerDetailId");

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
                name: "CustomerDetails",
                columns: table => new
                {
                    CustomerDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CommunicationId = table.Column<int>(type: "int", nullable: false),
                    CreditCardId = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteCustomer_CustomerDetails_CustomerDetailId",
                table: "FavoriteCustomer",
                column: "CustomerDetailId",
                principalTable: "CustomerDetails",
                principalColumn: "CustomerDetailId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communications_Customers_CustomerDetailCustomerId",
                table: "Communications");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Customers_CustomerId",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteCustomer_CustomerDetails_CustomerDetailId",
                table: "FavoriteCustomer");

            migrationBuilder.DropTable(
                name: "CustomerDetails");

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

            migrationBuilder.RenameColumn(
                name: "CustomerDetailId",
                table: "Favorites",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "CustomerDetailId",
                table: "FavoriteCustomer",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteCustomer_CustomerDetailId",
                table: "FavoriteCustomer",
                newName: "IX_FavoriteCustomer_CustomerId");

            migrationBuilder.AddColumn<int>(
                name: "CommunicationId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreditCardId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryCity",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryCountry",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CommunicationId",
                table: "Customers",
                column: "CommunicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreditCardId",
                table: "Customers",
                column: "CreditCardId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Communications_CommunicationId",
                table: "Customers",
                column: "CommunicationId",
                principalTable: "Communications",
                principalColumn: "CommunicationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CreditCards_CreditCardId",
                table: "Customers",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "CreditCardId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteCustomer_Customers_CustomerId",
                table: "FavoriteCustomer",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
