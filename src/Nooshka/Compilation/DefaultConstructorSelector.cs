using System.Linq;
using System.Reflection;
using Nooshka.Util;

namespace Nooshka.Compilation
{
    public class DefaultConstructorSelector : IConstructorSelector
    {
        public ConstructorInfo SelectConstructor(ServiceDefinition serviceDefinition, Configuration configuration)
        {
            var constructors = serviceDefinition.ImplementationType.GetConstructors();
            var matchingConstructors =
                from constructor in constructors
                let parameters = constructor.GetParameters()
                let info = new { constructor, parameters, parameters.Length }
                where parameters.All(p => configuration.CanResolve(p.ParameterType.GetServiceType()))
                orderby info.Length descending
                select info.constructor;

            return matchingConstructors.First();
        }
    }
}
