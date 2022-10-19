using AV.Methods.ValueObjects;

namespace AV.Methods
{
    public class MedianForInterval : IAverageIntervalEvaluator
    {
        private IEnumerable<double> _weights;

        public MedianForInterval(IEnumerable<double> weights)
        {
            _weights = weights;
        }



        public double Calculate(IEnumerable<BaseInterval> intervals)
        {
            var accumulatedFrequency = Enumerable
                .Range(0, _weights.Count() - 1)
                .Select(n =>
                    _weights.Take(n + 1)
                        .Aggregate(0d, (p, n) => p + n))
                .ToList();

            var medianInterval = 0;

            for (int i = 0; i < _weights.Count(); i++)
            {
                if (accumulatedFrequency[i] >= _weights.Sum() / 2)
                {
                    medianInterval = i;
                    break;
                }
            }

            var interval = (intervals.ElementAt(medianInterval) as ClosedInterval);

            return interval.LeftBoundary.Value
                + (interval.RightBoundary.Value - interval.LeftBoundary.Value)
                * (0.5 * _weights.Sum() - accumulatedFrequency[medianInterval - 1])
                / _weights.ElementAt(medianInterval);
        }
    }
}
