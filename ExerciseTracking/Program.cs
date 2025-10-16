using System;
using System.Collections.Generic;

namespace ExerciseTracking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var activities = new List<Activity>
            {
                new Running(new DateTime(2022, 11, 3), 30, 3.0),
                new Cycling(new DateTime(2022, 11, 3), 45, 16.0),
                new Swimming(new DateTime(2022, 11, 3), 40, 30)
            };

            foreach (var activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }
        }
    }
}
