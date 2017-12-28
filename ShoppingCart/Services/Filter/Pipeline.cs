using System.Collections.Generic;

namespace ShoppingCart.Services.Filter
{
    public abstract class Pipeline<T>
    {
        protected readonly List<IFilter<T>> Filters = new List<IFilter<T>>();

        public Pipeline<T> With(IFilter<T> filter)
        {
            Filters.Add(filter);
            return this;
        }

        public abstract T Process(T input);
    }
}