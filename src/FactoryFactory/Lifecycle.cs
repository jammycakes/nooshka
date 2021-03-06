using FactoryFactory.Lifecycles;
using Microsoft.Extensions.DependencyInjection;

namespace FactoryFactory
{
    public static class Lifecycle
    {
        public static readonly ILifecycle Scoped = new ScopedLifecycle();
        public static readonly ILifecycle Singleton = new SingletonLifecycle();
        public static readonly ILifecycle Transient = new TransientLifecycle();
        public static readonly ILifecycle Untracked = new UntrackedLifecycle();

        public static ILifecycle Default => Transient;

        public static ILifecycle Get(ServiceLifetime lifetime)
        {
            switch (lifetime) {
                case ServiceLifetime.Scoped: return Scoped;
                case ServiceLifetime.Singleton: return Singleton;
                case ServiceLifetime.Transient: return Transient;
                default: return null;
            }
        }
    }
}
