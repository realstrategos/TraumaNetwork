using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraumaNetwork.Models
{
    public class TraumaContext : DbContext
    {
        public TraumaContext(DbContextOptions<TraumaContext> options)
            : base(options)
        { }

        public DbSet<Agency> Agencies { get; set; }
    }

    public class Agency
    {
        public List<AgencyCategory> Categories { get; set; }
        public List<AgencyLocation> Locations { get; set; }
        public List<AgencyAgeGroup> AgeGroups { get; set; }
        public List<AgencyService> Services { get; set; }
        public List<AgencySpecialty> Specialties { get; set; }


        // Contact Info
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
    }

    public interface IFilterable
    {
        string Name { get; }
        int Order { get; }
    }

    public class Category : IFilterable
    {
        public string Name { get; set; }
        public int Order { get; set; }
    }
    public class AgeGroup : IFilterable
    {
        public string Name { get; set; }
        public int Order { get; set; }
    }
    public class Service : IFilterable
    {
        public string Name { get; set; }
        public int Order { get; set; }
    }
    public class Specialty : IFilterable
    {
        public string Name { get; set; }
        public int Order { get; set; }
    }
    public class Financial : IFilterable
    {
        public string Name { get; set; }
        public int Order { get; set; }
    }

    public class AgencySpecialty
    {
        public Agency Agency { get; set; }
        public Specialty Specialty { get; set; }
    }
    public class AgencyService
    {
        public string TypicalResponseTime { get; set; }
        public Agency Agency { get; set; }
        public Service Service { get; set; }
    }
    public class AgencyAgeGroup
    {
        public Agency Agency { get; set; }
        public AgeGroup AgeGroup { get; set; }
    }
    public class AgencyCategory
    {
        public Agency Agency { get; set; }
        public Category Category { get; set; }
    }
    public class AgencyLocation
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }


    

}
