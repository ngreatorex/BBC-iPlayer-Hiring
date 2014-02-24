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
        private readonly IKnapsackProblemSolver _solver;

        public Backlog(IRepository<IStory, string> repository, IKnapsackProblemSolver solver)
        {
            this._repository = repository;
            this._solver = solver;
        }

        public void Add(IStory s)
        {
            _repository.Insert(s.Id, s);
        }

        public IStory Remove(string id)
        {
            return _repository.DeleteById(id);
        }

        public List<IStory> getSprint(int totalPointsAchievable)
        {
            return _solver.Solve(totalPointsAchievable, _repository.GetAll()).ToList();
        }
    }
}
