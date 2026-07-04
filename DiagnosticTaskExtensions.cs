using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Pat.Extensions
{
    public static class DiagnosticTaskExtensions
    {
        /// <summary>
        /// Associates a task with a user-specified name before GetAwaiter is called
        /// </summary>
        public static NamedTask<T> WithName<T>(this Task<T> task, string name)
        {
            return new NamedTask<T>(task, name);
        }

        /// <summary>
        /// Gets a diagnostic awaiter for a task, based only on its ID.
        /// </summary>
        public static NamedAwaiter<T> GetAwaiter<T>(this Task<T> task)
        {
            return new NamedTask<T>(task, "[" + task.Id + "]").GetAwaiter();
        }

        public struct NamedTask<T>
        {
            private readonly Task<T> task;
            private readonly string name;

            public NamedTask(Task<T> task, string name)
            {
                this.task = task;
                this.name = name;
            }

            public NamedAwaiter<T> GetAwaiter()
            {
                Console.WriteLine("GetAwaiter called for task \"{0}\"", name);
                return new NamedAwaiter<T>(task.GetAwaiter(), name);
            }
        }

        public struct NamedAwaiter<T> : INotifyCompletion
        {
            private readonly TaskAwaiter<T> awaiter;
            private readonly string name;

            public NamedAwaiter(TaskAwaiter<T> awaiter, string name)
            {
                this.awaiter = awaiter;
                this.name = name;
            }

            public void OnCompleted(Action continuation)
            {
                Console.WriteLine("OnCompleted called for task \"{0}\"", name);
                // We could potentially report the result here
                awaiter.OnCompleted(continuation);
            }

            public bool IsCompleted
            {
                get => awaiter.IsCompleted;
            }

            public T GetResult()
            {
                return awaiter.GetResult();
            }
        }

        /// <summary>
        /// Associates a task with a user-specified name before GetAwaiter is called
        /// </summary>
        public static NamedTask WithName(this Task task, string name)
        {
            return new NamedTask(task, name);
        }

        /// <summary>
        /// Gets a diagnostic awaiter for a task, based only on its ID.
        /// </summary>
        public static NamedAwaiter GetAwaiter(this Task task)
        {
            return new NamedTask(task, "[" + task.Id + "]").GetAwaiter();
        }

        public struct NamedTask
        {
            private readonly Task task;
            private readonly string name;

            public NamedTask(Task task, string name)
            {
                this.task = task;
                this.name = name;
            }

            public NamedAwaiter GetAwaiter()
            {
                Console.WriteLine("++++++++++++++++++++ GetAwaiter called for task \"{0}\"", name);
                return new NamedAwaiter(task.GetAwaiter(), name);
            }

            public object ContinueWith(Action<Task> continuationAction, CancellationToken token, TaskContinuationOptions continuationOptions, TaskScheduler ts)
            {
                return task.ContinueWith(continuationAction, token, continuationOptions, ts);
            }

            public object ContinueWith(Func<Task, Task> continuationTask, CancellationToken token, TaskContinuationOptions continuationOptions, TaskScheduler ts)
            {
                return task.ContinueWith(continuationTask, token, continuationOptions, ts);
            }
        }

        public struct NamedAwaiter : INotifyCompletion
        {
            private readonly TaskAwaiter awaiter;
            private readonly string name;

            public NamedAwaiter(TaskAwaiter awaiter, string name)
            {
                this.awaiter = awaiter;
                this.name = name;
            }

            public void OnCompleted(Action continuation)
            {
                Console.WriteLine("++++++++++++++++++++ OnCompleted called for task \"{0}\"", name);
                // We could potentially report the result here
                awaiter.OnCompleted(continuation);
            }

            public bool IsCompleted
            {
                get => awaiter.IsCompleted;
            }

            public void GetResult()
            {
                Console.WriteLine("++++++++++++++++++++ GetResult called for task \"{0}\"", name);
                awaiter.GetResult();
            }
        }
    }
}
