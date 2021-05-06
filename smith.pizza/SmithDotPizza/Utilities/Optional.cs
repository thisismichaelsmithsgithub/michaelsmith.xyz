using System;
using System.Collections;
using System.Collections.Generic;
#pragma warning disable 693

namespace SmithDotPizza.Utilities
{
    public sealed class Optional<T> : IEnumerable<T>
    {
        public T Value { get; }
        public bool HasValue { get; }

        private Optional()
        {
            HasValue = false;
        }

        private Optional(T value)
        {
            Value = value;
            HasValue = true;
        }

        public static Optional<T> Of<T>(T value) => new(value);

        public static Optional<T> Empty() => new();

        public Optional<TResult> Map<TResult>(Func<T, TResult> f) =>
            HasValue
                ? Optional<TResult>.Of(f(Value))
                : Optional<TResult>.Empty();

        public T OrElse(T defaultValue) => HasValue ? Value : defaultValue;

        public T OrElse(Func<T> defaultValue) => HasValue ? Value : defaultValue();

        public IEnumerator<T> GetEnumerator()
        {
            if (HasValue)
            {
                yield return Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}