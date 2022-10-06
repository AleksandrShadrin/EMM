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

    }
}
