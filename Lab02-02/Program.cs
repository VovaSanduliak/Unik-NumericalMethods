namespace Lab02_02
{
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
            
            // double[,] C =
            // {
            //     { 3, 1, 0.5, },
            //     { 0.5, 2, 0.75 },
            //     { 0.2, 0.5, 1 }
            // };
            //
            // double[] z = [15, 20, 7];
            
            
            
            double[] initialGuess = [0, 0, 0, 0];
            double tolerance = 1e-6;
            int maxIterations = 100;

            GaussianElimination(C, z);
            
            double[] solution = Jacobi(C, z, initialGuess, tolerance, maxIterations);

            ShowMatrix(C, z);
            Console.WriteLine();
            
            Console.WriteLine("Solution:");
            for (int i = 0; i < solution.Length; i++)
            {
                Console.WriteLine($"x[{i + 1}] = {solution[i]:0.000}");
            }

            if (IsDiagonallyDominant(C)) Console.WriteLine(true);
            else Console.WriteLine(false);
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
        
        static bool IsDiagonallyDominant(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                double diagonalElement = Math.Abs(matrix[i, i]);
                double sumOtherElements = 0;

                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        sumOtherElements += Math.Abs(matrix[i, j]);
                    }
                }

                if (diagonalElement <= sumOtherElements)
                {
                    return false;
                }
            }
            return true;
        }
        
        static void SwapRows(ref double[,] matrix, int row1, int row2)
        {
            int n = matrix.GetLength(1);

            for (int i = 0; i < n; i++)
            {
                (matrix[row1, i], matrix[row2, i]) = (matrix[row2, i], matrix[row1, i]);
            }
        }

        static void SwapElements(ref double[] array, int index1, int index2)
        {
            (array[index1], array[index2]) = (array[index2], array[index1]);
        }
        
        static double[] Jacobi(double[,] C, double[] z, double[] initialGuess, double tolerance, int maxIterations)
        {
            int n = z.Length;
            double[] xOld = new double[n];
            double[] xNew = new double[n];

            // Initialize the guess
            Array.Copy(initialGuess, xOld, n);

            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                // Jacobi iteration
                for (int i = 0; i < n; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (i != j)
                        {
                            sum += C[i, j] * xOld[j];
                        }
                    }

                    xNew[i] = (z[i] - sum) / C[i, i];
                }

                // Check for convergence
                double maxDiff = 0;
                for (int i = 0; i < n; i++)
                {
                    maxDiff = Math.Max(maxDiff, Math.Abs(xNew[i] - xOld[i]));
                }

                if (maxDiff < tolerance)
                {
                    Console.WriteLine($"Converged in {iteration + 1} iterations.");
                    return xNew;
                }

                // Update xOld for next iteration
                Array.Copy(xNew, xOld, n);
            }

            Console.WriteLine("Max iterations reached.");
            return xNew;
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

    }
    
    
}
