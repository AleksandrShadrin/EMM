using AV.Methods.ValueObjects;

namespace AV.Methods.Factories
{
    public class OpenedIntervalFactory
    {
        public BaseInterval CreateOpenedIntervalWithRightIncludedBoundary(double value)
        {
            var boundary = new Boundary(value, true);

            return new OpenedInterval(boundary);
        }

        public BaseInterval CreateOpenedIntervalWithRightExcludedBoundary(double value)
        {
            var boundary = new Boundary(value);

            return new OpenedInterval(boundary);
        }
        public BaseInterval CreateOpenedIntervalWithLeftIncludedBoundary(double value)
        {
            var boundary = new Boundary(value, true);

            return new OpenedInterval(boundary, false);
        }

        public BaseInterval CreateOpenedIntervalWithLeftExcludedBoundary(double value)
        {
            var boundary = new Boundary(value);

            return new OpenedInterval(boundary, false);
        }
    }
}
