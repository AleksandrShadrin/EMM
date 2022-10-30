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
        _medianCalculator = new MedianForDiscrete(_serie.Y.Select(_ => 1d));
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

    public void SetSerie(Serie serie)
    {
        _serie = serie;
        _medianCalculator = new MedianForDiscrete(_serie.Y.Select(_ => 1d));
    }

    public double GetF()
    {
        var n = _serie.X.Count();
        var p = 0.95;

        var mean_y = _serie.Y.Sum() / n;

        var sigma_fact = _serie.Y
            .Select(v => Math.Pow(v - mean_y, 2))
            .Sum() / n;

        var sigma_ost = _serie.Y
            .Zip(GetTrend().Y,
            (y1, y2) => Math.Pow(y1 - y2, 2))
            .Sum() / n;

        var R = 1 - sigma_ost / sigma_fact;

        return R / (1 - R) * (n - 2);
    }

    private int NumberOfSeries()
    {
        var median = _medianCalculator.Calculate(_serie.Y.OrderBy(v => v));

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