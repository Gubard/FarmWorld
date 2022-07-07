using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Attributes;

namespace Services
{
    public class DependencyInjector
    {
        private readonly Dictionary<Type, Lazy<object>> _singleton;
        private readonly Dictionary<Type, Func<object>> _transient;
        private readonly Dictionary<Type, ConstructorInfo> _constructors;
        private readonly Dictionary<ParameterInfo, object> _constructorParametersValue;
        private readonly Dictionary<ConstructorInfo, IEnumerable<ParameterInfo>> _constructorParameters;
        private readonly Dictionary<Type, IEnumerable<PropertyInfo>> _typePublicSetters;
        private readonly bool _autoInject;

        public DependencyInjector(bool autoInject)
        {
            _autoInject = autoInject;
            _singleton = new Dictionary<Type, Lazy<object>>();
            _transient = new Dictionary<Type, Func<object>>();
            _constructors = new Dictionary<Type, ConstructorInfo>();
            _constructorParametersValue = new Dictionary<ParameterInfo, object>();
            _constructorParameters = new Dictionary<ConstructorInfo, IEnumerable<ParameterInfo>>();
            _typePublicSetters = new Dictionary<Type, IEnumerable<PropertyInfo>>();
        }

        private IEnumerable<PropertyInfo> GetTypePublicSetters(Type type)
        {
            if (_typePublicSetters.TryGetValue(type, out var properties))
            {
                return properties;
            }

            properties = type.GetProperties(BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.Public);
            _typePublicSetters.Add(type, properties);

            return properties;
        }

        private IEnumerable<ParameterInfo> GetConstructorParameters(ConstructorInfo constructor)
        {
            if (_constructorParameters.TryGetValue(constructor, out var parameters))
            {
                return parameters;
            }

            parameters = constructor.GetParameters();
            _constructorParameters.Add(constructor, parameters);

            return parameters;
        }

        public DependencyInjector Reserve(Type type, Type parameterType, object value)
        {
            var constructor = GetSingleConstructor(type);
            var parameter = GetConstructorParameters(constructor).Single(x => x.ParameterType == parameterType);
            _constructorParametersValue[parameter] = value;

            return this;
        }

        public DependencyInjector Reserve<TObject>(Type parameterType, object value)
        {
            return Reserve(typeof(TObject), parameterType, value);
        }

        public DependencyInjector Reserve<TObject, TParameter>(TParameter value)
        {
            return Reserve<TObject>(typeof(TParameter), value);
        }

        public DependencyInjector Reserve<TObject>(params object[] values)
        {
            foreach (var value in values)
            {
                Reserve<TObject>(value.GetType(), value);
            }

            return this;
        }

        public DependencyInjector RegisterSingleton(Type type, Type implementation)
        {
            return RegisterSingleton(type, () => Resolve(implementation));
        }

        public DependencyInjector RegisterTransient(Type type, Type implementation)
        {
            return RegisterTransient(type, () => Resolve(implementation));
        }

        public DependencyInjector RegisterSingleton(Type type, object value)
        {
            return RegisterSingleton(type, () => value);
        }

        public DependencyInjector RegisterSingleton(object value)
        {
            return RegisterSingleton(value.GetType(), () => value);
        }

        public DependencyInjector RegisterSingleton(Type type)
        {
            return RegisterSingleton(type, () => Resolve(type));
        }

        public DependencyInjector RegisterTransient(Type type)
        {
            return RegisterTransient(type, () => Resolve(type));
        }

        public DependencyInjector RegisterSingleton<TObject>(Func<object> func)
        {
            return RegisterSingleton(typeof(TObject), func);
        }

        public DependencyInjector RegisterTransient<TObject>(Func<object> func)
        {
            return RegisterTransient(typeof(TObject), func);
        }

        public DependencyInjector RegisterSingleton<TObject, TImplementation>()
        {
            return RegisterSingleton(typeof(TObject), typeof(TImplementation));
        }

        public DependencyInjector RegisterTransient<TObject, TImplementation>()
        {
            return RegisterTransient(typeof(TObject), typeof(TImplementation));
        }

        public DependencyInjector RegisterSingleton<TObject>()
        {
            return RegisterSingleton(typeof(TObject));
        }

        public DependencyInjector RegisterTransient<TObject>()
        {
            return RegisterTransient(typeof(TObject));
        }

        public DependencyInjector RegisterSingleton<TObject>(object value)
        {
            return RegisterSingleton(typeof(TObject), value);
        }

        public DependencyInjector RegisterSingleton(Type type, Func<object> func)
        {
            return RegisterSingleton(type, new Lazy<object>(func));
        }

        public DependencyInjector RegisterTransient(Type type, Func<object> func)
        {
            if (_singleton.ContainsKey(type))
            {
                _singleton.Remove(type);
            }

            _transient[type] = func;

            return this;
        }

        public DependencyInjector RegisterSingleton(Type type, Lazy<object> lazy)
        {
            if (_transient.ContainsKey(type))
            {
                _transient.Remove(type);
            }

            _singleton[type] = lazy;

            return this;
        }

        private object CreateByConstructor(ConstructorInfo constructor)
        {
            var parameters = GetConstructorParameters(constructor);
            var parametersValue = parameters.Select(x => GetParameterValue(x)).ToArray();
            var result = Activator.CreateInstance(constructor.DeclaringType, parametersValue);

            return result;
        }

        private object GetParameterValue(ParameterInfo parameter)
        {
            if (_constructorParametersValue.TryGetValue(parameter, out var value))
            {
                return value;
            }

            return Resolve(parameter.ParameterType);
        }

        public void Inject(object value)
        {
            var properties = GetTypePublicSetters(value.GetType());

            foreach (var property in properties)
            {
                if (_autoInject)
                {
                    if (!property.GetCustomAttributes<NonInjectAttribute>().Any())
                    {
                        property.SetValue(value, Resolve(property.PropertyType));
                    }
                }
                else
                {
                    if (property.GetCustomAttributes<InjectAttribute>().Any())
                    {
                        property.SetValue(value, Resolve(property.PropertyType));
                    }
                }
            }
        }

        public TObject Resolve<TObject>()
        {
            return (TObject)Resolve(typeof(TObject));
        }

        public object Resolve(Type type)
        {
            if (_singleton.TryGetValue(type, out var lazy))
            {
                return lazy.Value;
            }

            if (_transient.TryGetValue(type, out var func))
            {
                return func.Invoke();
            }

            var constructor = GetSingleConstructor(type);

            return CreateByConstructor(constructor);
        }

        private ConstructorInfo GetSingleConstructor(Type type)
        {
            if (_constructors.TryGetValue(type, out var constructor))
            {
                return constructor;
            }

            constructor = type.GetConstructors().Single();
            _constructors.Add(type, constructor);

            return constructor;
        }
    }
}