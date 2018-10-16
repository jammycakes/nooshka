using System;

namespace Nooshka.Impl
{
    public class RegistrationOptions<TService>
    {
        private readonly Registration _state;

        public RegistrationOptions(Registration state)
        {
            _state = state;
        }

        /// <summary>
        ///  Configures the lifecycle for this registration.
        /// </summary>
        /// <param name="lifecycle"></param>
        /// <returns></returns>
        public RegistrationOptions<TService> Lifecycle(Lifecycle lifecycle)
        {
            _state.Lifecycle = lifecycle;
            return this;
        }

        /// <summary>
        ///  Sets a precondition for this registration.
        /// </summary>
        /// <param name="precondition"></param>
        /// <returns></returns>
        public RegistrationOptions<TService> Precondition(Func<ServiceRequest, bool> precondition)
        {
            _state.Precondition = precondition;
            return this;
        }

        /// <summary>
        ///  Configures this registration as a singleton.
        /// </summary>
        /// <returns></returns>
        public RegistrationOptions<TService> Singleton() => Lifecycle(Nooshka.Lifecycle.Singleton);

        /// <summary>
        ///  Configures this registration as a transient.
        /// </summary>
        /// <returns></returns>
        public RegistrationOptions<TService> Transient() => Lifecycle(Nooshka.Lifecycle.Transient);

        /// <summary>
        ///  Configures this registration as a scoped service.
        /// </summary>
        /// <returns></returns>
        public RegistrationOptions<TService> Scoped() => Lifecycle(Nooshka.Lifecycle.Scoped);
    }
}
