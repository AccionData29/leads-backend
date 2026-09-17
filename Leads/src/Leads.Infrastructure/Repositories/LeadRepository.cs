using Leads.Application.DTOs; using Leads.Application.Ports; using Leads.Domain.Enums; using Leads.Infrastructure.Persistence; using Microsoft.EntityFrameworkCore;
namespace Leads.Infrastructure.Repositories;
public sealed class LeadRepository(LeadsDbContext db) : ILeadRepository
{
 public async Task<IReadOnlyList<LeadListItem>> ListAsync(long? e,long? p,string? status,int page,int size,CancellationToken ct)
 {
  var q=db.Leads.AsNoTracking().AsQueryable();
  if(e.HasValue) q=q.Where(x=>x.EmpresaId==e);
  if(p.HasValue) q=q.Where(x=>x.PuntoVentaId==p);
  if(!string.IsNullOrWhiteSpace(status) && Enum.TryParse<LeadStatus>(status,true,out var s)) q=q.Where(x=>x.EstadoGestion==s);
  var take=Math.Clamp(size,1,200); var skip=Math.Max(0,page-1)*take;
  var leads=await q.OrderByDescending(x=>x.FechaRegistro).Skip(skip).Take(take).ToListAsync(ct);
  var ids=leads.Select(x=>x.LeadId).ToArray();
  var scores=await db.LeadScores.AsNoTracking().Where(x=>ids.Contains(x.LeadId)).GroupBy(x=>x.LeadId).Select(g=>g.OrderByDescending(x=>x.CreatedAt).First()).ToDictionaryAsync(x=>x.LeadId,ct);
  var assignments=await db.LeadAssignments.AsNoTracking().Where(x=>ids.Contains(x.LeadId)).GroupBy(x=>x.LeadId).Select(g=>g.OrderByDescending(x=>x.AssignedAt).First()).ToDictionaryAsync(x=>x.LeadId,ct);
  return leads.Select(l=>new LeadListItem(l.LeadId,l.FechaRegistro,l.Canal,l.EmpresaId,l.PuntoVentaId,l.NombreCliente,l.Ciudad,l.ModeloInteresTexto,l.EstadoGestion,scores.TryGetValue(l.LeadId,out var sc)?sc.Priority:null,scores.TryGetValue(l.LeadId,out sc)?sc.FinalScore:null,assignments.TryGetValue(l.LeadId,out var a)?a.AsesorId:null)).ToList();
 }
 public async Task<LeadDetail?> GetAsync(long id,CancellationToken ct)
 {
  var lead=await db.Leads.AsNoTracking().SingleOrDefaultAsync(x=>x.LeadId==id,ct); if(lead is null) return null;
  var customer=lead.CustomerId.HasValue?await db.Customers.AsNoTracking().Where(x=>x.CustomerId==lead.CustomerId.Value).Select(x=>new CustomerSummary(x.CustomerId,x.Nombre,x.TelefonoNormalizado,x.EmailNormalizado,x.Ciudad)).SingleOrDefaultAsync(ct):null;
  var enrichment=await db.LeadEnrichments.AsNoTracking().Where(x=>x.LeadId==id).OrderByDescending(x=>x.CreatedAt).Select(x=>new EnrichmentDto(x.LeadId,x.ModeloInteres,x.CuotaInicial,x.FormaPago,x.Intencion,x.Objecion,x.SolicitoCita,x.SolicitoCotizacion,x.Confianza,x.Provider,x.PipelineVersion,x.CreatedAt)).FirstOrDefaultAsync(ct);
  var score=await db.LeadScores.AsNoTracking().Where(x=>x.LeadId==id).OrderByDescending(x=>x.CreatedAt).Select(x=>new ScoreDto(x.LeadId,x.HistoricalProbability,x.FinalScore,x.Priority,x.ReasonsJson,x.ModelVersion,x.CreatedAt)).FirstOrDefaultAsync(ct);
  var assignment=await db.LeadAssignments.AsNoTracking().Where(x=>x.LeadId==id).OrderByDescending(x=>x.AssignedAt).Select(x=>new AssignmentDto(x.LeadId,x.AsesorId,x.AssignedAt,x.Reason)).FirstOrDefaultAsync(ct);
  return new LeadDetail(lead.LeadId,lead.FechaRegistro,lead.Canal,lead.EmpresaId,lead.PuntoVentaId,lead.NombreCliente,lead.Telefono,lead.Email,lead.Ciudad,lead.ModeloInteresTexto,lead.EstadoGestion,lead.FechaPrimerContacto,lead.Campania,lead.MotorcycleSku,lead.ModelMatchConfidence,lead.ModelMatchMethod,customer,enrichment,score,assignment);
 }
 public async Task<ScoreDto?> GetScoreAsync(long id,CancellationToken ct)=>await db.LeadScores.AsNoTracking().Where(x=>x.LeadId==id).OrderByDescending(x=>x.CreatedAt).Select(x=>new ScoreDto(x.LeadId,x.HistoricalProbability,x.FinalScore,x.Priority,x.ReasonsJson,x.ModelVersion,x.CreatedAt)).FirstOrDefaultAsync(ct);
 public async Task<AssignmentDto?> GetAssignmentAsync(long id,CancellationToken ct)=>await db.LeadAssignments.AsNoTracking().Where(x=>x.LeadId==id).OrderByDescending(x=>x.AssignedAt).Select(x=>new AssignmentDto(x.LeadId,x.AsesorId,x.AssignedAt,x.Reason)).FirstOrDefaultAsync(ct);
}
