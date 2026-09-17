using FluentAssertions; using Leads.Domain.Entities;
namespace Leads.UnitTests;
public class LeadTests { [Fact] public void Lead_should_keep_business_identity(){var lead=new Lead(10,DateTime.UtcNow,"whatsapp",1,2,"Cliente",Leads.Domain.Enums.LeadStatus.New);lead.LeadId.Should().Be(10);lead.EmpresaId.Should().Be(1);lead.PuntoVentaId.Should().Be(2);lead.Canal.Should().Be("whatsapp");} }
