using System;
using System.ComponentModel.DataAnnotations;

namespace CustomMatrix
{
    public class Matrix : IMatrix
    {
        [Display(Name = "Multidemensional array", Description = "Contains double values of matrix")]
        protected double[,] _Mat;

        [Display(Name = "Rows (Name)", Description = "Total rows")]
        protected int _row;
        [Display(Name = "Columns (Name)", Description = "Total columns")]
        protected int _col;

        public Matrix(int Row, int Col)
        {
            this._row = Row;
            this._col = Col;
            _Mat = new double[Row, Col];
        }

        public Matrix(int Row, int Col, double d):
            this(Row, Col)
        {
            for (int r = 0; r < _row; r++)
            {
                for (int c = 0; c < _col; c++)
                {
                    _Mat[r, c] = d;
                }
            }
        }

        private static int SignOfElement(int i, int j)
        {
            int sign = ((i + j) % 2 == 0) ? 1 : -1;
            return sign;
        }

        private static Matrix CreateSubMatrix(Matrix input, int i, int j)
        {
            Matrix output;
            if (!input.IsSquare())
            {
                throw new Exception("Matrix must be square.");
            }
            else
            {
                output = new Matrix(input._row - 1, input._col - 1);
                int x = 0, y = 0;
                for (int m = 0; m < input._row; m++, x++) //or _col
                {
                    if (m != i)
                    {
                        y = 0;
                        for (int n = 0; n < input._row; n++) //or _col
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

        public double this[int Row, int Col]
        {
            get { return _Mat[Row, Col]; }
            set { _Mat[Row, Col] = value; }
        }

        public int Rows
        {
            get { return _row; }
        }

        public int Cols
        {
            get { return _col; }
        }

        public double[,] MatrixArray
        {
            get { return _Mat; }
            set
            {
                this._row = value.GetUpperBound(0) + 1;
                this._col = value.GetUpperBound(1) + 1;
                _Mat = value;
            }
        }

        public bool IsSquare()
        {
            return _col == _row;
        }

        public bool IsSingular()
        {
            return Determinant(this) == 0.0;
        }

        public static Matrix Identity(int r, int c)
        {
            Matrix MA = new Matrix(r, c);
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    MA[i, j] = (i == j ? 1.0 : 0.0);
                }
            }
            return MA;
        }

        public static Matrix Random(int r, int c, int seed)
        {
            Random random = new Random(seed);
            Matrix R = new Matrix(r, c);
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    R[i, j] = random.NextDouble() * 10.0;
                }
            }
            return R;
        }

        public Matrix Transpose()
        {
            Matrix Trans = new Matrix(this._col, this._row);

            for (int i = 0; i < this._row; i++)
            {
                for (int j = 0; j < this._col; j++)
                {
                    Trans[j, i] = this[i, j];
                }
            }
            return Trans;
        }

        /// <summary>
        /// multiply this matrix by matrix M
        /// used in the * operator overload
        /// </summary>
        /// <param name="M"></param>
        /// <returns>multiplication</returns>
        public virtual Matrix Multiply(Matrix M)
        {
            Matrix X = new Matrix(this._row, M._col);
            for (int i = 0; i < this._row; i++)
            {
                for (int j = 0; j < M._col; j++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < this._col; k++) //this._col or M._row
                    {
                        sum += this[i, k] * M[k, j];
                    }
                    X[i, j] = sum;
                }
            }
            return X;
        }

        public static Matrix Minor(Matrix input, int row, int col)
        {
            Matrix mm = new Matrix(input.Rows - 1, input.Cols - 1);
            int ii = 0;
            int jj = 0;
            for (int r = 0; r < input.Rows; r++)
            {
                if (r == row) continue;
                jj = 0;
                for (int c = 0; c < input.Cols; c++)
                {
                    if (c == col) continue;
                    mm[ii, jj] = input[r, c];
                    jj++;
                }
                ii++;
            }
            return mm;
        }

        public double GetDeterminant()
        {
            return Determinant(this);
        }

        public static double Determinant(Matrix input)
        {
            if (!input.IsSquare())
            {
                throw new Exception("Matrix must be square to calculate determinant");
            }
            else
            {
                if (input._row > 2)
                {
                    double value = 0;
                    for (int j = 0; j < input._row; j++)
                    {
                        Matrix Temp = CreateSubMatrix(input, 0, j);
                        value = value + input[0, j] * (SignOfElement(0, j) * Determinant(Temp));
                    }
                    return value;
                }
                else if (input._row == 2)
                {
                    return (input[0, 0] * input[1, 1]) - (input[1, 0] * input[0, 1]);
                }
                else
                {
                    return input[0, 0];
                }
            }
        }

        public static Matrix Adjoint(Matrix m)
        {
            if (!m.IsSquare())
            {
                throw new ArgumentOutOfRangeException("Dimension", m.Rows,
                   "The matrix must be square!");
            }
            Matrix ma = new Matrix(m.Rows, m.Cols);
            for (int r = 0; r < m.Rows; r++)
            {
                for (int c = 0; c < m.Cols; c++)
                {
                    ma[r, c] = Math.Pow(-1, r + c) * Determinant(Minor(m, r, c));
                }
            }
            return ma.Transpose();
        }

        public static Matrix Inverse(Matrix m)
        {
            if (m.IsSingular())
            {
                throw new DivideByZeroException("Cannot inverse a matrix with a zero determinant!");
            }
            return Adjoint(m) / Determinant(m);
        }

        public static Matrix operator /(Matrix m, double d)
        {
            Matrix result = new Matrix(m.Rows, m.Cols);
            for (int r = 0; r < m.Rows; r++)
            {
                for (int c = 0; c < m.Cols; c++)
                {
                    result[r, c] = m[r, c] / d;
                }
            }
            return result;
        }

        public override string ToString()
        {
            string s = "\n";
            for (int r = 0; r < this._row; r++)
            {
                s += "| ";
                for (int c = 0; c < this._col; c++)
                {
                    s += _Mat[r, c].ToString("0.0000").PadLeft(8);
                }
                s += "|\n";
            }
            return s;
        }
    }
}
