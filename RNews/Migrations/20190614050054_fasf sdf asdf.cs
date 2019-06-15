using Microsoft.EntityFrameworkCore.Migrations;

namespace RNews.Migrations
{
    public partial class fasfsdfasdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsLike",
                table: "CommentLikes",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsLike",
                table: "CommentLikes",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
