using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BetParser.Data.Models;

public class OddResult
{
    [Key]
    public int OddId { get; set; }

    public bool Success { get; set; }

    public virtual Odd Odd { get; set; } = null!;
}

internal class OddResultConfiguration : IEntityTypeConfiguration<OddResult>
{
    public void Configure(EntityTypeBuilder<OddResult> builder)
    {
        builder.ToTable("OddResults");

    }
}
