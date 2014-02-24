using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BacklogTracker.Implementation;
using NUnit;
using NUnit.Core;
using NUnit.Framework;

namespace BacklogTracker.Tests
{   
    [TestFixture(typeof(PriorityBasedSprintGenerator))]
    [TestFixture(typeof(KnapsackProblemSolverSprintGenerator))]
    public class SprintGeneratorTests<T> where T : ISprintGenerator, new()
    {
        private static List<IStory> Stories = new List<IStory>()
        {
            new Story("STORY-0") { Points = 6, Priority = 1 }, 
            new Story("STORY-1") { Points = 2, Priority = 4 },
            new Story("STORY-2") { Points = 3, Priority = 4 },
            new Story("STORY-3") { Points = 5, Priority = 3 },
            new Story("STORY-4") { Points = 1, Priority = 2 },
            new Story("STORY-5") { Points = 1, Priority = 4 },
            new Story("STORY-6") { Points = 7, Priority = 3 },
            new Story("STORY-7") { Points = 10, Priority = 2 },
            new Story("STORY-8") { Points = 4, Priority = 2 },
            new Story("STORY-9") { Points = 2, Priority = 1 },
        };

        private static TestCaseData[] TestCases =
        {
            new TestCaseData(0, new IStory[] {}), 
            new TestCaseData(1, new[] { Stories[4] }), 
            new TestCaseData(2, new[] { Stories[9] }), 
            new TestCaseData(3, new[] { Stories[9], Stories[4] }),
            new TestCaseData(4, new[] { Stories[9], Stories[4], Stories[5] }),
            new TestCaseData(5, new[] { Stories[9], Stories[4], Stories[1] }),
            new TestCaseData(6, new[] { Stories[0] }),
            new TestCaseData(7, new[] { Stories[0], Stories[4] }),
            new TestCaseData(8, new[] { Stories[0], Stories[9] }),
            new TestCaseData(9, new[] { Stories[0], Stories[9], Stories[4] }),
            new TestCaseData(10, new[] { Stories[0], Stories[9], Stories[4], Stories[5] }),
            new TestCaseData(11, new[] { Stories[0], Stories[9], Stories[4], Stories[1] }),
            new TestCaseData(12, new[] { Stories[0], Stories[9], Stories[8] }),
        };

        private ISprintGenerator _cut = new T();

        [Test]
        public void TestSprintSizeBoundaryValues()
        {
            Assert.That(() => _cut.Solve(int.MinValue, Stories), Throws.ArgumentException);
            Assert.That(() => _cut.Solve(-1, Stories), Throws.ArgumentException);
            Assert.That(() => _cut.Solve(0, Stories), Throws.Nothing);
            Assert.That(() => _cut.Solve(int.MaxValue, Stories), Throws.Nothing);
        }

        [Test]
        public void TestThatArgumentNullExceptionIsThrown()
        {
            Assert.That(() => _cut.Solve(0, null), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test, TestCaseSource("TestCases")]
        public void TestSolutionAccuracy(int max, IStory[] expectedSolution)
        {
            var result = _cut.Solve(max, Stories);

            Console.WriteLine("Solution has {0} stories for a total of {1} points:", result.Count(), result.Sum(x => x.Points));
            Console.WriteLine();
            foreach (var story in result)
                Console.WriteLine(story.ToString());

            Assert.That(result, Is.EquivalentTo(expectedSolution), "Solutions are not equivalent");
            Assert.That(result, Is.Ordered.By("Priority"), "Solution is not ordered correctly");
        }

        [TestCase(100000, 10, 10, 10)]
        [TestCase(100000, 100, 10, 10)]
        [TestCase(100000, 1000, 10, 10)]
        [TestCase(100000, 10000, 10, 10)]
        [TestCase(100000, 100000, 10, 10)]
        [TestCase(1000000, 10000, 1, 10)]
        [TestCase(1000000, 10000, 10, 10)]
        [TestCase(1000000, 10000, 10000, 10000)]
        public void TestSolutionPerformance(int storyCount, int sprintSize, int maxPoints, int maxPriority)
        {
            List<IStory> stories = new List<IStory>();
            Random r = new Random();

            for (int i = 0; i < storyCount; i++)
            {
                stories.Add(new Story(string.Format("STORY-{0}", i)) { Points = r.Next(1, maxPoints), Priority = r.Next(1, maxPriority)});
            }

            var stopwatch = Stopwatch.StartNew();
            var solution = _cut.Solve(sprintSize, stories);
            stopwatch.Stop();

            var solutionSize = solution.Sum(x => x.Points);

            Console.WriteLine("Solution took {0} ms and contained {1} stories totalling {2} points", stopwatch.ElapsedMilliseconds, solution.Count(), solutionSize);
            
            Assert.That(solutionSize, Is.LessThanOrEqualTo(sprintSize), "Solution was too large");
            Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(5000), "Solution took longer than 5 seconds!");
        }
    }
}
