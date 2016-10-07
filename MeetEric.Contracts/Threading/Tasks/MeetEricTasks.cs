namespace MeetEric.Threading.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MeetEric.Common;

    public static class MeetEricTask
    {
        public static IEnumerable<Task> InParallel(this IEnumerable<Func<Task>> tasks)
        {
            foreach (var task in tasks)
            {
                var starter = new Task(async () => await task());
                starter.Start();
                yield return starter;
            }
        }

        public static IEnumerable<Task<T>> InParallel<T>(this IEnumerable<Func<Task<T>>> tasks)
        {
            foreach (var task in tasks)
            {
                var starter = new Task<T>(() => task().Result);
                starter.Start();
                yield return starter;
            }
        }

        public static void Run(Func<Task> task)
        {
            Wait(Task.Run(task));
        }

        public static T Run<T>(Func<Task<T>> task)
        {
            var runner = Task.Run(task);
            Wait(runner);
            return runner.Result;
        }

        private static void Wait(Task runner)
        {
            try
            {
                runner.ConfigureAwait(false);
                runner.Wait();
            }
            catch (AggregateException e)
            {
                e.FlattenAndThrow();
            }
        }
    }
}
