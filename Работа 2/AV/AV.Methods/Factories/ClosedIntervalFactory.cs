using AV.Methods.ValueObjects;

namespace AV.Methods.Factories
{
    public class ClosedClosedIntervalFactory
    {
        public BaseInterval CreateClosedIntervalWithLeftInclude(double left, double right)
        {
            var leftBoundary = new Boundary(left, true);
            var rightBoundary = new Boundary(right);

            return new ClosedInterval(leftBoundary, rightBoundary);
        }

        public BaseInterval CreateClosedIntervalWithRightInclude(double left, double right)
        {
            var leftBoundary = new Boundary(left);
            var rightBoundary = new Boundary(right, true);

            return new ClosedInterval(leftBoundary, rightBoundary);
        }

        public BaseInterval CreateClosedIntervalWithBothInclude(double left, double right)
        {
            var leftBoundary = new Boundary(left, true);
            var rightBoundary = new Boundary(right, true);

            return new ClosedInterval(leftBoundary, rightBoundary);
        }
    }
}
