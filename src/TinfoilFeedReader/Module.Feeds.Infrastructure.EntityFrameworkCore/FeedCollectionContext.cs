using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Module.Feeds.Domain;
using System;
using System.Collections.Generic;

namespace Module.Feeds.Infrastructure.EntityFrameworkCore
{
    public class FeedCollectionContext : DbContext
    {
        public FeedCollectionContext(DbContextOptions options)
            : base(options) { }

        public DbSet<FeedCollection> Collections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeedCollection>()
                .ToTable("FeedCollection")
                .Property(e => e.Id)
                .IsRequired();

            modelBuilder.Entity<FeedCollection>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<FeedCollection>()
                .HasMany(e => e.Feeds)
                .WithOne();

            modelBuilder.Entity<Feed>()
                .ToTable("Feed")
                .Property(e => e.Id)
                .IsRequired();

            modelBuilder.Entity<Feed>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Feed>()
                .HasMany(e => e.Sources);

            modelBuilder.Entity<FeedSource>()
                .ToTable("FeedSource")
                .Property(e => e.Id)
                .IsRequired();

            modelBuilder.Entity<FeedSource>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<FeedCollection>().HasData(new FeedCollection
            {
                Id = Guid.Parse("{69040700-3B62-42CF-803E-0C26C40DB842}")
            });

            modelBuilder.Entity<Feed>().HasData(
                new
                {
                    Id = Guid.Parse("{4CC67215-2615-4A7A-B4A0-67A04626DBB0}"),
                    Name = "Local News",
                    FeedCollectionId = Guid.Parse("{69040700-3B62-42CF-803E-0C26C40DB842}")
                },
                new
                {
                    Id = Guid.Parse("{A83E49B2-13E1-4E5A-A294-B32740409C50}"),
                    Name = "Politics",
                    FeedCollectionId = Guid.Parse("{69040700-3B62-42CF-803E-0C26C40DB842}")
                },
                new
                {
                    Id = Guid.Parse("{BFDFB55D-8887-41A5-B37C-E7FCFCC2A83E}"),
                    Name = "Technology",
                    FeedCollectionId = Guid.Parse("{69040700-3B62-42CF-803E-0C26C40DB842}")
                },
                new
                {
                    Id = Guid.Parse("{D0867B47-0DB5-446E-99E0-5F66674F4AD1}"),
                    Name = "YouTube Subscriptions",
                    FeedCollectionId = Guid.Parse("{69040700-3B62-42CF-803E-0C26C40DB842}")
                });

            modelBuilder.Entity<FeedSource>().HasData(
                new
                {
                    Id = Guid.Parse("{6F6CCD9C-F5B1-4F7F-99CF-601DA1276CAB}"),
                    Name = "Ars Technica",
                    Url = "https://feeds.feedburner.com/arstechnica/index",
                    FeedId = Guid.Parse("{BFDFB55D-8887-41A5-B37C-E7FCFCC2A83E}")
                },
                new
                {
                    Id = Guid.Parse("{7F0E8887-A73A-4A97-B32E-27BF2825D08A}"),
                    Name = "The Verge",
                    Url = "https://www.theverge.com/rss/index.xml",
                    FeedId = Guid.Parse("{BFDFB55D-8887-41A5-B37C-E7FCFCC2A83E}")
                },
                new
                {
                    Id = Guid.Parse("{FF5A433A-2425-4711-85B9-05E4DD86A4DE}"),
                    Name = "CGP Grey",
                    Url = "https://www.youtube.com/feeds/videos.xml?channel_id=UC2C_jShtL725hvbm1arSV9w",
                    FeedId = Guid.Parse("{D0867B47-0DB5-446E-99E0-5F66674F4AD1}")
                });
        }
    }
}
