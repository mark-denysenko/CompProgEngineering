using CustomMatrix;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomMatrix.Tests
{
    [TestClass]
    public class MatrixTest
    {
        [TestMethod()]
        public void DeterminantTest2x2()
        {
            var matrix = GetMockMatrix2x2();
            
            Assert.AreEqual(-10.0, matrix.GetDeterminant());
        }

        [TestMethod()]
        public void DeterminantTest3x3()
        {
            var matrix = GetMockMatrix3x3();

            Assert.AreEqual(21.0, matrix.GetDeterminant());
        }

        [TestMethod()]
        public void DeterminantTestNotSquareMatrix()
        {
            var matrix = Matrix.Random(2, 3, 0);

            Assert.ThrowsException<Exception>(() => matrix.GetDeterminant());
        }

        [TestMethod()]
        public void MatrixRightSizeInitialization()
        {
            var matrix = new Matrix(4, 2);

            Assert.AreEqual(4, matrix.Rows);
            Assert.AreEqual(2, matrix.Cols);
        }

        private Matrix GetMockMatrix2x2()
        {
            Matrix m = new Matrix(2, 2);

            m[0, 0] = 1.0; m[0, 1] = 3.0;
            m[1, 0] = 4.0; m[1, 1] = 2.0;

            return m;
        }

        private Matrix GetMockMatrix3x3()
        {
            Matrix m = new Matrix(3, 3);

            m[0, 0] = 1.0; m[0, 1] = 3.0; m[0, 2] = 2.0;
            m[1, 0] = 4.0; m[1, 1] = 2.0; m[1, 2] = 5.0;
            m[2, 0] = 2.0; m[2, 1] = 3.0; m[2, 2] = 1.0;

            return m;
        }
    }
}
