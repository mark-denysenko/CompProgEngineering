using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatrixWeb.Patterns
{
    public interface IMementoMatrix
    {
        MementoMatrix CreateMemento();
        void SetMemento(MementoMatrix memento);
    }

    public class MementoMatrix
    {
        public double[,] Matrix;
    }

    public interface IMementoSle
    {
        MementoSle CreateMemento();
        void SetMemento(MementoSle memento);
    }

    public class MementoSle: MementoMatrix
    {
        public double[] values;
    }
}