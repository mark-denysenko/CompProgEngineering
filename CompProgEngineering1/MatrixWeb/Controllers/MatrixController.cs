using CustomMatrix;
using MatrixWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatrixWeb.Controllers
{
    public class MatrixController : Controller
    {
        private IDeterminant _matrix;

        public MatrixController(IDeterminant matrix)
        {
            _matrix = matrix;
        }

        // GET: Matrix
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetUpMatrix(int size = 2)
        {
            var matrix = new MatrixView
            {
                Data = new double[size][]
            };
            for (int i = 0; i < matrix.Data.Length; i++)
                matrix.Data[i] = new double[size];

            return View(matrix);
        }

        [HttpPost]
        public ActionResult Determinant(MatrixView model)
        {
            var matrix = new CustomMatrix.Matrix(model.Data);
            model.Determinant = _matrix.GetDeterminant(matrix);

            return View("SetUpMatrix", model);
        }
    }
}
