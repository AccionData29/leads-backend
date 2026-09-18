namespace Leads.Infrastructure.Persistence;

public sealed class LeadsDbContext(DbContextOptions<LeadsDbContext> options) : DbContext(options)
{
    public DbSet<Lead> Leads => Set<Lead>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Motorcycle> Motorcycles => Set<Motorcycle>();
    public DbSet<Advisor> Advisors => Set<Advisor>();
    public DbSet<Conversation> Conversations => Set<Conversation>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<LeadEnrichment> LeadEnrichments => Set<LeadEnrichment>();
    public DbSet<LeadScore> LeadScores => Set<LeadScore>();
    public DbSet<LeadAssignment> LeadAssignments => Set<LeadAssignment>();
    public DbSet<PipelineRun> PipelineRuns => Set<PipelineRun>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("leads");

        modelBuilder.Entity<Lead>(entity =>
        {
            entity.ToTable("leads");
            entity.HasKey(x => x.LeadId);
            entity.Property(x => x.LeadId).ValueGeneratedNever();
            entity.Property(x => x.Canal).HasMaxLength(50).IsRequired();
            entity.Property(x => x.NombreCliente).HasMaxLength(250).IsRequired();
            entity.Property(x => x.EstadoGestion).HasConversion<string>().HasMaxLength(30);
            entity.Property(x => x.ModelMatchConfidence).HasPrecision(5, 4);
            entity.HasIndex(x => x.MotorcycleSku);
            entity.HasIndex(x => new { x.EmpresaId, x.PuntoVentaId });
            entity.HasIndex(x => x.FechaRegistro);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customers");
            entity.HasKey(x => x.CustomerId);
            entity.Property(x => x.Nombre).HasMaxLength(250).IsRequired();
            entity.HasIndex(x => new { x.EmpresaId, x.TelefonoNormalizado });
            entity.HasIndex(x => new { x.EmpresaId, x.EmailNormalizado });
        });

        modelBuilder.Entity<Motorcycle>(entity =>
        {
            entity.ToTable("motorcycles");
            entity.HasKey(x => x.Sku);
            entity.Property(x => x.PrecioLista).HasPrecision(18, 2);
            entity.HasIndex(x => new { x.Marca, x.Linea });
        });

        modelBuilder.Entity<Advisor>(entity =>
        {
            entity.ToTable("advisors");
            entity.HasKey(x => x.AsesorId);
            entity.Property(x => x.Nombre).HasMaxLength(250).IsRequired();
            entity.HasIndex(x => new { x.EmpresaId, x.PuntoVentaId, x.Activo });
        });

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.ToTable("conversations");
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.ConversacionId).IsUnique();
            entity.HasIndex(x => x.LeadId);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("messages");
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.ConversationId);
        });

        modelBuilder.Entity<LeadEnrichment>(entity =>
        {
            entity.ToTable("lead_enrichments");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Confianza).HasPrecision(5, 4);
            entity.HasIndex(x => new { x.LeadId, x.CreatedAt });
        });

        modelBuilder.Entity<LeadScore>(entity =>
        {
            entity.ToTable("lead_scores");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.HistoricalProbability).HasPrecision(5, 4);
            entity.Property(x => x.FinalScore).HasPrecision(5, 4);
            entity.Property(x => x.Priority).HasConversion<string>().HasMaxLength(20);
            entity.HasIndex(x => new { x.LeadId, x.CreatedAt });
        });

        modelBuilder.Entity<LeadAssignment>(entity =>
        {
            entity.ToTable("lead_assignments");
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => new { x.LeadId, x.AssignedAt });
        });

        modelBuilder.Entity<PipelineRun>(entity =>
        {
            entity.ToTable("pipeline_runs");
            entity.HasKey(x => x.RunId);
            entity.Property(x => x.Status).HasMaxLength(30).IsRequired();
            entity.Property(x => x.PipelineVersion).HasMaxLength(100).IsRequired();
            entity.HasIndex(x => x.StartedAt);
        });
    }
}
