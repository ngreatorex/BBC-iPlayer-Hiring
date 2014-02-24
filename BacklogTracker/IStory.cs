using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacklogTracker
{
    public interface IStory
    {
        String Id { get; }
        int Points { get; set; }
        int Priority { get; set; }
    }
}
