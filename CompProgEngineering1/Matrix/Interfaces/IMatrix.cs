using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMatrix
{
    public interface IMatrix : IDeterminant
    {
        int Rows { get; }
        int Cols { get; }
        double GetDeterminant();
        double this[int x, int y] { get; set; }
    }
}
