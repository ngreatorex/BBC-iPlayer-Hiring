using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacklogTracker.Implementation
{
    public class Story : IStory
    {
        public Story(string id)
        {
            this.Id = id;
        }

        public string Id
        {
            get;
            private set;
        }

        public int Points
        {
            get;
            set;
        }

        public int Priority
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("{0}: Points = {1}, Priority = {2}", Id, Points, Priority);
        }
    }
}
