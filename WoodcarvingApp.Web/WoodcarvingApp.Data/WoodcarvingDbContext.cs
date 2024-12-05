﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WoodcarvingApp.Data.Models;

namespace WoodcarvingApp.Web.Data
{
    public class WoodcarvingDbContext : IdentityDbContext
    {
        public WoodcarvingDbContext(DbContextOptions<WoodcarvingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Woodcarving> Woodcarvings { get; set; } = null!;
        public virtual DbSet<Woodcarver> Woodcarvers { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<WoodType> WoodTypes { get; set; } = null!;
        public virtual DbSet<Exhibition> Exhibitions { get; set; } = null!;

    }
}
