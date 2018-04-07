using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TraumaNetwork.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly TraumaContext _context;

        public ApiController(TraumaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("categories")]
        public IEnumerable<Category> Categories()
        {
            return _context.Category;
        }

        [HttpGet]
        [Route("services")]
        public IEnumerable<Service> Services(Guid? categoryID = null)
        {
            var query = _context.Set<Service>().AsQueryable();
            if (categoryID.HasValue)
            {
                query = query.Where(x => x.Agencies.Any(y => y.Agency.Categories.Any(z => z.CategoryID == categoryID)));
            }
            return query.ToList();
        }

        [HttpGet]
        [Route("agegroups")]
        public IEnumerable<AgeGroup> AgeGroup(Guid? categoryID = null)
        {
            var query = _context.Set<AgeGroup>().AsQueryable();
            if (categoryID.HasValue)
            {
                query = query.Where(x => x.Agencies.Any(y => y.Agency.Categories.Any(z => z.CategoryID == categoryID)));
            }
            return query.ToList();
        }

        [HttpGet]
        [Route("specialties")]
        public IEnumerable<Specialty> Specialties(Guid? categoryID = null)
        {
            var query = _context.Set<Specialty>().AsQueryable();
            if (categoryID.HasValue)
            {
                query = query.Where(x => x.Agencies.Any(y => y.Agency.Categories.Any(z => z.CategoryID == categoryID)));
            }
            return query.ToList();
        }

        [HttpGet]
        [Route("financialplans")]
        public IEnumerable<FinancialPlan> FinancialPlans(Guid? categoryID = null)
        {
            var query = _context.Set<FinancialPlan>().AsQueryable();
            if (categoryID.HasValue)
            {
                query = query.Where(x => x.Agencies.Any(y => y.Agency.Categories.Any(z => z.CategoryID == categoryID)));
            }
            return query.ToList();
        }
    }
}
