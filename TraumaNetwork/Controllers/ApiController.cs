using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        [HttpGet]
        [Route("agencies")]
        public IEnumerable<Agency> Agencies(Guid? categoryID = null, Guid? serviceID = null, Guid? financialplanID = null, Guid? specialtyID = null)
        {
            var query = _context.Agencies.AsQueryable();
            var temp = query.Include(x => x.Categories);
            if (categoryID.HasValue)
            {
                query = query.Where(x => x.Categories.Any(z => z.CategoryID == categoryID));
            }
            if (serviceID.HasValue)
            {
                query = query.Where(x => x.Services.Any(z => z.ServiceID == serviceID));
            }
            if (financialplanID.HasValue)
            {
                query = query.Where(x => x.FinancialPlans.Any(z => z.FinancialID == financialplanID));
            }
            if (specialtyID.HasValue)
            {
                query = query.Where(x => x.Specialties.Any(z => z.SpecialtyID == specialtyID));
            }
            return query.Include(x => x.Locations).ToList();
        }
    }
}
