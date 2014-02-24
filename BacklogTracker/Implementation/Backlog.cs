using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacklogTracker.Implementation
{
    public class Backlog : IBacklog
    {
        private readonly IRepository<IStory, string> _repository;
        private readonly ISprintGenerator _solver;
        private readonly object _lock = new object();

        public Backlog(IRepository<IStory, string> repository, ISprintGenerator solver)
        {
            this._repository = repository;
            this._solver = solver;
        }

        public void Add(IStory s)
        {
            lock (_lock)
            {
                _repository.Insert(s.Id, s);
            }
        }

        public IStory Remove(string id)
        {
            lock (_lock)
            {
                return _repository.DeleteById(id);
            }
        }

        public List<IStory> getSprint(int totalPointsAchievable)
        {
            lock (_lock)
            {
                return _solver.Solve(totalPointsAchievable, _repository.GetAll()).ToList();
            }
        }
    }
}
