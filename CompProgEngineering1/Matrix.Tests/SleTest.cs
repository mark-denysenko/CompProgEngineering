using CustomMatrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix.Tests
{
    [TestClass]
    public class SleTest
    {
        [TestMethod]
        public void TestSleProcessing()
        {
            var sle = GetMockSle();

            var result = sle.CalculateEquation();

            // x1
            Assert.AreEqual(-1.0, result[0]);
            // x2
            Assert.AreEqual(0.0, result[1]);
            // x3
            Assert.AreEqual(1.0, result[2]);
        }

        private SLE GetMockSle()
        {
            double[] xi = new double[] { 1.0, 3.0, 2.0 };
            CustomMatrix.Matrix mat = new CustomMatrix.Matrix(3, 3);

            mat[0, 0] = 2.0; mat[0, 1] = -4.0; mat[0, 2] = 3.0;
            mat[1, 0] = 1.0; mat[1, 1] = -2.0; mat[1, 2] = 4.0;
            mat[2, 0] = 3.0; mat[2, 1] = -1.0; mat[2, 2] = 5.0;

            return new SLE(mat, xi);
        }
    }
}
