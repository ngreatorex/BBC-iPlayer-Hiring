using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacklogTracker
{
    /// <summary>
    /// This interface represents an agile User Story in a Scrum backlog
    /// </summary>
    public interface IStory
    {
        /// <summary>
        /// The unique ID of this story
        /// </summary>
        String Id { get; }

        /// <summary>
        /// The number of story points assigned to the story. 
        /// This represents an estimate of the development effort required for this story.
        /// </summary>
        int Points { get; set; }

        /// <summary>
        /// The business priority of the story.
        /// A lower number represents a higher priority 
        /// </summary>
        int Priority { get; set; }
    }
}
