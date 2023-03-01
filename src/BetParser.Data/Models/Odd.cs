using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BetParser.Data.Models;

public class Odd
{
    public int Id { get; set; }

    public int MatchId { get; set; }

    public OddType Type { get; set; }

    public float Value { get; set; }

    public virtual Match Match { get; set; } = null!;

    public virtual OddResult? Result { get; set; }
}

internal class OddConfiguration : IEntityTypeConfiguration<Odd>
{
    public void Configure(EntityTypeBuilder<Odd> builder)
    {
        builder.ToTable("Odds");

        builder.HasIndex(e => new { e.MatchId, e.Type }).IsUnique();
    }
}
