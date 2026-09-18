using Leads.Domain.Entities; using Microsoft.EntityFrameworkCore;
namespace Leads.Infrastructure.Persistence;
public sealed class LeadsDbContext(DbContextOptions<LeadsDbContext> options) : DbContext(options)
{
 public DbSet<Lead> Leads => Set<Lead>();
 public DbSet<Customer> Customers=>Set<Customer>(); 
 public DbSet<Motorcycle> Motorcycles=>Set<Motorcycle>(); 
 public DbSet<Advisor> Advisors=>Set<Advisor>(); 
 public DbSet<Conversation> Conversations=>Set<Conversation>(); 
 public DbSet<Message> Messages=>Set<Message>(); 
 public DbSet<LeadEnrichment> LeadEnrichments=>Set<LeadEnrichment>(); 
 public DbSet<LeadScore> LeadScores=>Set<LeadScore>(); 
 public DbSet<LeadAssignment> LeadAssignments=>Set<LeadAssignment>(); 
 public DbSet<PipelineRun> PipelineRuns=>Set<PipelineRun>();

 protected override void OnModelCreating(ModelBuilder modelBuilder)
 {
      modelBuilder.HasDefaultSchema("leads");
      modelBuilder.Entity<Lead>(e=>{e.ToTable("leads");
          e.HasKey(x=>x.LeadId);
          e.Property(x=>x.LeadId).ValueGeneratedNever();
          e.Property(x=>x.Canal).HasMaxLength(50).IsRequired();
          e.Property(x=>x.NombreCliente).HasMaxLength(250).IsRequired();
          e.Property(x=>x.EstadoGestion).HasConversion<string>().HasMaxLength(30);
          e.Property(x=>x.ModelMatchConfidence).HasPrecision(5,4);
          e.HasIndex(x=>x.MotorcycleSku);
          e.HasIndex(x=>new{x.EmpresaId,x.PuntoVentaId});
          e.HasIndex(x=>x.FechaRegistro);
      });
      modelBuilder.Entity<Customer>(e=>{e.ToTable("customers");
          e.HasKey(x=>x.CustomerId);
          e.Property(x=>x.Nombre).HasMaxLength(250).IsRequired();
          e.HasIndex(x=>new{x.EmpresaId,x.TelefonoNormalizado})
          ;e.HasIndex(x=>new{x.EmpresaId,x.EmailNormalizado});
      });
      modelBuilder.Entity<Motorcycle>(e=>{e.ToTable("motorcycles");
          e.HasKey(x=>x.Sku);
          e.Property(x=>x.PrecioLista).HasPrecision(18,2);
          e.HasIndex(x=>new{x.Marca,x.Linea});
      });
      modelBuilder.Entity<Advisor>(e=>{e.ToTable("advisors");
          e.HasKey(x=>x.AsesorId);
          e.Property(x=>x.Nombre).HasMaxLength(250).IsRequired();
          e.HasIndex(x=>new{x.EmpresaId,x.PuntoVentaId,x.Activo});
      });
      modelBuilder.Entity<Conversation>(e=>{e.ToTable("conversations");
          e.HasKey(x=>x.Id);
          e.HasIndex(x=>x.ConversacionId).IsUnique();
          e.HasIndex(x=>x.LeadId);
      });
      modelBuilder.Entity<Message>(e=>{e.ToTable("messages");
          e.HasKey(x=>x.Id);
          e.HasIndex(x=>x.ConversationId);
      });
      modelBuilder.Entity<LeadEnrichment>(e=>{e.ToTable("lead_enrichments");
          e.HasKey(x=>x.Id);
          e.Property(x=>x.Confianza).HasPrecision(5,4);
          e.HasIndex(x=>new{x.LeadId,x.CreatedAt});
      });
      modelBuilder.Entity<LeadScore>(e=>{e.ToTable("lead_scores");
          e.HasKey(x=>x.Id);
          e.Property(x=>x.HistoricalProbability).HasPrecision(5,4);
          e.Property(x=>x.FinalScore).HasPrecision(5,4);
          e.Property(x=>x.Priority).HasConversion<string>().HasMaxLength(20);
          e.HasIndex(x=>new{x.LeadId,x.CreatedAt});
      });
      modelBuilder.Entity<LeadAssignment>(e=>{e.ToTable("lead_assignments");
          e.HasKey(x=>x.Id);e.HasIndex(x=>new{x.LeadId,x.AssignedAt});
      });
      modelBuilder.Entity<PipelineRun>(e=>{e.ToTable("pipeline_runs");
          e.HasKey(x=>x.RunId);
          e.Property(x=>x.Status).HasMaxLength(30).IsRequired();
          e.Property(x=>x.PipelineVersion).HasMaxLength(100).IsRequired();
          e.HasIndex(x=>x.StartedAt);
      });

 }
}
