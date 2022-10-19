namespace AV.Methods
{
    public class ArithmeticAverage : IAverageDiscreteEvaluator
    {
        public double Calculate(IEnumerable<double> values)
            => values.Average();
    }
}
