using System;

namespace FixedSizeBuffers
{
    /// <summary>A function which receives a <see cref="Span{T}"/></summary>
    /// <typeparam name="T">The type of objects in the span</typeparam>
    /// <param name="span">The span</param>
    public delegate void SpanAction<T>(Span<T> span);
    /// <summary>A function which receives a <see cref="Span{T}"/> and an additional state object.</summary>
    /// <typeparam name="T">The type of objects in the span</typeparam>
    /// <typeparam name="U">The type of the state object</typeparam>
    /// <param name="span">The span</param>
    /// <param name="arg">The state object</param>
    public delegate void SpanAction<T, U>(Span<T> span, U arg);
}