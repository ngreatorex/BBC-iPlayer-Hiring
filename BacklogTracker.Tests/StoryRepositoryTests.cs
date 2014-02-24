using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BacklogTracker.Implementation;
using Moq;
using NUnit.Core;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Kernel;

namespace BacklogTracker.Tests
{
    [TestFixture(typeof(MemoryRepository<IStory, string>))]
    public class StoryRepositoryTests<T> 
        where T : IRepository<IStory, string>, new()
    {
        [Test]
        public void TestInsert()
        {
            var sut = new T();
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var story = fixture.Create<IStory>();
            Mock.Get(story).SetupGet(x => x.Id).Returns(fixture.Create<string>());

            Assert.That(sut.GetById(story.Id), Is.Null);

            sut.Insert(story.Id, story);

            Assert.That(sut.GetById(story.Id), Is.EqualTo(story));

            Assert.That(() => sut.Insert(story.Id, story), Throws.ArgumentException);
        }

        [Test]
        public void TestUpdate()
        {
            var sut = new T();
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var story1 = fixture.Create<IStory>();
            Mock.Get(story1).SetupGet(x => x.Id).Returns(fixture.Create<string>());

            var story2 = fixture.Create<IStory>();
            Mock.Get(story2).SetupGet(x => x.Id).Returns(story1.Id);

            // Verify that the mocks are not equal for sanity's sake
            Assert.That(story1, Is.Not.EqualTo(story2));

            Assert.That(() => sut.Update(story1.Id, story1), Throws.ArgumentException);

            sut.Insert(story1.Id, story1);

            Assert.That(sut.GetById(story1.Id), Is.EqualTo(story1));

            sut.Update(story1.Id, story2);

            Assert.That(sut.GetById(story1.Id), Is.EqualTo(story2));

        }

        [Test]
        public void TestDelete()
        {
            var sut = new T();
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var story = fixture.Create<IStory>();
            Mock.Get(story).SetupGet(x => x.Id).Returns(fixture.Create<string>());

            sut.Insert(story.Id, story);

            var result = sut.DeleteById(story.Id);
                
            Assert.That(result, Is.EqualTo(story));
            Assert.That(sut.GetById(story.Id), Is.Null);

            Assert.That(() => sut.DeleteById(story.Id), Throws.ArgumentException);
        }

        [Test]
        public void TestGetAll()
        {
            var sut = new T();
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            var stories = new List<IStory>();

            for (int i = 1; i <= 20; i++)
            {
                var story = fixture.Create<IStory>();
                Mock.Get(story).SetupGet(x => x.Id).Returns(fixture.Create<string>());
                stories.Add(story);

                sut.Insert(story.Id, story);

                var result = sut.GetAll().ToArray();

                Assert.That(result, Has.Length.EqualTo(i).And.EquivalentTo(stories));
            }
        }
    }
}
