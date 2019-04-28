using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMatrix
{
    public class MatrixService : IDeterminant
    {
        public double GetDeterminant(Matrix input)
        {
            if (!input.IsSquare())
            {
                throw new Exception("Matrix must be square to calculate determinant");
            }
            else
            {
                if (input.Rows > 2)
                {
                    double value = 0;
                    for (int j = 0; j < input.Rows; j++)
                    {
                        Matrix Temp = CreateSubMatrix(input, 0, j);
                        value = value + input[0, j] * (SignOfElement(0, j) * GetDeterminant(Temp));
                    }
                    return value;
                }
                else if (input.Rows == 2)
                {
                    return (input[0, 0] * input[1, 1]) - (input[1, 0] * input[0, 1]);
                }
                else
                {
                    return input[0, 0];
                }
            }
        }

        private int SignOfElement(int i, int j)
        {
            int sign = ((i + j) % 2 == 0) ? 1 : -1;
            return sign;
        }

        private Matrix CreateSubMatrix(Matrix input, int i, int j)
        {
            Matrix output;
            if (!input.IsSquare())
            {
                throw new Exception("Matrix must be square.");
            }
            else
            {
                output = new Matrix(input.Rows - 1, input.Rows - 1);
                int x = 0, y = 0;
                for (int m = 0; m < input.Rows; m++, x++)
                {
                    if (m != i)
                    {
                        y = 0;
                        for (int n = 0; n < input.Rows; n++)
                        {
                            if (n != j)
                            {
                                output[x, y] = input[m, n];
                                y++;
                            }
                        }
                    }
                    else
                    {
                        x--;
                    }
                }
            }
            return output;
        }
    }
}
