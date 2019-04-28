using CustomMatrix;
using MatrixWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatrixWeb.Controllers
{
    public class SleController : Controller
    {
        private ILinearEquation _linearEquation;

        public SleController(ILinearEquation linearEquation)
        {
            _linearEquation = linearEquation;
        }

        // GET: Sle
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SleCalculation(int size = 2)
        {
            var model = new SleView
            {
                Data = new double[size][],
                Facts = new double[size],
                X = new double[size]
            };
            for (int i = 0; i < size; i++)
                model.Data[i] = new double[size];

            return View(model);
        }

        [HttpPost]
        public ActionResult SleCalculation(SleView model)
        {
            var sle = new SLE(model.Data, model.Facts);
            model.X = _linearEquation.CalculateEquation(sle.MatrixArray, sle.values);

            return View(model);
        }
    }
}