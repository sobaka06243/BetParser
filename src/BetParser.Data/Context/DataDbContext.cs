using BetParser.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BetParser.Data.Context;

public class DataDbContext : DbContext
{
    public DataDbContext(DbContextOptions<DataDbContext> dbContext) : base(dbContext)
    {

    }
    public virtual DbSet<Match> Matches { get; set; } = null!;

    public virtual DbSet<Odd> Odds { get; set; } = null!;

    public virtual DbSet<OddResult> OddResults { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=BetParser;Password=sobaka06243");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
