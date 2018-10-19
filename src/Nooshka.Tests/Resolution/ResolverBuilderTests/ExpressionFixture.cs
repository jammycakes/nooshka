using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;
using Nooshka.Compilation;
using Nooshka.Compilation.Expressions;
using Nooshka.Tests.Model;
using Xunit;


namespace Nooshka.Tests.Resolution.ResolverBuilderTests
{
    public class ExpressionFixture
    {
        ConfigurationOptions _options = new ConfigurationOptions(
            constructorSelector: new DefaultConstructorSelector(),
            compiler: new ExpressionCompiler()
        );

        [Fact]
        public void CanCreateServiceResolutionExpressionFromDefaultConstructor()
        {
            var module = new Module();
            module.Define<IServiceWithDependencies>().As<ServiceWithDependencies>();
            module.Define<IServiceWithoutDependencies>().As<ServiceWithoutDependencies>();
            var configuration = new Configuration(_options, module);
            var definition =
                ((IModule)module).GetRegistrations(typeof(IServiceWithDependencies))
                .Single();

            var builder = _options.Compiler.Build(definition, configuration)
                as ExpressionServiceBuilder;

            Assert.NotNull(builder);
            var expressionBody = builder.Expression.Body;
            Assert.IsType<NewExpression>(expressionBody);
            Assert.Equal(typeof(ServiceWithDependencies), expressionBody.Type);
        }

        [Fact]
        public void CanCreateServiceResolutionExpressionFromConstructorExpression()
        {
            var module = new Module();
            module.Define<IServiceWithDependencies>()
                .As(req => new ServiceWithDependencies(
                    Resolve.From<IServiceWithoutDependencies>(),
                    "Hello world"
                ));
            module.Define<IServiceWithoutDependencies>().As<ServiceWithoutDependencies>();

            var configuration = new Configuration(_options, module);
            var definition =
                ((IModule)module).GetRegistrations(typeof(IServiceWithDependencies))
                .Single();

            var builder = _options.Compiler.Build(definition, configuration)
                as ExpressionServiceBuilder;

            Assert.NotNull(builder);

            var container = configuration.CreateContainer();
            var service = container.GetService<IServiceWithDependencies>();
            Assert.Equal("Hello world", service.Message);
            Assert.IsType<ServiceWithoutDependencies>(service.Dependency);
        }
    }
}
