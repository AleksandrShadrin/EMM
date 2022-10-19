namespace AV.Methods
{
    public class MedianForDiscrete : IAverageDiscreteEvaluator
    {
        private readonly IEnumerable<double> _weights;

        public MedianForDiscrete(IEnumerable<double> weights)
        {
            _weights = weights;
        }

        public double Calculate(IEnumerable<double> values)
        {
            var accumulatedFrequency = Enumerable
                .Range(0, _weights.Count() - 1)
                .Select(n =>
                    _weights.Take(n + 1)
                        .Aggregate(0d, (p, n) => p + n))
                .ToList();

            for (int i = 0; i < values.Count(); i++)
            {
                if (accumulatedFrequency[i] >= _weights.Sum() / 2)
                {
                    return values.ElementAt(i + 1);
                }
            }

            throw new ArgumentException(nameof(values));
        }
    }
}
