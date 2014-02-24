using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BacklogTracker.Implementation;
using NUnit;
using NUnit.Core;
using NUnit.Framework;

namespace BacklogTracker.Tests
{   
    public class DynamicProgrammingKnapsackSolverTests
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
            new TestCaseData(1, new[] { Stories[4] }), 
            new TestCaseData(2, new[] { Stories[9] }), 
            new TestCaseData(3, new[] { Stories[9], Stories[4] }),
            new TestCaseData(4, new[] { Stories[9], Stories[4], Stories[5] }),
            new TestCaseData(5, new[] { Stories[9], Stories[4], Stories[1] }) 
        };

        private DynamicProgrammingKnapsackSolver _cut = new DynamicProgrammingKnapsackSolver();

        [Test, TestCaseSource("TestCases")]
        public void Test(int max, IStory[] solution)
        {
            var result = _cut.Solve(max, Stories);
            CollectionAssert.AreEqual(solution, result, "Solutions are not equal");
        }
    }
}
