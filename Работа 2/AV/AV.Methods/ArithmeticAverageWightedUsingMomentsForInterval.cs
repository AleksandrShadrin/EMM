using AV.Methods.ValueObjects;

namespace AV.Methods
{
    public class ArithmeticAverageWightedUsingMomentsForInterval : IAverageIntervalEvaluator
    {
        public double A { get; }
        public double K { get; }
        public double k { get; }

        private readonly ArithmeticAverageWeightedInterval averageWeightedInterval;

        public ArithmeticAverageWightedUsingMomentsForInterval(IEnumerable<double> weights, double a = 0, double K = 1, double k = 1)
        {
            A = a;
            this.K = K;
            this.k = k;

            averageWeightedInterval = new(weights.Select(w => w / k).ToList());
        }

        public double Calculate(IEnumerable<BaseInterval> values)
        {
            var newValues = values.Select(v =>
            {
                if(v is ClosedInterval)
                {
                    (v as ClosedInterval).LeftBoundary.Value = ((v as ClosedInterval).LeftBoundary.Value - A) / K;
                    (v as ClosedInterval).RightBoundary.Value = ((v as ClosedInterval).RightBoundary.Value - A) / K;
                } 
                else if(v is OpenedInterval)
                {
                    (v as OpenedInterval).Boundary.Value = ((v as OpenedInterval).Boundary.Value - A) / K;
                }

                return v;
            }).ToList();

            return averageWeightedInterval.Calculate(newValues);
        }
    }
}
