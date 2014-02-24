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
    /// <summary>
    /// This class is used to bootstrap the IoC / DI container and to return
    /// instances of constructed classes
    /// </summary>
    public class Bootstrap
    {
        private static readonly Container _container;

        static Bootstrap()
        {
            _container = new Container();
            _container.RegisterSingleOpenGeneric(typeof (IRepository<,>), typeof (MemoryRepository<,>));
            _container.RegisterSingle<IBacklog, Backlog>();
            _container.Register<ISprintGenerator, PriorityBasedSprintGenerator>();

            _container.Verify();
        }

        /// <summary>
        /// Return an instance of type T
        /// </summary>
        /// <typeparam name="T">The type you want</typeparam>
        /// <returns>An instance of that type</returns>
        public static T GetInstance<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}
