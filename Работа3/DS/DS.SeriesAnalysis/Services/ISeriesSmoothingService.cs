using DS.SeriesAnalysis.Constants;

namespace DS.SeriesAnalysis.Services
{
    public interface ISeriesSmoothingService
    {
        void SetSeriesSmootherType(SmoothType smoothType);
        Serie SmoothSerie(Serie serie);
    }
}