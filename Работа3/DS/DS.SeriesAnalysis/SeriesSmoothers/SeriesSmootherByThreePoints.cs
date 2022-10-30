namespace DS.SeriesAnalysis.SeriesSmoothers
{
    internal class SeriesSmootherByThreePoints : ISeriesSmoother
    {
        public Serie Smooth(Serie serie)
        {
            var serieCopy = serie.Copy();

            var y_first = (5 * serie.Y.First()
                + 2 * serie.Y.ElementAt(1)
                - serie.Y.ElementAt(2)) / 6;

            var y_last = (5 * serie.Y.Last()
                + 2 * serie.Y.ElementAt(serie.Y.Count - 2)
                - serie.Y.ElementAt(serie.Y.Count - 3)) / 6;

            for (int i = 1; i < serieCopy.Y.Count - 1; i++)
            {
                serieCopy.Y[i] = SmoothPoints(
                    serie.Y.ElementAt(i - 1),
                    serie.Y.ElementAt(i),
                    serie.Y.ElementAt(i + 1));
            }

            serieCopy.Y[0] = y_first;
            serieCopy.Y[^1] = y_last;

            return serieCopy;
        }

        private double SmoothPoints(double y1, double y2, double y3)
            => (y1 / 2 + y2 + y3 / 2) / 2;
    }
}
