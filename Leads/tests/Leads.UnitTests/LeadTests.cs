namespace Leads.UnitTests;

public class LeadTests
{
    [Fact]
    public void LeadShouldKeepBusinessIdentity()
    {
        var now = new DateTime(2026, 9, 17, 12, 0, 0, DateTimeKind.Utc);

        var lead = new Lead(10, now, "whatsapp", 1, 2, "Cliente", LeadStatus.New);

        lead.LeadId.Should().Be(10);
        lead.FechaRegistro.Should().Be(now);
        lead.Canal.Should().Be("whatsapp");
        lead.EmpresaId.Should().Be(1);
        lead.PuntoVentaId.Should().Be(2);
        lead.NombreCliente.Should().Be("Cliente");
        lead.EstadoGestion.Should().Be(LeadStatus.New);
    }

    [Fact]
    public void LeadShouldExposeOptionalFieldsAsNullByDefault()
    {
        var lead = new Lead(42, DateTime.UtcNow, "web", 7, 9, "Ana", LeadStatus.Contacted);

        lead.Telefono.Should().BeNull();
        lead.Email.Should().BeNull();
        lead.Ciudad.Should().BeNull();
        lead.ModeloInteresTexto.Should().BeNull();
        lead.FechaPrimerContacto.Should().BeNull();
        lead.Campania.Should().BeNull();
        lead.CustomerId.Should().BeNull();
        lead.TelefonoNormalizado.Should().BeNull();
        lead.EmailNormalizado.Should().BeNull();
        lead.CiudadNormalizada.Should().BeNull();
        lead.CanalNormalizado.Should().BeNull();
        lead.ModeloTextoNormalizado.Should().BeNull();
        lead.MotorcycleSku.Should().BeNull();
        lead.ModelMatchConfidence.Should().BeNull();
        lead.ModelMatchMethod.Should().BeNull();
    }

    [Theory]
    [InlineData(LeadStatus.New)]
    [InlineData(LeadStatus.Contacted)]
    [InlineData(LeadStatus.Qualified)]
    [InlineData(LeadStatus.Appointment)]
    [InlineData(LeadStatus.Won)]
    [InlineData(LeadStatus.Lost)]
    public void LeadStatusShouldContainExpectedValues(LeadStatus status)
    {
        status.Should().BeOneOf(LeadStatus.New, LeadStatus.Contacted, LeadStatus.Qualified,
            LeadStatus.Appointment, LeadStatus.Won, LeadStatus.Lost);
    }

    [Fact]
    public void LeadPriorityShouldContainExpectedValues()
    {
        Enum.GetValues<LeadPriority>()
            .Should().Equal(LeadPriority.Low, LeadPriority.Medium, LeadPriority.High);
    }
}

public class DtoTests
{
    [Fact]
    public void PipelineRunDtoSecondaryConstructorShouldSetZeroCountersAndCarryMetadata()
    {
        var runId = Guid.NewGuid();
        var startedAt = new DateTime(2026, 9, 17, 8, 0, 0, DateTimeKind.Utc);

        var dto = new PipelineRunDto(runId, "Running", startedAt, null, "{\"ok\":true}", "warning");

        dto.RunId.Should().Be(runId);
        dto.Status.Should().Be("Running");
        dto.StartedAt.Should().Be(startedAt);
        dto.FinishedAt.Should().BeNull();
        dto.RecordsRead.Should().Be(0);
        dto.RecordsProcessed.Should().Be(0);
        dto.RecordsFailed.Should().Be(0);
        dto.SummaryJson.Should().Be("{\"ok\":true}");
        dto.Error.Should().Be("warning");
    }

    [Fact]
    public void RecordTypesShouldPreserveValuesAndSupportEquality()
    {
        var customer = new CustomerSummary(Guid.NewGuid(), "Ana", "57300", "ana@mail.com", "Bogotá");
        var enrichment = new EnrichmentDto(12, "XR 190", 500000m, "Credito", "Alta", "Ninguna", true, false, 0.85m, "provider", "v1", DateTime.UtcNow);
        var score = new ScoreDto(12, 0.4m, 91m, LeadPriority.High, "[]", "m-2026", DateTime.UtcNow);
        var assignment = new AssignmentDto(12, 9, DateTime.UtcNow, "Prima");

        var first = new LeadDetail(12, DateTime.UtcNow, "web", 4, 5, "Ana", "57300", "ana@mail.com", "Bogotá", "XR 190", LeadStatus.Qualified, DateTime.UtcNow.AddDays(-1), "campania", "SKU-1", 0.93m, "ml", customer, enrichment, score, assignment);
        var second = new LeadDetail(12, first.FechaRegistro, "web", 4, 5, "Ana", "57300", "ana@mail.com", "Bogotá", "XR 190", LeadStatus.Qualified, first.FechaPrimerContacto, "campania", "SKU-1", 0.93m, "ml", customer, enrichment, score, assignment);

        first.Should().BeEquivalentTo(second);
        first.Should().Be(second);
    }

    [Fact]
    public void DefaultValuesInPipelineStartRequestShouldBeOptional()
    {
        var request = new PipelineStartRequest();

        request.SourceDir.Should().BeNull();
    }
}

