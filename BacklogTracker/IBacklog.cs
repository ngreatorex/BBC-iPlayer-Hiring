using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacklogTracker
{
    /// <summary>
    /// Represents a Scrum backlog
    /// </summary>
    /// <remarks>
    /// Implementations of this interface should be thread safe
    /// </remarks>
    public interface IBacklog
    {
        /// <summary>
        /// Add a new story to the backlog
        /// </summary>
        /// <param name="s">The story to add</param>
        /// <exception cref="ArgumentException">This should be thrown if the ID of the given story already exists in the backlog</exception>
        void Add(IStory s);

        /// <summary>
        /// Remove a story from the backlog
        /// </summary>
        /// <param name="id">The unique ID of the story to remove</param>
        /// <returns>The story that was removed</returns>
        /// <exception cref="ArgumentException">This should be thrown if the ID given doesn't exist in the backlog</exception>
        IStory Remove(String id); 

        /// <summary>
        /// Generate a list of stories that represents a sprint
        /// </summary>
        /// <param name="totalPointsAchievable">The maximum number of story points that can be accommodated
        /// in this sprint</param>
        /// <returns>A list of stories ordered by the business priority</returns>
        /// <exception cref="ArgumentException">Thrown if the totalPointsAchievable is less than zero</exception>
        List<IStory> getSprint(int totalPointsAchievable);
    }
}
