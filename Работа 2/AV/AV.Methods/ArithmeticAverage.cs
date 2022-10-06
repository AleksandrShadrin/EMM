namespace AV.Methods
{
    public class ArithmeticAverage : IAverageDiscretEvaluator
    {
        public double Calculate(IEnumerable<double> values)
            => values.Average();
    }
}
