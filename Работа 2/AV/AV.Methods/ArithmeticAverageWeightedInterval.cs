using AV.Methods.Exceptions;
using AV.Methods.ValueObjects;

namespace AV.Methods
{
    public class ArithmeticAverageWeightedInterval : IAverageIntervalEvaluator
    {
        private readonly ArithmeticAverageWeightedDiscret averageWeightedDiscret;

        public ArithmeticAverageWeightedInterval(List<double> weights)
        {
            averageWeightedDiscret = new ArithmeticAverageWeightedDiscret(weights);
        }

        public void SetWeights(IEnumerable<double> weights)
        {
            averageWeightedDiscret.SetWeights(weights);
        }

        public double Calculate(IEnumerable<BaseInterval> values)
        {
            var closedIntervals = values
                .Where(v => v is ClosedInterval)
                .Select(v =>
                {
                    var value = v as ClosedInterval;
                    return (value.RightBoundary.Value + value.LeftBoundary.Value) / 2;
                })
                .ToList();

            if (values.Where(v => v is OpenedInterval).Count() > 0)
            {
                var savedValues = new List<double>();
                if (values.First() is OpenedInterval)
                    savedValues.Add(closedIntervals.First());
                if (values.Last() is OpenedInterval)
                    savedValues.Add(closedIntervals.Last());

                if(values.First() is ClosedInterval && values.Last() is ClosedInterval)
                    throw new OpenedIntervalCanBeOnlyInTheEndOrStartException();

                closedIntervals.AddRange(savedValues);
            }

            return averageWeightedDiscret.Calculate(closedIntervals);
        }
    }
}
