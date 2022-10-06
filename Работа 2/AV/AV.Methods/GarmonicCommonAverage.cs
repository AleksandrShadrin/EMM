namespace AV.Methods
{
    public class GarmonicCommonAverage : IAverageDiscreteEvaluator
    {
        public double Calculate(IEnumerable<double> values)
            => values.Count() / values.Select(x => 1 / x).Sum();
    }
}
