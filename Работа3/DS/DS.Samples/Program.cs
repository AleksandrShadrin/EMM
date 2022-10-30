using DS.SeriesAnalysis.Services;

var random = new Random(123);

var seriesAnalyzerService = new SeriesAnalyzerService();
var seriesSmootherService = new SeriesSmoothingService();
// seriesSmootherService.SetSeriesSmootherType(DS.SeriesAnalysis.Constants.SmoothType.SMOOTH_BY_FIVE_POINTS);

var tValues = Enumerable.Range(1, 10).Select(v => (double)v);
var yValues = tValues.Select(v => func(v));

var series = new Serie(tValues, yValues);

foreach (var value in series.Y)
{
    Console.Write($"{value:0.00} ");
}

Console.WriteLine();

foreach (var value in seriesSmootherService.SmoothSerie(series).Y)
{
    Console.Write($"{value:0.00} ");
}

double func(double t)
{
    return t + 1.1 * random.NextDouble() * (random.NextDouble() > 0.5 ? -1 : 1);
}