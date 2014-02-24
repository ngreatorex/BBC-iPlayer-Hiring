using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacklogTracker.Implementation
{
    /// <summary>
    /// Represents a Scrum backlog with the data stored in the injected repository
    /// </summary>
    /// <remarks>
    /// This class IS thread safe
    /// This class is designed for constructor dependency injection
    /// </remarks>
    public class Backlog : IBacklog
    {
        private readonly IRepository<IStory, string> _repository;
        private readonly ISprintGenerator _solver;
        private readonly object _lock = new object();

        /// <summary>
        /// Construct a new backlog instance for the given repository
        /// </summary>
        /// <remarks>This is designed for constructor dependency injection</remarks>
        /// <param name="repository">The repository to use for storing the stories</param>
        /// <param name="solver">The implementation to use to calculate sprints</param>
        public Backlog(IRepository<IStory, string> repository, ISprintGenerator solver)
        {
            this._repository = repository;
            this._solver = solver;
        }

        /// <inheritDoc/>
        public void Add(IStory s)
        {
            lock (_lock)
            {
                _repository.Insert(s.Id, s);
            }
        }

        /// <inheritDoc/>
        public IStory Remove(string id)
        {
            lock (_lock)
            {
                return _repository.DeleteById(id);
            }
        }

        /// <inheritDoc/>
        public List<IStory> getSprint(int totalPointsAchievable)
        {
            lock (_lock)
            {
                return _solver.Solve(totalPointsAchievable, _repository.GetAll()).ToList();
            }
        }
    }
}
