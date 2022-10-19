using AV.Methods.ValueObjects;

namespace AV.Methods
{
    public interface IAverageIntervalEvaluator
    {
        double Calculate(IEnumerable<BaseInterval> intervals);
    }
}
