namespace AV.Methods.ValueObjects
{
    public class ClosedInterval : BaseInterval
    {
        public Boundary LeftBoundary { get; }
        public Boundary RightBoundary { get; }

        public ClosedInterval(Boundary leftBoundary, Boundary rightBoundary) : base()
        {
            RightBoundary = rightBoundary is null ? throw new ArgumentNullException(nameof(rightBoundary)) : rightBoundary;
            LeftBoundary = leftBoundary is null ? throw new ArgumentNullException(nameof(leftBoundary)) : leftBoundary;
        }
        public override string ToString()
        {
            return $"{(LeftBoundary.Include ? "[" : "(")}" +
                $"{LeftBoundary};{RightBoundary}" +
                $"{(RightBoundary.Include ? "]" : ")")}";
        }

        public ClosedInterval CloneWithNewBoundariesValues(double l, double r)
        {
            return new ClosedInterval(LeftBoundary with { Value = l }, RightBoundary with { Value = r });
        }
    }
}
