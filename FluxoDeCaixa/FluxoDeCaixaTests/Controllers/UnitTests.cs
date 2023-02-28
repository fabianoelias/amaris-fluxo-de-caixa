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

        [TestMethod()]
        public void StatusIndexTest()
        {
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
        public void MovimentarTest()
        {

        }
    }
}