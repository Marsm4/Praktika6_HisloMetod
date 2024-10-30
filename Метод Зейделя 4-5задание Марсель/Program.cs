using System;

class SeidelMethod
{
    static double[] SolveBySeidel(double[,] A, double[] B, double epsilon = 1e-4, int maxIterations = 1000)
    {
        int n = B.Length;
        double[] X = new double[n]; // начальные значения равны нулю
        double[] X_prev = new double[n]; // массив для предыдущего значения

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            Array.Copy(X, X_prev, n); // сохранить предыдущие значения

            for (int i = 0; i < n; i++)
            {
                double sum1 = 0.0; // сумма для уже обновленных значений
                double sum2 = 0.0; // сумма для еще не обновленных значений

                for (int j = 0; j < i; j++)
                {
                    sum1 += A[i, j] * X[j]; // использовать уже обновленные значения
                }

                for (int j = i + 1; j < n; j++)
                {
                    sum2 += A[i, j] * X_prev[j]; // использовать предыдущие значения
                }

                X[i] = (B[i] - sum1 - sum2) / A[i, i];
            }

            // Проверка на сходимость
            double maxDifference = 0.0;
            for (int i = 0; i < n; i++)
            {
                maxDifference = Math.Max(maxDifference, Math.Abs(X[i] - X_prev[i]));
            }

            if (maxDifference < epsilon)
            {
                Console.WriteLine($"Решение достигнуто на итерации {iteration + 1}");
                return X;
            }
        }

        Console.WriteLine("Решение не сходится за заданное количество итераций.");
        return null;
    }

    static void Main()
    {
        double[,] A = {
           {22.52, -4.62, -1.41 },
            {-5.10, -28.37, 4.58 },
            {4.68, -1.91, 23.85 }
      
        };
        double[] B = { 0.53, -8.78, 5.14};
        double epsilon = 1e-4;

        double[] solution = SolveBySeidel(A, B, epsilon);

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
