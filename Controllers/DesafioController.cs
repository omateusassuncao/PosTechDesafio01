using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosTechDesafio01.Data;
using PosTechDesafio01.Model;

namespace PosTechDesafio01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DesafioController : ControllerBase
    {

        private ApplicationContext _context;

        public DesafioController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public string Desafio()
        {
            var query = "select power_name from powers";
            var result = _context.Powers.FromSql($"SELECT power_name from powers").ToList().FirstOrDefault();
            return result.Name;
        }
    }
}