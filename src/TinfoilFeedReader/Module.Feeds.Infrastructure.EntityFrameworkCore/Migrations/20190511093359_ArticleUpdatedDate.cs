using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Module.Feeds.Infrastructure.EntityFrameworkCore.Migrations
{
    public partial class ArticleUpdatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Source_SourceId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedSource_Feed_FeedId",
                table: "FeedSource");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastUpdatedDate",
                table: "Article",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

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

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Article");

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
