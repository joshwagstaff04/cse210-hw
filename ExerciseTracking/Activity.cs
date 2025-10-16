using System;

namespace ExerciseTracking
{
    public abstract class Activity
    {
        private DateTime _date;
        private int _minutes;

        protected Activity(DateTime date, int minutes)
        {
            _date = date;
            _minutes = minutes;
        }

        public DateTime Date => _date;
        public int Minutes => _minutes;


        public abstract double GetDistance();
        public abstract double GetSpeed();
        public abstract double GetPace();

        public virtual string GetSummary()
        {
            return $"{Date:dd MMM yyyy} {GetType().Name} ({Minutes} min): " +
                   $"Distance {GetDistance():0.0} miles, " +
                   $"Speed {GetSpeed():0.0} mph, " +
                   $"Pace: {GetPace():0.00} min per mile";
        }
    }
}
