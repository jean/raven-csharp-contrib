using System;
using System.Threading;

using SharpRaven;

// this attribute will log any exception in methods that have been enhanced
[assembly: RavenLogException]

// this attribute will log warning message for methods that takes longer than 1ms to execute
[assembly: RavenLogExecutionTimeAttribute(1)]

namespace SharpRavenContribExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // wrapper delegate for executing the test methods
            Action<Action> wrapper = f =>
                {
                    try
                    {
                        f();
                    }
                    catch (Exception)
                    {
                    }
                };

            wrapper(LoggedToSentry);
            wrapper(NotLoggedToSentry);

            Console.WriteLine("All done, check your Sentry account console. Press any key to exit...");
            Console.ReadKey();
        }

        private static void LoggedToSentry()
        {
            Thread.Sleep(2); // sleep for 2 ms so it's enough to trigger a message to be logged
            throw new Exception("Test");
        }

        /// <summary>
        /// You can exclude methods from the logging too
        /// </summary>
        [RavenLogException(AttributeExclude = true)]
        [RavenLogExecutionTimeAttribute(1, AttributeExclude = true)]
        private static void NotLoggedToSentry()
        {
            throw new ArgumentException();
        }
    }
}