namespace AV.Methods.ValueObjects
{
    public record Boundary
    {
        public bool Include { get; init; }
        public double Value { get; init; }

        public Boundary(double value, bool include = false)
        {
            Value = value;
            Include = include;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
