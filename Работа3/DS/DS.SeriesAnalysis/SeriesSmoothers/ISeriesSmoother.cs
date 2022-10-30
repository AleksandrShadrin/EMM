namespace DS.SeriesAnalysis.SeriesSmoothers
{
    internal interface ISeriesSmoother
    {
        Serie Smooth(Serie serie);
    }
}
