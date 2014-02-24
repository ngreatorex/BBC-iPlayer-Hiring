using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacklogTracker.Implementation
{
    /// <summary>
    /// This class will generate sprints based on the following rules:
    ///   1. Higher priority stories should always be included first, even if this means story points go unused during this sprint.
    ///   2. It is better to include one large priority 1 story than to have one small priority 1 story and several lower priority stories
    /// </summary>
    /// <remarks>This class IS NOT thread safe</remarks>
    public class PriorityBasedSprintGenerator : ISprintGenerator
    {
        public IEnumerable<IStory> Solve(int capacity, IEnumerable<IStory> candidates)
        {
            if (capacity < 0)
                throw new ArgumentException("Argument must be greater than zero", "capacity");

            if (candidates == null)
                throw new ArgumentNullException("candidates");

            var candidateList = new LinkedList<IStory>(candidates.OrderBy(x => x.Priority).ThenByDescending(x => x.Points));
           
            int capacityLeft = capacity;
            bool candidateFound = true;
            List<IStory> solution = new List<IStory>();

            while (capacityLeft > 0 && candidateFound)
            {
                candidateFound = false;

                foreach (var candidate in candidateList)
                {
                    if (capacityLeft >= candidate.Points)
                    {
                        solution.Add(candidate);
                        capacityLeft -= candidate.Points;
                        candidateList.Remove(candidate);
                        candidateFound = true;
                        break;
                    }
                }
            }

            return solution;
        }
    }
}
