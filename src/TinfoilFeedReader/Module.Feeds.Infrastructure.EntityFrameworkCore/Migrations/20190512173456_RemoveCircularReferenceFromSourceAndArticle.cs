using Microsoft.EntityFrameworkCore.Migrations;

namespace Module.Feeds.Infrastructure.EntityFrameworkCore.Migrations
{
    public partial class RemoveCircularReferenceFromSourceAndArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Source_SourceId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedSource_Feed_FeedId",
                table: "FeedSource");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Source_SourceId",
                table: "Article",
                column: "SourceId",
                principalTable: "Source",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedSource_Feed_FeedId",
                table: "FeedSource",
                column: "FeedId",
                principalTable: "Feed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Source_SourceId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedSource_Feed_FeedId",
                table: "FeedSource");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Source_SourceId",
                table: "Article",
                column: "SourceId",
                principalTable: "Source",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedSource_Feed_FeedId",
                table: "FeedSource",
                column: "FeedId",
                principalTable: "Feed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
