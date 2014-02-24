using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacklogTracker
{
    public interface IBacklog
    {
        void Add(IStory s);
        IStory Remove(String id);   // On success, the removed Story is returned.
        List<IStory> getSprint(int totalPointsAchievable);
    }
}
