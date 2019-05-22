using CustomMatrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatrixWeb.Patterns
{
    public interface PrototypeMatrix
    {
        IMatrix Clone();
    }
}