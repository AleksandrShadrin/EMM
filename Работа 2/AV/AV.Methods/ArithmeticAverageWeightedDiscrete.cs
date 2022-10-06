using AV.Methods.Exceptions;

namespace AV.Methods
{
    public class ArithmeticAverageWeightedDiscrete : IAverageDiscreteEvaluator
    {
        protected List<double> _weights;

        public ArithmeticAverageWeightedDiscrete(List<double> weights)
        {
            _weights = weights;
        }

        public void SetWeights(IEnumerable<double> weights)
        {
            _weights = weights.ToList();
        }

        public double Calculate(IEnumerable<double> values)
        {
            if(values.Count() != _weights.Count())
            {
                throw new WeightsCountDontEqualValuesCount(_weights.Count(), values.Count());
            }

            return Enumerable
                .Zip(values, _weights , 
                (f, s) => f * s)
                .Sum() / _weights.Sum();
        }
    }
}
