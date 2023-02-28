using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using FluxoDeCaixa.Data;
using FluxoDeCaixa.Models;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeCaixa.Controllers.Tests
{
    [TestClass()]
    public class UnitTests : Controller
    {
        readonly ApplicationDbContext db = new(new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(nameof(Caixa)).Options);

        private void CreateStatus()
        {
            db.Status.RemoveRange(db.Status);

            db.SaveChanges();

            db.Status.Add(new Status()
            {
                StatusId = 1,
                Nome = "Aberto"
            });

            db.Status.Add(new Status()
            {
                StatusId = 2,
                Nome = "Fechado"
            });

            db.SaveChanges();
        }

        private void CreateCaixa()
        {
            CreateStatus();

            db.Caixa.RemoveRange(db.Caixa);

            db.SaveChanges();

            db.Caixa.Add(new Caixa()
            {
                Id = 1,
                Saldo = 100,
                StatusId = 1
            });

            db.Caixa.Add(new Caixa()
            {
                Id = 2,
                Saldo = 200,
                StatusId = 1
            });

            db.SaveChanges();
        }

        [TestMethod()]
        public void StatusIndexTest()
        {
            CreateStatus();

            var status = new StatusController(db);

            var result = status.Index().Result;

            if (result != null)
            {
                if (((ViewResult)result).Model is List<Status> model)
                {
                    Assert.IsTrue(model.Count == 2, $"Expected: 2 - Actual: {model.Count}");
                }
            }
        }

        [TestMethod]
        public void ApiCaixaGetCaixaTest()
        {
            CreateCaixa();

            var caixa = new API.CaixasController(db);

            var result = caixa.GetCaixa().Result;

            if (result != null)
            {
                if (result.Value != null)
                    Assert.IsTrue(result.Value.Count() == 2, $"Expected: 2 - Actual: {result.Value.Count()}");
            }
        }
    }
}