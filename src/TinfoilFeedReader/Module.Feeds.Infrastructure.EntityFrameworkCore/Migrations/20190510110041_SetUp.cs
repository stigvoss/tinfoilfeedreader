using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Module.Feeds.Infrastructure.EntityFrameworkCore.Migrations
{
    public partial class SetUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedCollection",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Source",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Source", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feed",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SequenceIndex = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FeedCollectionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feed_FeedCollection_FeedCollectionId",
                        column: x => x.FeedCollectionId,
                        principalTable: "FeedCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    SourceId = table.Column<Guid>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    PublishDate = table.Column<DateTimeOffset>(nullable: false),
                    Summary = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    SourceName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => new { x.SourceId, x.Id });
                    table.ForeignKey(
                        name: "FK_Article_Source_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedSource",
                columns: table => new
                {
                    FeedId = table.Column<Guid>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SequenceIndex = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SourceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedSource", x => new { x.FeedId, x.Id });
                    table.ForeignKey(
                        name: "FK_FeedSource_Feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedSource_Source_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FeedCollection",
                column: "Id",
                value: new Guid("69040700-3b62-42cf-803e-0c26c40db842"));

            migrationBuilder.InsertData(
                table: "Source",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[,]
                {
                    { new Guid("6f6ccd9c-f5b1-4f7f-99cf-601da1276cab"), "Ars Technica", "https://feeds.feedburner.com/arstechnica/index" },
                    { new Guid("7f0e8887-a73a-4a97-b32e-27bf2825d08a"), "The Verge", "https://www.theverge.com/rss/index.xml" },
                    { new Guid("ff5a433a-2425-4711-85b9-05e4dd86a4de"), "CGP Grey", "https://www.youtube.com/feeds/videos.xml?channel_id=UC2C_jShtL725hvbm1arSV9w" }
                });

            migrationBuilder.InsertData(
                table: "Feed",
                columns: new[] { "Id", "FeedCollectionId", "Name", "SequenceIndex" },
                values: new object[,]
                {
                    { new Guid("4cc67215-2615-4a7a-b4a0-67a04626dbb0"), new Guid("69040700-3b62-42cf-803e-0c26c40db842"), "Local News", 1 },
                    { new Guid("a83e49b2-13e1-4e5a-a294-b32740409c50"), new Guid("69040700-3b62-42cf-803e-0c26c40db842"), "Politics", 2 },
                    { new Guid("bfdfb55d-8887-41a5-b37c-e7fcfcc2a83e"), new Guid("69040700-3b62-42cf-803e-0c26c40db842"), "Technology", 3 },
                    { new Guid("d0867b47-0db5-446e-99e0-5f66674f4ad1"), new Guid("69040700-3b62-42cf-803e-0c26c40db842"), "YouTube Subscriptions", 4 }
                });

            migrationBuilder.InsertData(
                table: "FeedSource",
                columns: new[] { "FeedId", "Id", "Name", "SequenceIndex", "SourceId" },
                values: new object[] { new Guid("bfdfb55d-8887-41a5-b37c-e7fcfcc2a83e"), 1, null, 1, new Guid("6f6ccd9c-f5b1-4f7f-99cf-601da1276cab") });

            migrationBuilder.InsertData(
                table: "FeedSource",
                columns: new[] { "FeedId", "Id", "Name", "SequenceIndex", "SourceId" },
                values: new object[] { new Guid("bfdfb55d-8887-41a5-b37c-e7fcfcc2a83e"), 2, null, 2, new Guid("7f0e8887-a73a-4a97-b32e-27bf2825d08a") });

            migrationBuilder.InsertData(
                table: "FeedSource",
                columns: new[] { "FeedId", "Id", "Name", "SequenceIndex", "SourceId" },
                values: new object[] { new Guid("d0867b47-0db5-446e-99e0-5f66674f4ad1"), 3, null, 1, new Guid("ff5a433a-2425-4711-85b9-05e4dd86a4de") });

            migrationBuilder.CreateIndex(
                name: "IX_Feed_FeedCollectionId",
                table: "Feed",
                column: "FeedCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedSource_SourceId",
                table: "FeedSource",
                column: "SourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "FeedSource");

            migrationBuilder.DropTable(
                name: "Feed");

            migrationBuilder.DropTable(
                name: "Source");

            migrationBuilder.DropTable(
                name: "FeedCollection");
        }
    }
}
