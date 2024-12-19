using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class New2 : Migration
    {
        
    protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "Name_Surname",
            table: "Customers",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "varchar(30)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
              name: "Name_Surname",
              table: "Customers",
              maxLength: 30,
              nullable: true,
              oldClrType: typeof(string),
              oldMaxLength: 100);
        }
    }
}
