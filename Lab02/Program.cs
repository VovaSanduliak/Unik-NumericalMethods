double[,] C =
{
    { 0.2, 2.7, 0.4, 0.5 },
    { 1.9, 0.5, 0.7, 0.6 },
    { 0.4, 2.3, 0.3, 0.8 },
    { 0.2, 0.1, 0.9, 1.2 }
};

double[] z =
{
    296.11,
    136.43,
    265.47,
    107.13
};

Gaussian(C, z);

static double[] Gaussian(double[,] C, double[] z)
{
    int n = z.Length;
    for (int i = 0; i < n; i++)
    {
        int max = i;
        for (int j = i + 1; j < n; j++)
        {
            if (Math.Abs(C[j, i]) > Math.Abs(C[max, i]))
            {
                max = j;
            }
        }
        
        for (int k = i; k < n; k++)
        {
            (C[max, k], C[i, k]) = (C[i, k], C[max, k]);
        }
        (z[max], z[i]) = (z[i], z[max]);
        
        for (int j = i + 1; j < n; j++)
        {
            double factor = C[j, i] / C[i, i];
            z[j] -= factor * z[i];
            for (int k = i; k < n; k++)
            {
                C[j, k] -= factor * C[i, k];
            }
        }
    }

    
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


for (int i = 0; i < C.GetLength(0); i++)
{
    for (int j = 0; j < C.GetLength(1); j++)
    {
        Console.Write(C[i, j] + "\t");
    }
    Console.WriteLine();
}