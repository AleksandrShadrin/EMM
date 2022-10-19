using DS.SeriesAnalysis.Services;

var random = new Random(123);

var seriesAnalyzerService = new SeriesAnalyzerService();

var tValues = Enumerable.Range(1, 10).Select(v => (double)v);
var yValues = tValues.Select(v => func(v));

var series = new Serie(tValues, yValues);
seriesAnalyzerService.SetSerie(series);

Console.WriteLine($"Trend exist: {seriesAnalyzerService.TrendExist()}");

double func(double t)
{
    return t + 1.1 * Random.Shared.Next() * (Random.Shared.Next() > 0.5 ? -1 : 1);
}