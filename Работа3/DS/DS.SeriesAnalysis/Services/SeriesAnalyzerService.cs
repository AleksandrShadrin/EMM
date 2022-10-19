using AV.Methods;
using DS.SeriesAnalysis.Constants;
using DS.SeriesAnalysis.EquationSolvers;

namespace DS.SeriesAnalysis.Services;

public class SeriesAnalyzerService : ISeriesAnalyzerService
{
    private IEquationSolver _solver;
    private Serie _serie;
    private MedianForDiscrete _medianCalculator;

    public SeriesAnalyzerService(Serie serie)
    {
        _serie = serie;
        _medianCalculator = new AV.Methods.MedianForDiscrete(_serie.Y.Select(_ => 1d));
    }

    public SeriesAnalyzerService()
    {
    }

    public void Fit(Serie serie, EquationType equationType, int degree = 2)
    {
        _solver = equationType switch
        {
            EquationType.POLYNOMIAL => new PolynomialSolver(degree),
            EquationType.EXPONENTIAL => new ExponentialSolver(),
            EquationType.LOGARITHMIC => new LogarithmicSolver(),
            _ => throw new ArgumentException(nameof(equationType))
        };

        _solver.FindCoeffs(serie);
    }

    public Serie GetTrend()
        => new Serie(
            _serie.X.Select(x => x).ToList(),
            _serie.X.Select(x => _solver.GetValueAtPoint(x)));

    public bool TrendExist()
    {
        var stdofSeries = 1.96 * Math.Sqrt((_serie.Y.Count - 1) / 4);
        var averageSeries = (_serie.Y.Count + 1) / 2;

        return NumberOfSeries() > averageSeries + stdofSeries
               || NumberOfSeries() < averageSeries - stdofSeries;
    }

    public void SetSerie(Serie series)
    {
        _serie = series;
        _medianCalculator = new AV.Methods.MedianForDiscrete(_serie.Y.Select(_ => 1d));
    }

    private int NumberOfSeries()
    {
        var median = _medianCalculator.Calculate(_serie.Y);

        var boolMask = _serie.Y.Select(y => y >= median).ToList();
        var numberOfSeries = 1;
        for (int i = 0; i < boolMask.Count - 1; i++)
        {
            if (boolMask.ElementAt(i) != boolMask.ElementAt(i + 1))
                numberOfSeries++;
        }

        return numberOfSeries;
    }
}