public class UseCaseTests
{
    [Fact]
    public async Task LeadQueriesShouldPassThroughRepositoryCalls()
    {
        var expectedList = new List<LeadListItem>
        {
            new(1, DateTime.UtcNow, "web", 4, 5, "Ana", "Bogotá", "XR 190", LeadStatus.Qualified, LeadPriority.High, 90m, 9)
        };
        var expectedDetail = new LeadDetail(1, DateTime.UtcNow, "web", 4, 5, "Ana", "57300", "ana@mail.com", "Bogotá", "XR 190", LeadStatus.Qualified, DateTime.UtcNow.AddDays(-1), "campania", "SKU-1", 0.92m, "ml", null, null, null, null);
        var expectedScore = new ScoreDto(1, 0.5m, 88m, LeadPriority.Medium, "[]", "m-2026", DateTime.UtcNow);
        var expectedAssignment = new AssignmentDto(1, 9, DateTime.UtcNow, "Asignado");

        var repo = new StubLeadRepository(expectedList, expectedDetail, expectedScore, expectedAssignment);
        var queries = new LeadQueries(repo);

        var list = await queries.ListAsync(4, 5, "Qualified", 1, 10, CancellationToken.None);
        var detail = await queries.GetAsync(1, CancellationToken.None);
        var score = await queries.GetScoreAsync(1, CancellationToken.None);
        var assignment = await queries.GetAssignmentAsync(1, CancellationToken.None);

        list.Should().BeSameAs(expectedList);
        detail.Should().BeSameAs(expectedDetail);
        score.Should().BeSameAs(expectedScore);
        assignment.Should().BeSameAs(expectedAssignment);
        repo.Calls.Should().ContainInOrder(
            nameof(ILeadRepository.ListAsync),
            nameof(ILeadRepository.GetAsync),
            nameof(ILeadRepository.GetScoreAsync),
            nameof(ILeadRepository.GetAssignmentAsync));
    }

    [Fact]
    public async Task ReferenceQueriesShouldDelegateToRepositories()
    {
        var advisors = new List<AdvisorDto>
        {
            new(9, "Luis", 5, 4, 20, true)
        };
        var motorcycles = new List<MotorcycleDto>
        {
            new("SKU-1", "Yamaha", "R3", 321, "Deportivo", 25000000m, 2, 1)
        };
        var runs = new List<PipelineRunDto>
        {
            new(Guid.NewGuid(), "Completed", DateTime.UtcNow, DateTime.UtcNow.AddMinutes(5), 100, 99, 1, "{\"ok\":true}", null)
        };

        var advisorRepo = new StubAdvisorRepository(advisors);
        var motorcycleRepo = new StubMotorcycleRepository(motorcycles);
        var runRepo = new StubPipelineRunRepository(runs);
        var queries = new ReferenceQueries(advisorRepo, motorcycleRepo, runRepo);

        (await queries.AdvisorsAsync(4, 5, true, CancellationToken.None)).Should().BeSameAs(advisors);
        (await queries.MotorcyclesAsync("Yamaha", "Deportivo", CancellationToken.None)).Should().BeSameAs(motorcycles);
        (await queries.RunsAsync(1, 10, CancellationToken.None)).Should().BeSameAs(runs);
    }

    [Fact]
    public async Task PipelineCommandsShouldDelegateToService()
    {
        var response = new PipelineStartResponse(Guid.NewGuid(), "Accepted");
        var request = new PipelineStartRequest("/tmp/data");
        var service = new StubPipelineService(response);
        var commands = new PipelineCommands(service);

        var result = await commands.StartAsync(request, CancellationToken.None);

        result.Should().Be(response);
        service.LastRequest.Should().BeSameAs(request);
    }

    private sealed class StubLeadRepository(
        IReadOnlyList<LeadListItem> list,
        LeadDetail? detail,
        ScoreDto? score,
        AssignmentDto? assignment) : ILeadRepository
    {
        public List<string> Calls { get; } = new();

        public Task<IReadOnlyList<LeadListItem>> ListAsync(long? empresaId, long? puntoVentaId, string? status, int page, int pageSize, CancellationToken ct)
        {
            Calls.Add(nameof(ListAsync));
            return Task.FromResult(list);
        }

        public Task<LeadDetail?> GetAsync(long leadId, CancellationToken ct)
        {
            Calls.Add(nameof(GetAsync));
            return Task.FromResult(detail);
        }

        public Task<ScoreDto?> GetScoreAsync(long leadId, CancellationToken ct)
        {
            Calls.Add(nameof(GetScoreAsync));
            return Task.FromResult(score);
        }

        public Task<AssignmentDto?> GetAssignmentAsync(long leadId, CancellationToken ct)
        {
            Calls.Add(nameof(GetAssignmentAsync));
            return Task.FromResult(assignment);
        }
    }

    private sealed class StubAdvisorRepository(IReadOnlyList<AdvisorDto> advisors) : IAdvisorRepository
    {
        public Task<IReadOnlyList<AdvisorDto>> ListAsync(long? empresaId, long? puntoVentaId, bool? activo, CancellationToken ct)
            => Task.FromResult(advisors);
    }

    private sealed class StubMotorcycleRepository(IReadOnlyList<MotorcycleDto> bikes) : IMotorcycleRepository
    {
        public Task<IReadOnlyList<MotorcycleDto>> ListAsync(string? marca, string? segmento, CancellationToken ct)
            => Task.FromResult(bikes);
    }

    private sealed class StubPipelineRunRepository(IReadOnlyList<PipelineRunDto> runs) : IPipelineRunRepository
    {
        public Task<IReadOnlyList<PipelineRunDto>> ListAsync(int page, int size, CancellationToken ct)
            => Task.FromResult(runs);
    }

    private sealed class StubPipelineService(PipelineStartResponse response) : IPipelineService
    {
        public PipelineStartRequest? LastRequest
        {
            get; private set;
        }

        public Task<PipelineStartResponse> StartAsync(PipelineStartRequest request, CancellationToken ct)
        {
            LastRequest = request;
            return Task.FromResult(response);
        }
    }
}
