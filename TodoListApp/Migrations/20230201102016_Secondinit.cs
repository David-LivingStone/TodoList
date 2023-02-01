using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoListApp.Migrations
{
    public partial class Secondinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TodoListModel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TodoListModel");
        }
    }
}
