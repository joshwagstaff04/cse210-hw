using System;

namespace ExerciseTracking
{
    public class Swimming : Activity
    {
        private int _laps;
        private const double MetersPerLap = 50.0;
        private const double MetersPerMile = 1609.34;

        public Swimming(DateTime date, int minutes, int laps)
            : base(date, minutes)
        {
            _laps = laps;
        }

        public override double GetDistance()
        {

            return (_laps * MetersPerLap) / MetersPerMile;
        }

        public override double GetSpeed()
        {

            double miles = GetDistance();
            return (miles / Minutes) * 60.0;
        }

        public override double GetPace()
        {

            double miles = GetDistance();
            return miles == 0 ? 0 : Minutes / miles;
        }
    }
}
