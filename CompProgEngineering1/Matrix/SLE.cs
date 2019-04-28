using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMatrix
{
    /// <summary>
    ///  СЛАУ
    /// </summary>
    public class SLE : Matrix
    {
        [Display(Name = "SLE right-values", Description = "Array of free values")]
        public double[] values;

        public SLE(int Row, int Col):
            base(Row, Col)
        {
            values = new double[Row];
        }

        public SLE(double[][] mat, double[] xi):
            base(mat)
        {
            values = xi;
        }

        public SLE(Matrix mat, double[] xi):
            base(mat.Rows, mat.Cols)
        {
            MatrixArray = mat.MatrixArray;
            values = xi;
        }

        public double[] CalculateEquation()
        {
            return ComputeCoefficents(this.MatrixArray, values);
        }

        public void GenerateRandom()
        {
            this.MatrixArray = Matrix.Random(Rows, Cols, DateTime.Now.Millisecond + 5).MatrixArray;

            var rand = new Random(DateTime.Now.Millisecond + 8);
            for (int i = 0; i < values.Length; i++)
                values[i] = rand.NextDouble() * 10.0;
        }

        public static double[] ComputeCoefficents(double[,] X, double[] Y)
        {
            int I, J, K1, N;
            N = Y.Length;

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
                        Y[I] /= X[I, K];
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
                        Y[I] -= Y[K];
                    }
                }
            }

            for (I = N - 2; I >= 0; I--)
            {
                for (J = N - 1; J >= I + 1; J--)
                {
                    Y[I] -= X[I, J] * Y[J];
                }
            }

            return Y;
        }

        public override string ToString()
        {
            string s = "\n";
            for (int r = 0; r < this._row; r++)
            {
                s += "| ";
                for (int c = 0; c < this._col; c++)
                {
                    //s += String.Format("{0:F4} ", _Mat[r, c]);F4 laat 0 weg bij decimals==>geen align op punt
                    s += _Mat[r, c].ToString("0.0000").PadLeft(8);
                }
                s += "|   " + values[r].ToString("0.0000") + "\n";
            }
            return s;
        }
    }
}
