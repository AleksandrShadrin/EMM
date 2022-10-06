namespace AV.Methods.Exceptions
{
    public class WeightsCountDontEqualValuesCount : AVMethodsException
    {
        public int WeightsCount { get; }
        public int ValuesCount { get; }
        public WeightsCountDontEqualValuesCount(int weightsCount, int valuesCount) : base($"Weights count is {weightsCount}, values count is {valuesCount}.")
        { 
            WeightsCount = weightsCount;
            ValuesCount = valuesCount;
        }
    }
}
