namespace DS.Presentation.Services
{
    public class SeriesGeneratorWithNoise : ISeriesGenerator
    {
        private readonly Random random;

        public SeriesGeneratorWithNoise(Random random)
        {
            this.random = random;
        }

        public Serie GenerateSerieForExponent(double start, double step, int points)
        {
            var xValues = GenerateXValues(start, step, points);
            var a0 = random.NextDouble();
            var a1 = random.NextDouble();
            var yValues = xValues
                .Select(v => a0 * Math.Exp(a1 * v)
                    + random.NextDouble()
                    * (random.NextDouble() > 0.5 ? -1 : 1));

            return new Serie(xValues, yValues);
        }

        public Serie GenerateSerieForLogarithm(double start, double step, int points)
        {
            var xValues = GenerateXValues(start, step, points);
            var a0 = random.NextDouble();
            var a1 = random.NextDouble();
            var yValues = xValues
                .Select(v => a0 + a1 * Math.Log(v)
                    + random.NextDouble()
                    * (random.NextDouble() > 0.5 ? -1 : 1));

            return new Serie(xValues, yValues);
        }

        public Serie GenerateSerieForPolynom(double start, double step, int points, int degree)
        {
            var xValues = GenerateXValues(start, step, points);

            var coefs = Enumerable
                .Range(0, degree + 1)
                .Select(_ => random.NextDouble())
                .ToList();

            var yValues = xValues
                .Select(v =>
                    coefs.Select(
                        (c, i) => c * Math.Pow(v, i))
                    .Sum() + random.NextDouble()
                    * (random.NextDouble() > 0.5 ? -1 : 1));

            return new Serie(xValues, yValues);
        }

        private IEnumerable<double> GenerateXValues(double start, double step, int points)
            => Enumerable.Range(0, points)
                .Select(p => start + p * step);
    }
}
