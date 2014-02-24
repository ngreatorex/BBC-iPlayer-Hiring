using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BacklogTracker.Implementation;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace BacklogTracker
{
    public class Bootstrap
    {
        private static Container _container;

        static Bootstrap()
        {
            _container = new Container();
            _container.RegisterSingleOpenGeneric(typeof (IRepository<,>), typeof (MemoryRepository<,>));
            _container.RegisterSingle<IBacklog, Backlog>();
            _container.Register<ISprintGenerator, PriorityBasedSprintGenerator>();

            _container.Verify();
        }

        public static T GetInstance<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}
