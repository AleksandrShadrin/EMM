namespace AV.Methods.ValueObjects
{
    public class Boundary
    {
        public bool Include { get; set; }
        public double Value { get; set; }

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
