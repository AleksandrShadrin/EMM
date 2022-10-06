namespace AV.Methods.ValueObjects
{
    public class OpenedInterval : BaseInterval
    {
        public Boundary Boundary { get; }

        private bool boundedFromRight;

        public OpenedInterval(Boundary boundary, bool boundedFromRight = true)
        {
            this.boundedFromRight = boundedFromRight;
            Boundary = boundary is null ? throw new ArgumentNullException(nameof(boundary)) : boundary;
        }

        public bool BoundedFromRight()
            => boundedFromRight;

        public bool BoundedFromLeft()
            => !boundedFromRight;

        public OpenedInterval CloneWithNewBoundaryValue(double value)
        {
            return new(Boundary with { Value = value }, boundedFromRight: boundedFromRight);
        }

        public override string ToString()
        {
            if (boundedFromRight)
            {
                return $"До {Boundary}";
            }
            return $"Больше {Boundary}";
        }
    }
}
