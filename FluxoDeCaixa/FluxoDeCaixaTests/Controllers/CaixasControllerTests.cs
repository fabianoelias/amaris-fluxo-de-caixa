using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using FluxoDeCaixa.Data;
using FluxoDeCaixa.Models;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeCaixa.Controllers.Tests
{
    [TestClass()]
    public class CaixasControllerTests : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(nameof(Caixa)).Options);

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
                List<Status>? model = ((ViewResult)result).Model as List<Status>;

                if (model != null)
                {
                    Assert.IsTrue(model.Count == 2, $"Expected: 2 - Actual: {model.Count}");
                }
            }
        }
    }
}