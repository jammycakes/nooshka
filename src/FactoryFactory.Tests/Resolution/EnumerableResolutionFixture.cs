using FactoryFactory.Tests.Model;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FactoryFactory.Tests.Resolution
{
    public class EnumerableResolutionFixture
    {
        [Fact]
        public void UnresolvableDependenciesShouldResolveAsEmptyEnumerable()
        {
            /*
             * ASP.NET Core requires this test to pass (it failed in 0.1.0), but
             * this case is not covered by the standard tests in the Microsoft
             * specification fixture.
             */

            var service = new Registry()
                .Define<ServiceWithEnumerableDependencies>()
                .As<ServiceWithEnumerableDependencies>()
                .CreateContainer()
                .GetService<ServiceWithEnumerableDependencies>();
            Assert.Empty(service.Dependencies);
        }
    }
}
