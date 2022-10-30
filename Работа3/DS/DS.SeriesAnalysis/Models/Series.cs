public class Serie
{
    public List<double> X { get; init; }
    public List<double> Y { get; init; }

    public Serie(IEnumerable<double> x, IEnumerable<double> y)
    {
        X = x.ToList();
        Y = y.ToList();
    }

    public Serie Copy()
    {
        return new Serie(
            x: X.Select(v => v)
                .ToList(),
            y: Y.Select(v => v)
                .ToList());
    }
}