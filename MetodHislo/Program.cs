using System;

class IterativeMethod
{
    static double[] SolveByIteration(double[,] A, double[] B, double epsilon = 1e-4, int maxIterations = 1000)
    {
        int n = B.Length;
        double[] X = new double[n];
        double[] X_new = new double[n];

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            Array.Copy(X, X_new, n);

            for (int i = 0; i < n; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        sum += A[i, j] * X[j];
                    }
                }
                X_new[i] = (B[i] - sum) / A[i, i];
            }

            // Критерий сходимости: проверка на разницу между X и X_new
            double maxDifference = 0.0;
            for (int i = 0; i < n; i++)
            {
                maxDifference = Math.Max(maxDifference, Math.Abs(X_new[i] - X[i]));
            }

            if (maxDifference < epsilon)
            {
                Console.WriteLine($"Решение достигнуто на итерации {iteration + 1}");
                return X_new;
            }

            Array.Copy(X_new, X, n); // Переход к следующей итерации
        }

        Console.WriteLine("Решение не сходится за заданное количество итераций.");
        return null;
    }

    static void Main()
    {
        double[,] A = {
            {24.58 , -0.18 ,  -7.14  , -5.06, 8.00 },
            { 6.98 , 13.75 ,  1.10 ,  7.43, -4.96 },
            {-7.20 ,  1.42 , 26.33 , 4.35, 0.58},
            {-6.70 ,  -5.30  ,  -1.20 ,  -21.02, 6.55},
            {-6.19 ,  -8.56  ,  -3.08 ,  -6.76, -13.61}
        };
        double[] B = { 4.26, -6.73, -2.19, 8.45, -4.09 };
        double epsilon = 1e-4;

        double[] solution = SolveByIteration(A, B, epsilon);

        if (solution != null)
        {
            Console.WriteLine("Решение:");
            for (int i = 0; i < solution.Length; i++)
            {
                Console.WriteLine($"X[{i}] = {solution[i]:F4}");
            }
        }
    }
}
