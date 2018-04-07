using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TraumaNetwork.Controllers
{
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        private readonly TraumaContext _context;

        public ApiController(TraumaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("categories")]
        public IEnumerable<Category> GetCategory()
        {
            return _context.Category;
        }
    }
}
