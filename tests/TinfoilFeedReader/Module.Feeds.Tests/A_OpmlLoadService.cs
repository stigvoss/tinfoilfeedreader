using Module.Feeds.Domain;
using Module.Feeds.Infrastructure.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.Feeds.Tests
{
    public class A_OpmlLoadService
    {
        private const string TestOpml = "<opml version=\"1.1\"><body><outline text=\"YouTube-abonnementer\" title=\"YouTube-abonnementer\"><outline text=\"SmarterEveryDay\" title=\"SmarterEveryDay\" type=\"rss\" xmlUrl=\"https://www.youtube.com/feeds/videos.xml?channel_id=UC6107grRI4m0o2-emgoDnAA\" /><outline text=\"Veritasium\" title=\"Veritasium\" type=\"rss\" xmlUrl=\"https://www.youtube.com/feeds/videos.xml?channel_id=UCHnyfMqiRRG1u-2MsSQLbXA\" /><outline text=\"Vsauce\" title=\"Vsauce\" type=\"rss\" xmlUrl=\"https://www.youtube.com/feeds/videos.xml?channel_id=UC6nSFpj9HTCZ5t-N3Rm3-HA\" /><outline text=\"Tom Scott\" title=\"Tom Scott\" type=\"rss\" xmlUrl=\"https://www.youtube.com/feeds/videos.xml?channel_id=UCBa659QWEk1AI4Tg--mrJ2A\" /><outline text=\"SciShow\" title=\"SciShow\" type=\"rss\" xmlUrl=\"https://www.youtube.com/feeds/videos.xml?channel_id=UCZYTClx2T1of7BRZ86-8fow\" /><outline text=\"minutephysics\" title=\"minutephysics\" type=\"rss\" xmlUrl=\"https://www.youtube.com/feeds/videos.xml?channel_id=UCUHW94eEFW7hkUMVaZz4eDg\" /><outline text=\"CGP Grey\" title=\"CGP Grey\" type=\"rss\" xmlUrl=\"https://www.youtube.com/feeds/videos.xml?channel_id=UC2C_jShtL725hvbm1arSV9w\" /></outline></body></opml>";

        private OpmlLoadService _opml;

        [SetUp]
        public void Setup()
        {
            _opml = new OpmlLoadService();
        }

        [Test]
        public void Can_Read_Outline_Feed_Names()
        {
            string expectedFeedName = "YouTube-abonnementer";

            Feed feed = _opml.LoadFeedFrom(TestOpml);

            Assert.That(feed.Name, Is.EqualTo(expectedFeedName));
        }

        [Test]
        public void Can_Read_Outlines_With_Child_Outlines()
        {
            int expectedFeedCount = 7;

            Feed feed = _opml.LoadFeedFrom(TestOpml);

            Assert.That(feed.FeedSources.Count, Is.EqualTo(expectedFeedCount));
        }

        [Test]
        public void Can_Read_Outline_Names()
        {
            string expectedTitle1 = "SmarterEveryDay";
            string expectedTitle2 = "CGP Grey";

            Feed feed = _opml.LoadFeedFrom(TestOpml);

            foreach (var feedSource in feed.FeedSources)
            {
                Assert.That(feedSource.Name, Is.Not.Null);
                Assert.That(feedSource.Name, Is.Not.Empty);
            }

            Assert.That(feed.FeedSources.Select(fs => fs.Name), Contains.Item(expectedTitle1));
            Assert.That(feed.FeedSources.Select(fs => fs.Name), Contains.Item(expectedTitle2));
        }

        [Test]
        public void Can_Read_Outline_Urls()
        {
            string expectedHost = "www.youtube.com";

            Feed feed = _opml.LoadFeedFrom(TestOpml);

            foreach (var feedSource in feed.FeedSources)
            {
                Assert.That(new Uri(feedSource.Source.Url).Host, Is.EqualTo(expectedHost));
            }
        }
    }
}