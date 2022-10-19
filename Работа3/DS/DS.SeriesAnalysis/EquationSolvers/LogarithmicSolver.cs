namespace DS.SeriesAnalysis.EquationSolvers;

public class LogarithmicSolver : IEquationSolver
{
    private List<double> _coeffs = new();
    IEquationSolver _equationSolver = new PolynomialSolver(1);

    public void FindCoeffs(Serie serie)
    {
        var newSerie = new Serie(
            serie.X.Select(x => Math.Log(x)).ToList(),
            serie.Y.Select(y => y));

        _equationSolver.FindCoeffs(newSerie);

        _coeffs = _equationSolver.GetCoeffs().ToList();
    }

    public double GetValueAtPoint(double t)
    {
        return _coeffs.ElementAt(0) + _coeffs.ElementAt(1) * Math.Log(t);
    }

    public IEnumerable<double> GetCoeffs()
        => _coeffs;
}