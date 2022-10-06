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
                if (v is ClosedInterval)
                {
                    var closedInterval = (v as ClosedInterval);
                    return closedInterval.CloneWithNewBoundariesValues((closedInterval.LeftBoundary.Value - A) / K, (closedInterval.RightBoundary.Value - A) / K);
                }
                else if (v is OpenedInterval)
                {
                    var openedInterval = (v as OpenedInterval);
                    return openedInterval.CloneWithNewBoundaryValue((openedInterval.Boundary.Value - A) / K);
                }

                return v;
            }).ToList();

            return averageWeightedInterval.Calculate(newValues);
        }
    }
}
