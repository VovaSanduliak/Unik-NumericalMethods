using System.Diagnostics;

ulong[] nValues = [
    10000,
    1000000,
    100000000,
    1000000000,
];
const double piSquaredOver6 = Math.PI * Math.PI / 6.0;

SetColor(ConsoleColor.White);
Console.WriteLine("Calculating series sums for different n values:");

foreach (ulong n in nValues)
{
    // timers
    Stopwatch swS1Float = new Stopwatch();
    Stopwatch swS2Float = new Stopwatch();
    Stopwatch swS1Double = new Stopwatch();
    Stopwatch swS2Double = new Stopwatch();
    
    swS1Float.Start();
    float s1Float = CalculateFloatS1(n);
    swS1Float.Stop();
    
    swS2Float.Start();
    float s2Float = CalculateFloatS2(n);
    swS2Float.Stop();
    
    swS1Double.Start();
    double s1Double = CalculateDoubleS1(n);
    swS1Double.Stop();
    
    swS2Double.Start();
    double s2Double = CalculateDoubleS2(n);
    swS2Double.Stop();
    
    SetColor(ConsoleColor.Blue);
    Console.WriteLine($"\nFor n = {n}:");
    SetColor(ConsoleColor.White);
    
    Console.WriteLine($"S1 (float)  = {s1Float},  Time: {swS1Float.ElapsedMilliseconds} ms");
    Console.WriteLine($"S2 (float)  = {s2Float}   Time: {swS2Float.ElapsedMilliseconds} ms");
    Console.WriteLine($"S1 (double) = {s1Double}, Time: {swS1Double.ElapsedMilliseconds} ms");
    Console.WriteLine($"S2 (double) = {s2Double}, Time: {swS2Double.ElapsedMilliseconds} ms");
    
    SetColor(ConsoleColor.Green);
    Console.WriteLine($"Difference from п^2/6 (S1 float) = {Math.Abs((double)s1Float - piSquaredOver6)}");
    Console.WriteLine($"Difference from п^2/6 (S2 float) = {Math.Abs((double)s2Float - piSquaredOver6)}");
    Console.WriteLine($"Difference from п^2/6 (S1 double) = {Math.Abs((double)s1Double - piSquaredOver6)}");
    Console.WriteLine($"Difference from п^2/6 (S2 double) = {Math.Abs((double)s2Double - piSquaredOver6)}");
    SetColor(ConsoleColor.White);
}

Console.WriteLine("end...");
return;

float CalculateFloatS1(ulong n)
{
    float sum = 0.0f;
    for (ulong k = 1; k <= n; k++)
    {
        sum += 1.0f / (k * k);
    }

    return sum;
}

float CalculateFloatS2(ulong n)
{
    float sum = 0.0f;
    for (ulong k = n; k >= 1; k--)
    {
        sum += 1.0f / (k * k);
    }

    return sum;
}

double CalculateDoubleS1(ulong n)
{
    double sum = 0.0d;
    for (ulong k = 1; k <= n; k++)
    {
        sum += 1.0d / (k * k);
    }

    return sum;
}

double CalculateDoubleS2(ulong n)
{
    double sum = 0.0d;
    for (ulong k = n; k >= 1; k--)
    {
        sum += 1.0d / (k * k);
    }

    return sum;
}

void SetColor(ConsoleColor color)
{
    Console.ForegroundColor = color;
}