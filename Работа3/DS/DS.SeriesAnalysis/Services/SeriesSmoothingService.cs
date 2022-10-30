using DS.SeriesAnalysis.Constants;
using DS.SeriesAnalysis.SeriesSmoothers;

namespace DS.SeriesAnalysis.Services
{
    public class SeriesSmoothingService : ISeriesSmoothingService
    {
        private ISeriesSmoother _seriesSmoother = new SeriesSmootherByThreePoints();

        public void SetSeriesSmootherType(SmoothType smoothType)
        {
            switch (smoothType)
            {
                case SmoothType.SMOOTH_BY_THREE_POINTS:
                    _seriesSmoother = new SeriesSmootherByThreePoints();
                    break;
                case SmoothType.SMOOTH_BY_FIVE_POINTS:
                    _seriesSmoother = new SeriesSmootherByFivePoints();
                    break;
            }
        }

        public Serie SmoothSerie(Serie serie)
            => _seriesSmoother.Smooth(serie);
    }
}