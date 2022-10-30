namespace DS.SeriesAnalysis.SeriesSmoothers
{
    internal class SeriesSmootherByFivePoints : ISeriesSmoother
    {
        public Serie Smooth(Serie serie)
        {
            var serieCopy = serie.Copy();

            var y_first = (3 * serie.Y.First()
                + 2 * serie.Y.ElementAt(1)
                + serie.Y.ElementAt(2)
                - serie.Y.ElementAt(3)) / 5;

            var y_second = (4 * serie.Y.First()
                + 3 * serie.Y.ElementAt(1)
                + 2 * serie.Y.ElementAt(2)
                + serie.Y.ElementAt(3)) / 10;

            var y_last = (3 * serie.Y.Last()
                + 2 * serie.Y.ElementAt(serie.Y.Count - 2)
                + serie.Y.ElementAt(serie.Y.Count - 3)
                - serie.Y.ElementAt(serie.Y.Count - 4)) / 5;

            var y_pre_last = (4 * serie.Y.Last()
                + 3 * serie.Y.ElementAt(serie.Y.Count - 2)
                + 2 * serie.Y.ElementAt(serie.Y.Count - 3)
                + serie.Y.ElementAt(serie.Y.Count - 4)) / 10;

            for (int i = 2; i < serieCopy.Y.Count - 2; i++)
            {
                serieCopy.Y[i] = SmoothPoints(
                    serie.Y.ElementAt(i - 2),
                    serie.Y.ElementAt(i - 1),
                    serie.Y.ElementAt(i),
                    serie.Y.ElementAt(i + 1),
                    serie.Y.ElementAt(i + 2));
            }

            serieCopy.Y[0] = y_first;
            serieCopy.Y[1] = y_second;
            serieCopy.Y[^1] = y_last;
            serieCopy.Y[^2] = y_pre_last;

            return serieCopy;
        }

        private double SmoothPoints(double y1, double y2, double y3, double y4, double y5)
            => (y1 / 2 + y2 + y3 + y4 + y5 / 2) / 4;
    }
}
