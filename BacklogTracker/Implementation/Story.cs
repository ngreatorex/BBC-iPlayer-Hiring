using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacklogTracker.Implementation
{
    public class Story : IStory
    {
        /// <summary>
        /// Create a new story with the given unique ID
        /// </summary>
        /// <param name="id"></param>
        public Story(string id)
        {
            this.Id = id;
        }

        /// <inheritDoc/>
        public string Id
        {
            get;
            private set;
        }

        /// <inheritDoc/>
        public int Points
        {
            get;
            set;
        }

        /// <inheritDoc/>
        public int Priority
        {
            get;
            set;
        }

        /// <summary>
        /// Generate a human readable version of this story
        /// </summary>
        /// <returns>A string containing the ID, Points and Priority</returns>
        public override string ToString()
        {
            return string.Format("{0}: Points = {1}, Priority = {2}", Id, Points, Priority);
        }
    }
}
