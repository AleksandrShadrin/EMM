using MatrixDotNet;
using MatrixDotNet.Extensions.Solver;

namespace DS.SeriesAnalysis.EquationSolvers;

public class PolynomialSolver : IEquationSolver
{
    private int _degree;
    private List<double> _coeffs = new();

    public PolynomialSolver(int degree = 2)
    {
        _degree = degree;
    }

    public void FindCoeffs(Serie serie)
    {
        List<List<double>> left = new();
        List<double> right = new();

        for (int i = 0; i < _degree + 1; i++)
        {
            var row = new List<double>();

            row.AddRange(
                Enumerable
                    .Range(0, _degree + 1)
                    .Select(value =>
                        serie.X.Select(x => Math.Pow(x, i + value)).Sum()
                    ).ToList());

            left.Add(row);
            right.Add(serie.Y.Zip(serie.X, (y, x) => Math.Pow(x, i) * y).Sum());
        }

        Matrix<double> matrix = new Matrix<double>(_degree + 1, _degree + 1);

        for (int i = 0; i < left.Count; i++)
        {
            matrix[i] = left.ElementAt(i).ToArray();
        }

        _coeffs = matrix.GaussSolve(right.ToArray()).ToList();
    }

    public double GetValueAtPoint(double t)
    {
        return Enumerable.Range(0, _degree + 1)
            .Select(p => Math.Pow(t, p))
            .Zip(_coeffs, (t, c) => t * c).Sum();
    }

    public IEnumerable<double> GetCoeffs()
        => _coeffs;
}