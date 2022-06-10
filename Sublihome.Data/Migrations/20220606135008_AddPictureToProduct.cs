using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sublihome.Data.Migrations
{
    public partial class AddPictureToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Product");
        }
    }
}
