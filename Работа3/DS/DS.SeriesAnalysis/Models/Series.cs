public class Serie
{
    public List<double> X { get; init; }
    public List<double> Y { get; init; }

    public Serie(IEnumerable<double> x, IEnumerable<double> y)
    {
        X = x.ToList();
        Y = y.ToList();
    }
}