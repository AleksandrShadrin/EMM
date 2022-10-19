namespace AV.Methods
{
    public class GarmonicWeightedAverage : IAverageDiscreteEvaluator
    {
        private IEnumerable<double> _weights;
        public GarmonicWeightedAverage(IEnumerable<double> weights)
        {
            _weights = weights;
        }

        public void SetWeights(IEnumerable<double> weights)
        {
            _weights = weights;
        }

        public double Calculate(IEnumerable<double> values)
            => _weights.Sum() / Enumerable
                .Zip(_weights, values, (w, v) => w / v).Sum();
    }
}
