using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Services
{
    public class DiContainer : IDisposable
    {
        private readonly Dictionary<Type, Func<object>> _globalRegistrations = new();
        private readonly Dictionary<Type, object> _globalSingletons = new();
        
        private readonly Dictionary<Type, Func<object>> _scopedRegistrations = new();
        private Dictionary<Type, object> _scopedInstances;
        
        private readonly HashSet<Type> _resolutionStack = new();
        
        private readonly List<IDisposable> _disposables = new();
        
        private bool _isScoped = false;

        public void RegisterGlobal<TInterface, TImplementation>(Lifecycle lifecycle = Lifecycle.Transient)
            where TImplementation : TInterface
        {
            Func<object> factory = () => CreateInstance(typeof(TImplementation));
            
            if (lifecycle == Lifecycle.Singleton)
                _globalSingletons[typeof(TInterface)] = factory();
            else
                _globalRegistrations[typeof(TInterface)] = factory;
        }
        
        public void RegisterScene<TInterface, TImplementation>(Lifecycle lifecycle = Lifecycle.Transient)
            where TImplementation : TInterface
        {
            if (!_isScoped)
                throw new InvalidOperationException("Scene registration is only allowed within a scope.");

            Func<object> factory = () =>
            {
                var instance = CreateInstance(typeof(TImplementation));
                TrackDisposable(instance);
                return instance;
            };

            if (lifecycle == Lifecycle.Singleton)
                _scopedInstances[typeof(TInterface)] = factory();
            else
                _scopedRegistrations[typeof(TInterface)] = factory;
        }

        public void Register<T>(Func<T> factory, Lifecycle lifecycle = Lifecycle.Transient)
        {
            if (lifecycle == Lifecycle.Singleton)
                _globalSingletons[typeof(T)] = factory();
            else
                _globalRegistrations[typeof(T)] = () => factory();
        }
        
        public void RegisterInstance<TInterface>(TInterface instance)
        {
            _globalSingletons[typeof(TInterface)] = instance;
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
        
        public void StartScope()
        {
            if (_isScoped)
                throw new InvalidOperationException("A scope is already active. Call EndScope() first.");

            _isScoped = true;
            _scopedInstances = new Dictionary<Type, object>();
        }
        
        public void EndScope()
        {
            if (!_isScoped)
                throw new InvalidOperationException("No active scope to end.");
            
            foreach (var instance in _scopedInstances.Values.OfType<IDisposable>())
                instance.Dispose();

            _scopedInstances.Clear();
            _isScoped = false;
        }
        
        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();

            _disposables.Clear();
            _globalRegistrations.Clear();
            _globalSingletons.Clear();
        }
        
        private void TrackDisposable(object instance)
        {
            if (instance is IDisposable disposable)
                _disposables.Add(disposable);
        }

        private object Resolve(Type type)
        {
            if (_isScoped && _scopedInstances.TryGetValue(type, out var scopedInstance))
                return scopedInstance;

            if (_isScoped && _scopedRegistrations.TryGetValue(type, out var scopedFactory))
                return scopedFactory();
            
            if (_globalSingletons.TryGetValue(type, out var globalInstance))
                return globalInstance;

            if (_globalRegistrations.TryGetValue(type, out var globalFactory))
                return globalFactory();

            throw new InvalidOperationException($"Type {type.Name} is not registered.");
        }

        private object CreateInstance(Type type)
        {
            if (!_resolutionStack.Add(type))
                throw new InvalidOperationException($"Circular dependency for {type.Name}");

            try
            {
                var constructor = GetConstructor(type);
                var args = constructor.GetParameters()
                    .Select(p => Resolve(p.ParameterType))
                    .ToArray();

                return constructor.Invoke(args);
            }
            finally
            {
                _resolutionStack.Remove(type);
            }
        }

        private ConstructorInfo GetConstructor(Type type)
        {
            var constructors = type.GetConstructors();
            
            var injectConstructors = constructors
                .Where(c => c.GetCustomAttribute<InjectAttribute>() != null)
                .ToList();

            if (injectConstructors.Count > 1)
                throw new InvalidOperationException(
                    $"Multiple [Inject] constructors found for {type.Name}. " +
                    "Only one constructor can be marked with [Inject]."
                );

            if (injectConstructors.Count == 1)
                return injectConstructors[0];
            
            var parameterlessConstructor = constructors.FirstOrDefault(c => c.GetParameters().Length == 0);
            if (parameterlessConstructor != null)
                return parameterlessConstructor;
            
            throw new InvalidOperationException(
                $"No suitable constructor found for {type.Name}. " +
                "Either add [Inject] to a constructor or provide a parameterless constructor."
            );
        }

    }
}