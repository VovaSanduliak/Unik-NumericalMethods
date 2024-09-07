namespace Lab02;

static class Program
{
    static void Main(string[] args)
    {
        double[,] C =
        {
            { 0.2, 2.7, 0.4, 0.5 },
            { 1.9, 0.5, 0.7, 0.6 },
            { 0.4, 2.3, 0.3, 0.8 },
            { 0.2, 0.1, 0.9, 1.2 }
        };

        double[] z =
        [
            296.11,
            136.43,
            265.47,
            107.13
        ];

        ShowMatrix(C, z);
        Console.WriteLine();

        GaussianElimination(C, z);

        Console.WriteLine("Final matrix:");
        ShowMatrix(C, z);

        double[] solution = BackSubstitution(C, z);
        Console.WriteLine();
        Console.WriteLine("Solution:");
        for (int i = 0; i < solution.Length; i++)
        {
            Console.WriteLine($"x[{i + 1}] = {solution[i]:0.000}");
        }
    }

    static void ShowMatrix(double[,] C, double[] z)
    {
        int n = z.Length;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write($"{C[i, j]:0.000}\t");
            }

            Console.WriteLine($"| {z[i]:0.000}");
        }
    }
    
    static void GaussianElimination(double[,] C, double[] z)
    {
        int n = z.Length;

        for (int i = 0; i < n; i++)
        {
            int maxRow = i;
            for (int j = i + 1; j < n; j++)
            {
                if (Math.Abs(C[j, i]) > Math.Abs(C[maxRow, i]))
                {
                    maxRow = j;
                }
            }

            for (int j = i; j < n; j++)
            {
                (C[maxRow, j], C[i, j]) = (C[i, j], C[maxRow, j]);
            }

            (z[maxRow], z[i]) = (z[i], z[maxRow]);

            for (int j = i + 1; j < n; j++)
            {
                double factor = C[j, i] / C[i, i];
                z[j] -= factor * z[i];
                for (int k = i; k < n; k++)
                {
                    C[j, k] -= factor * C[i, k];
                }
                
                ShowMatrix(C, z);
                Console.WriteLine();
            }
        }
    }
    
    static double[] BackSubstitution(double[,] C, double[] z)
    {
        int n = z.Length;
        double[] x = new double[n];

        for (int i = n - 1; i >= 0; i--)
        {
            x[i] = z[i];
            for (int j = i + 1; j < n; j++)
            {
                x[i] -= C[i, j] * x[j];
            }

            x[i] /= C[i, i];
        }

        return x;
    }
}