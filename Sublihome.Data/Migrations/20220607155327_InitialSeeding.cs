using Microsoft.EntityFrameworkCore.Migrations;
using Sublihome.Data.Entities.Orders;
using Sublihome.Data.Entities.Products;

namespace Sublihome.Data.Migrations
{
    public partial class InitialSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Add admin
            migrationBuilder.Sql(
                $"INSERT INTO Users (Id,FirstName,LastName,Email,IsAdmin,Password) " +
                $"VALUES ('1', 'admin', 'admin', 'admin@gmail.com', '1', '{BCrypt.Net.BCrypt.HashPassword("admin")}')");

            //Add cart for Admin
            migrationBuilder.Sql($"INSERT INTO Cart (Id,UserId) VALUES ('1', '1')");

            //Add Product Types
            migrationBuilder.Sql($"INSERT INTO ProductType (Id,Name) VALUES ('1', '{ProductTypeEnum.Shirt}')");
            migrationBuilder.Sql($"INSERT INTO ProductType (Id,Name) VALUES ('2', '{ProductTypeEnum.Cup}')");
            migrationBuilder.Sql($"INSERT INTO ProductType (Id,Name) VALUES ('3', '{ProductTypeEnum.Hoodie}')");

            //Add Order Statuses
            migrationBuilder.Sql($"INSERT INTO OrderStatuses (Id,Status) VALUES ('1', '{OrderStatusEnum.Pending}')");
            migrationBuilder.Sql($"INSERT INTO OrderStatuses (Id,Status) VALUES ('2', '{OrderStatusEnum.Approved}')");
            migrationBuilder.Sql($"INSERT INTO OrderStatuses (Id,Status) VALUES ('3', '{OrderStatusEnum.Rejected}')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM Users;");

            migrationBuilder.Sql($"DELETE FROM Cart;");

            migrationBuilder.Sql($"DELETE FROM ProductType;");

            migrationBuilder.Sql("DELETE FROM OrderStatuses;");
        }
    }
}
