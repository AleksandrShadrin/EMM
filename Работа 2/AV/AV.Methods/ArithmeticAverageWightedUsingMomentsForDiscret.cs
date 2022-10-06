namespace AV.Methods
{
    public class ArithmeticAverageWightedUsingMomentsForDiscret : IAverageDiscretEvaluator
    {
        public double A { get; }
        public double K { get; }
        public double k { get; }

        private readonly ArithmeticAverageWeightedDiscret averageWeightedDiscret;

        public ArithmeticAverageWightedUsingMomentsForDiscret(IEnumerable<double> weights, double a = 0, double K = 1, double k = 1)
        {
            A = a;
            this.K = K;
            this.k = k;

            averageWeightedDiscret = new(weights.Select(w => w / k).ToList());
        }

        public double Calculate(IEnumerable<double> values)
        {
            var newValues = values.Select(v => (v - A) / K).ToList();
            return averageWeightedDiscret.Calculate(newValues);
        }
    }
}
