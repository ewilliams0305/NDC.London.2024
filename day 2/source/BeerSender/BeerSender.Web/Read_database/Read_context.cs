using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeerSender.Web.Read_database;

public class Read_context : DbContext
{
    public DbSet<Bottles_model> Bottles => Set<Bottles_model>();
    
    public DbSet<Box_status> Box_statuses => Set<Box_status>();
    
    public DbSet<Projection_checkpoint> Projection_checkpoints => Set<Projection_checkpoint>();


    public Read_context(DbContextOptions<Read_context> options)
        : base(options)
    {
            
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Read_context_mapping());
    }
}

public class Projection_checkpoint
{
    [Key]
    [MaxLength(256)]
    public string Projection_name { get; set; }

    [Column(TypeName = "binary(8)")]
    public ulong Event_version { get; set; }
}

public class Box_status
{
    [Key]
    public Guid Aggregate_id { get; set; }
    public int Number_of_bottles { get; set; }

    public Shipment_status Shipment_status { get; set; }
}

public enum Shipment_status
{
    Open, 
    Closed,
    Shipped
}

public class Bottles_model
{
    public Guid Bottle_id { get; set; }
    public int Bottles_sold { get; set; }
    public string Name { get; set; }
    public string Brewery { get; set; }
    public decimal Alcohol_percentage { get; set; }
    public int Volume_in_ml { get; set; }
}

internal class Read_context_mapping : IEntityTypeConfiguration<Bottles_model>
{
    public void Configure(EntityTypeBuilder<Bottles_model> builder)
    {
        builder.HasKey(e => e.Bottle_id);


        builder.Property(e => e.Name)
            .HasMaxLength(256)
            .HasColumnType("varchar");

        builder.Property(e => e.Brewery)
            .HasMaxLength(2048)
            .HasColumnType("varchar");
    }
}