using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BacklogTracker.Implementation;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace BacklogTracker.Tests
{
    public class BacklogTests
    {
        [Test]
        public void TestThatAddedStoryIsReturnedInSprint()
        {
            var sut = Bootstrap.GetInstance<IBacklog>();
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var story = fixture.Create<IStory>();
            Mock.Get(story).SetupGet(x => x.Id).Returns(fixture.Create<string>());
            Mock.Get(story).SetupGet(x => x.Points).Returns(1);
            Mock.Get(story).SetupGet(x => x.Priority).Returns(1);

            Assert.That(sut.getSprint(10), Has.No.Member(story));
            sut.Add(story);
            Assert.That(sut.getSprint(10), Has.Member(story));
        }

        [Test]
        public void TestThatRemovedStoryIsCorrectAndNotReturnedInSprint()
        {
            var sut = Bootstrap.GetInstance<IBacklog>();
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var story = fixture.Create<IStory>();
            Mock.Get(story).SetupGet(x => x.Id).Returns(fixture.Create<string>());
            Mock.Get(story).SetupGet(x => x.Points).Returns(1);
            Mock.Get(story).SetupGet(x => x.Priority).Returns(1);

            sut.Add(story);
            Assert.That(sut.getSprint(10), Has.Member(story));

            var result = sut.Remove(story.Id);
            Assert.That(result, Is.EqualTo(story));

            Assert.That(sut.getSprint(10), Has.No.Member(story));
        }

        [Test]
        public void TestThatReturnedSprintIsOrderedByPriority()
        {
            var sut = Bootstrap.GetInstance<IBacklog>();
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            var stories = new List<IStory>();
            
            for (int i = 1; i <= 20; i++)
            {
                var story = fixture.Create<IStory>();
                Mock.Get(story).SetupGet(x => x.Id).Returns(fixture.Create<string>());
                Mock.Get(story).SetupGet(x => x.Points).Returns(1);
                Mock.Get(story).SetupGet(x => x.Priority).Returns(1);
                stories.Add(story);

                sut.Add(story);

                var result = sut.getSprint(1000);

                Assert.That(result, Is.Ordered.By("Priority"));
            }
        }
    }
}
