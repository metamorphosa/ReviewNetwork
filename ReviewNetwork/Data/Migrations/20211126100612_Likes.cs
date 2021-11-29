using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewNetwork.Data.Migrations
{
    public partial class Likes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Reviews",
                newName: "LikeCount");

            migrationBuilder.AddColumn<bool>(
                name: "IsLiked",
                table: "Likes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLiked",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "LikeCount",
                table: "Reviews",
                newName: "Rating");

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
