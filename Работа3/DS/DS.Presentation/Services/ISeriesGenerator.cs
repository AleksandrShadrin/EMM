namespace DS.Presentation.Services
{
    public interface ISeriesGenerator
    {
        Serie GenerateSerieForPolynom(double start, double step, int points, int degree);
        Serie GenerateSerieForExponent(double start, double step, int points);
        Serie GenerateSerieForLogarithm(double start, double step, int points);
    }
}
