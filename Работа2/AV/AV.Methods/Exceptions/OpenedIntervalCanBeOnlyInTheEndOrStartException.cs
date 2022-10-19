namespace AV.Methods.Exceptions
{
    public class OpenedIntervalCanBeOnlyInTheEndOrStartException : AVMethodsException
    {
        public OpenedIntervalCanBeOnlyInTheEndOrStartException() : base("Opened interval in wrong position.")
        {
        }
    }
}
