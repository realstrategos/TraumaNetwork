﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TraumaNetwork.Models
{
    public class TraumaContext : DbContext
    {
        public TraumaContext(DbContextOptions<TraumaContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgencyCategory>()
                .HasKey(x => new { x.AgencyID, x.CategoryID });
            modelBuilder.Entity<AgencyAgeGroup>()
                .HasKey(x => new { x.AgencyID, x.AgeGroupID });
            modelBuilder.Entity<AgencyService>()
                .HasKey(x => new { x.AgencyID, x.ServiceID });
            modelBuilder.Entity<AgencySpecialty>()
                .HasKey(x => new { x.AgencyID, x.SpecialtyID });
            modelBuilder.Entity<AgencyFinancialPlan>()
                .HasKey(x => new { x.AgencyID, x.FinancialID });

        }

        public DbSet<Agency> Agencies { get; set; }
    }

    public class Agency
    {
        [Key]
        public Guid ID { get; set; }
        public List<AgencyCategory> Categories { get; set; }
        public List<AgencyLocation> Locations { get; set; }
        public List<AgencyAgeGroup> AgeGroups { get; set; }
        public List<AgencyService> Services { get; set; }
        public List<AgencySpecialty> Specialties { get; set; }
        public List<AgencyFinancialPlan> FinancialPlans { get; set; }

        // Contact Info
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
    }

    public interface IFilterable
    {
        Guid ID { get; }
        string Name { get; }
        int Order { get; }
    }

    public class Category : IFilterable
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
    public class AgeGroup : IFilterable
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
    public class Service : IFilterable
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
    public class Specialty : IFilterable
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
    public class FinancialPlan : IFilterable
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }

    public class AgencySpecialty
    {
        [Key, Column(Order = 0)]
        public Guid AgencyID { get; set; }
        [Key, Column(Order = 1)]
        public Guid SpecialtyID { get; set; }

        public Agency Agency { get; set; }
        public Specialty Specialty { get; set; }
    }
    public class AgencyService
    {
        [Key, Column(Order = 0)]
        public Guid AgencyID { get; set; }
        [Key, Column(Order = 1)]
        public Guid ServiceID { get; set; }

        public string TypicalResponseTime { get; set; }
        public Agency Agency { get; set; }
        public Service Service { get; set; }
    }
    public class AgencyAgeGroup
    {
        [Key, Column(Order = 0)]
        public Guid AgencyID { get; set; }
        [Key, Column(Order = 1)]
        public Guid AgeGroupID { get; set; }

        public Agency Agency { get; set; }
        public AgeGroup AgeGroup { get; set; }
    }
    public class AgencyCategory
    {
        [Key, Column(Order = 0)]
        public Guid AgencyID { get; set; }
        [Key, Column(Order = 1)]
        public Guid CategoryID { get; set; }

        public Agency Agency { get; set; }
        public Category Category { get; set; }
    }

    public class AgencyFinancialPlan
    {
        [Key, Column(Order = 0)]
        public Guid AgencyID { get; set; }
        [Key, Column(Order = 1)]
        public Guid FinancialID { get; set; }

        public Agency Agency { get; set; }
        public FinancialPlan Financial { get; set; }
    }
    public class AgencyLocation
    {
        [Key]
        public Guid ID { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public Guid AgencyID { get; set; }
        public Agency Agency { get; set; }
    }




}
