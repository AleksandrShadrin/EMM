using DS.SeriesAnalysis.Constants;

namespace DS.SeriesAnalysis.Services;

public interface ISeriesAnalyzerService
{
    void Fit(Serie series, EquationType equationType, int degree = 2);
    Serie GetTrend();
    bool TrendExist();
    void SetSerie(Serie series);
    double GetF();
}