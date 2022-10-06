namespace AV.Methods
{
    public class GarmonicCommonAverage : IAverageDiscretEvaluator
    {
        public double Calculate(IEnumerable<double> values)
            => values.Count() / values.Select(x => 1 / x).Sum();
    }
}
