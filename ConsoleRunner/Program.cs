using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BacklogTracker;
using BacklogTracker.Implementation;

namespace ConsoleRunner
{
    class Program
    {
        private static IBacklog _backlog = Bootstrap.GetInstance<IBacklog>();

        static void Main(string[] args)
        {
            bool quit = false;

            while (!quit)
            {
                Console.Clear();
                Console.WriteLine("BBC Backlog Tracking Tool - Console Wrapper");
                Console.WriteLine();
                Console.WriteLine("  1.  Add a new story");
                Console.WriteLine("  2.  Remove a story");
                Console.WriteLine("  3.  Generate a sprint");
                Console.WriteLine("  4.  Quit");
                Console.WriteLine();
                Console.Write("Please choose an option: ");

                var key = Console.ReadKey();
                
                switch (key.KeyChar)
                {
                    case '1':
                        AddStory();
                        break;
                    case '2':
                        RemoveStory();
                        break;
                    case '3':
                        GenerateSprint();
                        break;
                    case '4':
                        quit = true;
                        break;
                }
            }
        }

        static void AddStory()
        {
            Console.Clear();
            Console.WriteLine("BBC Backlog Tracking Tool - Console Wrapper");
            Console.WriteLine();
            string id = "";

            while (string.IsNullOrEmpty(id))
            {
                Console.Write("Please enter story ID: ");
            
                id = Console.ReadLine();
            }
            int points = -1;

            while (points < 0)
            {
                Console.Write("Please enter story points: ");
                try
                {
                    points = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a positive integer");
                }
            }

            int priority = -1;

            while (priority < 0)
            {
                Console.Write("Please enter story priority: ");
                try
                {
                    priority = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a positive integer");
                }
            }

            try
            {
                var story = new Story(id) { Points = points, Priority = priority };
                _backlog.Add(story);
                Console.WriteLine("Successfully added story");
            }
            catch (Exception e)
            {
                Console.WriteLine("Sorry, an error occurred:");
                Console.WriteLine(e.ToString());
            }
            
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        static void RemoveStory()
        {
            Console.Clear();
            Console.WriteLine("BBC Backlog Tracking Tool - Console Wrapper");
            Console.WriteLine();
            Console.Write("Please enter story ID to remove: ");
            var id = Console.ReadLine();
            try
            {
                var story = _backlog.Remove(id);
                Console.WriteLine("Story {0} removed from backlog", story.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Sorry, an error occurred:");
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        static void GenerateSprint()
        {
            Console.Clear();
            Console.WriteLine("BBC Backlog Tracking Tool - Console Wrapper");
            Console.WriteLine();
            int points = -1;

            while (points < 0)
            {
                Console.Write("Please enter story points: ");
                try
                {
                    points = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a positive integer");
                }
            }

            try
            {
                var result = _backlog.getSprint(points);
                Console.WriteLine("Sprint has {0} stories for a total of {1} points:", result.Count(), result.Sum(x => x.Points));
                Console.WriteLine();
                foreach (var story in result)
                    Console.WriteLine(story.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Sorry, an error occurred:");
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
