using CustomMatrix;
using MatrixWeb.Controllers;
using MatrixWeb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace CustomMatrix.Tests
{
    [TestClass]
    public class SleTest
    {
        [TestMethod]
        public void TestSleProcessing()
        {
            var sle = GetMockSle3x3();
            var sleService = new SleService();

            var result = sleService.CalculateEquation(sle.MatrixArray, sle.values);

            // x1
            Assert.AreEqual(-1.0, result[0]);
            // x2
            Assert.AreEqual(0.0, result[1]);
            // x3
            Assert.AreEqual(1.0, result[2]);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestSleNotSquareException()
        {
            var sle = GetSle3x2();

            var result = sle.CalculateEquation();
        }

        [TestMethod]
        public void TestSleControllerDeterminant()
        {
            bool IsCalculatedEquation = false;
            var linearEquation = new Mock<ILinearEquation>();
            linearEquation.Setup(s => s.CalculateEquation(It.IsAny<double[,]>(), It.IsAny<double[]>()))
                .Callback(() => IsCalculatedEquation = true);

            new SleController(linearEquation.Object).SleCalculation(new SleView());

            Assert.IsTrue(IsCalculatedEquation);

        }

        private SLE GetMockSle3x3()
        {
            double[] xi = new double[] { 1.0, 3.0, 2.0 };
            CustomMatrix.Matrix mat = new CustomMatrix.Matrix(3, 3);

            mat[0, 0] = 2.0; mat[0, 1] = -4.0; mat[0, 2] = 3.0;
            mat[1, 0] = 1.0; mat[1, 1] = -2.0; mat[1, 2] = 4.0;
            mat[2, 0] = 3.0; mat[2, 1] = -1.0; mat[2, 2] = 5.0;

            return new SLE(mat, xi);
        }

        private SLE GetSle3x2()
        {
            double[] xi = new double[] { 1.0, 3.0, 2.0 };
            CustomMatrix.Matrix mat = new CustomMatrix.Matrix(3, 2);

            mat[0, 0] = 2.0; mat[0, 1] = -4.0;
            mat[1, 0] = 1.0; mat[1, 1] = -2.0;
            mat[2, 0] = 3.0; mat[2, 1] = -1.0;

            return new SLE(mat, xi);
        }
    }
}
