using CustomMatrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatrixWeb.Patterns
{
    public abstract class FactoryMethod
    {
        public abstract IMatrix CreateInstance(int row, int col);
    }

    public class MatrixCreator: FactoryMethod
    {
        public override IMatrix CreateInstance(int row, int col)
        {
            return new CustomMatrix.Matrix(row, col);
        }
    }

    public class SleCreator : FactoryMethod
    {
        public override IMatrix CreateInstance(int row, int col)
        {
            return new SLE(row, col);
        }
    }
}