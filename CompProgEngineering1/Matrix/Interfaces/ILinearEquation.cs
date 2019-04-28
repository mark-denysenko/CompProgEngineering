using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMatrix
{
    public interface ILinearEquation
    {
        double[] CalculateEquation(double[,] X, double[] Y);
    }
}
