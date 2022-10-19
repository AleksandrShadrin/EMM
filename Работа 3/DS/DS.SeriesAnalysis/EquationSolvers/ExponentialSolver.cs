namespace DS.SeriesAnalysis.EquationSolvers;

public class ExponentialSolver : IEquationSolver
{
    private List<double> _coeffs = new();
    IEquationSolver _equationSolver = new PolynomialSolver(1);


    public void FindCoeffs(Serie serie)
    {
        var newSerie = new Serie(
            serie.X.Select(v => v).ToList(),
            serie.Y.Select(y => Math.Log(y)));

        _equationSolver.FindCoeffs(newSerie);

        _coeffs = _equationSolver.GetCoeffs().ToList();
    }

    public double GetValueAtPoint(double t)
    {
        return Math.Exp(_coeffs.ElementAt(0) + _coeffs.ElementAt(1) * t);
    }

    public IEnumerable<double> GetCoeffs()
        => _coeffs;
}