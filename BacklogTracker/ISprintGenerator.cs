using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacklogTracker
{
    /// <summary>
    /// This interface represents a strategy for deciding which stories to include
    /// in a sprint. By using this interface, it becomes easy to change the algorithm
    /// for choosing stories.
    /// </summary>
    public interface ISprintGenerator
    {
        /// <summary>
        /// Select which stories from the given list to include in this sprint.
        /// </summary>
        /// <param name="capacity">The maximum number of story points to include in the sprint</param>
        /// <param name="candidates">The list of stories that can be included</param>
        /// <returns>An enumeration of the stories to include in the sprint, ordered by the priority</returns>
        IEnumerable<IStory> Solve(int capacity, IEnumerable<IStory> candidates);
    }
}
