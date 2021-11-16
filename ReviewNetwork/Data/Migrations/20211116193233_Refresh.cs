using Microsoft.EntityFrameworkCore.Migrations;

namespace ReviewNetwork.Data.Migrations
{
    public partial class Refresh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_CategoryId1",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_AspNetUsers_UserId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_Reviews_ReviewId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Category_CategoryId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Like",
                newName: "Likes");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Like_UserId",
                table: "Likes",
                newName: "IX_Likes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_ReviewId",
                table: "Likes",
                newName: "IX_Likes_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_CategoryId1",
                table: "Categories",
                newName: "IX_Categories_CategoryId1");

            migrationBuilder.AddColumn<int>(
                name: "PageId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                column: "LikeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    PageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.PageId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PageId",
                table: "Reviews",
                column: "PageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CategoryId1",
                table: "Categories",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Reviews_ReviewId",
                table: "Likes",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Categories_CategoryId",
                table: "Reviews",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Pages_PageId",
                table: "Reviews",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "PageId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryId1",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Reviews_ReviewId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Categories_CategoryId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Pages_PageId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_PageId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "Like");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserId",
                table: "Like",
                newName: "IX_Like_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_ReviewId",
                table: "Like",
                newName: "IX_Like_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_CategoryId1",
                table: "Category",
                newName: "IX_Category_CategoryId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                column: "LikeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_CategoryId1",
                table: "Category",
                column: "CategoryId1",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_AspNetUsers_UserId",
                table: "Like",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Reviews_ReviewId",
                table: "Like",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Category_CategoryId",
                table: "Reviews",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
