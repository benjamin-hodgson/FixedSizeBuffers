using System;

namespace FixedSizeBuffers
{
    /// <summary>A function which receives a <see cref="Span{T}"/> and an additional state object.</summary>
    /// <typeparam name="T">The type of objects in the span.</typeparam>
    /// <typeparam name="R">The type of the function's return value.</typeparam>
    /// <param name="span">The span.</param>
    /// <returns>A value of type <typeparamref name="R"/>.</returns>
    public delegate R SpanFunc<T, R>(Span<T> span);

    /// <summary>A function which receives a <see cref="Span{T}"/> and an additional state object.</summary>
    /// <typeparam name="T">The type of objects in the span.</typeparam>
    /// <typeparam name="U">The type of the state object.</typeparam>
    /// <typeparam name="R">The type of the function's return value.</typeparam>
    /// <param name="span">The span.</param>
    /// <param name="arg">The state object.</param>
    /// <returns>A value of type <typeparamref name="R"/>.</returns>
    public delegate R SpanFunc<T, U, R>(Span<T> span, U arg);
}
