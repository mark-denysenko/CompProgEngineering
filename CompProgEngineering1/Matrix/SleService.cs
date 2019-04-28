using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMatrix
{
    public class SleService : ILinearEquation
    {
        public double[] CalculateEquation(double[,] X, double[] Y)
        {
            int I, J, K1, N;
            N = Y.Length;

            double[] result = new double[Y.Length];
            Y.CopyTo(result, 0);

            for (int K = 0; K < N; K++)
            {
                K1 = K + 1;
                for (I = K; I < N; I++)
                {
                    if (X[I, K] != 0)
                    {
                        for (J = K1; J < N; J++)
                        {
                            X[I, J] /= X[I, K];
                        }
                        result[I] /= X[I, K];
                    }
                }

                for (I = K1; I < N; I++)
                {
                    if (X[I, K] != 0)
                    {
                        for (J = K1; J < N; J++)
                        {
                            X[I, J] -= X[K, J];
                        }
                        result[I] -= result[K];
                    }
                }
            }

            for (I = N - 2; I >= 0; I--)
            {
                for (J = N - 1; J >= I + 1; J--)
                {
                    result[I] -= X[I, J] * result[J];
                }
            }

            return result;
        }
    }
}
