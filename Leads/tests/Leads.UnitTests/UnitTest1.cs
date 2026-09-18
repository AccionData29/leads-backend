using FluentAssertions;
using Xunit;

namespace Leads.UnitTests;

public class LegacyCompatibilityTests
{
    [Fact]
    public void PlaceholderTestShouldBeConsistentWithXunit()
    {
        true.Should().BeTrue();
    }
}