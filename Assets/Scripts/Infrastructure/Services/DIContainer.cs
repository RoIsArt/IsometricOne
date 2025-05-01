using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity.Android.Gradle.Manifest;

namespace Assets.Scripts.Infrastructure.Services
{
    public class DIContainer
    {
        private readonly Dictionary<Type, Type> _registrations;
        private readonly Dictionary<Type, object> _singletons;
        private readonly HashSet<Type> _resolutionStack;

        public DIContainer()
        {
            _registrations = new Dictionary<Type, Type>();
            _singletons = new Dictionary<Type, object>();
            _resolutionStack = new HashSet<Type>();
        }

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _registrations[typeof(TInterface)] = typeof(TImplementation);
        }

        public void RegisterSingleton<TInterface, TImplementation>() where TImplementation : TInterface
        {
            Type interfaceType = typeof(TInterface);
            Type implementationType = typeof(TImplementation);
            _registrations[interfaceType] = implementationType;
            _singletons[interfaceType] = CreateInstance(implementationType);
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            if (_singletons.TryGetValue(type, out var singleton))
                return singleton;

            if (!_registrations.TryGetValue(type, out var implementationType))
                throw new InvalidOperationException($"Type {type.Name} is not registered");

            return CreateInstance(implementationType);
        }

        private object CreateInstance(Type implementationType)
        {
            if (_resolutionStack.Contains(implementationType))
                throw new InvalidOperationException($"Circular dependency detected for type {implementationType.Name}");

            _resolutionStack.Add(implementationType);

            ConstructorInfo constructor = GetConstructor(implementationType);
            ParameterInfo[] parameters = constructor.GetParameters();

            object[] parameterInstances = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                parameterInstances[i] = Resolve(parameters[i].ParameterType);
            }

            _resolutionStack.Remove(implementationType);

            return constructor.Invoke(parameterInstances);
        }

        private ConstructorInfo GetConstructor(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();

            if (constructors.Length == 0)
                throw new InvalidOperationException($"No public constructor found for {type.Name}");

            Array.Sort(constructors, (a, b) => b.GetParameters().Length.CompareTo(a.GetParameters().Length));

            return constructors[0];
        }
    }
}
