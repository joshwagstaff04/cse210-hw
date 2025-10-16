using System;

namespace ExerciseTracking
{
    public class Cycling : Activity
    {
        private double _speedMph;

        public Cycling(DateTime date, int minutes, double speedMph)
            : base(date, minutes)
        {
            _speedMph = speedMph;
        }

        public override double GetDistance()
        {

            return _speedMph * (Minutes / 60.0);
        }

        public override double GetSpeed() => _speedMph;

        public override double GetPace()
        {

            return _speedMph == 0 ? 0 : 60.0 / _speedMph;
        }
    }
}
