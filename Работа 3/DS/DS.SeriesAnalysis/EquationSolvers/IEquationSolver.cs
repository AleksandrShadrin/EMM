public interface IEquationSolver
{
    void FindCoeffs(Serie serie);
    double GetValueAtPoint(double t);
    IEnumerable<double> GetCoeffs();
}