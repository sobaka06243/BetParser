using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BetParser.Data.Models;

public class Match
{
    public int Id { get; set; }

    public string Team1 { get; set; } = null!;

    public string Team2 { get; set; } = null!;

    public DateTime MatchTime { get; set; }

    public virtual IEnumerable<Odd> Odds { get; set; } = null!;
}
internal class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.ToTable("Matches");

        builder.HasKey(e => new { e.Id });

        builder.HasIndex(e => new { e.Team1, e.Team2, e.MatchTime }).IsUnique(true);
    }
}