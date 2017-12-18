using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ShoppingCart.Data.IoC
{
    public class IoC : IIoC
    {
        private static IoC _instance;
        private readonly Dictionary<Type, IAdapter> _adapters;

        public IoC()
        {
            _adapters = new Dictionary<Type, IAdapter>();
        }

        public T For<T>()
        {
            return (T) _adapters[typeof(T)];
        }

        public void Register<T>(IAdapter adapter)
        {
            _adapters.Add(typeof(T), adapter);
        }

        [ExcludeFromCodeCoverage]
        public static IoC Instance()
        {
            if (_instance == null)
                _instance = new IoC();

            return _instance;
        }
    }
}