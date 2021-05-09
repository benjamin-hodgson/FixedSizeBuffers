using System;
using System.Runtime.InteropServices;

namespace FixedSizeBuffers
{
    /// <summary>Methods for running a function in the context of a stack-allocated buffer</summary>
    /// <typeparam name="T">The type of elements of the fixed-size buffer</typeparam>
    public static class WithFixedSizeBuffer<T>
    {
        /// <summary>
        /// Stack-allocate a buffer with <paramref name="size"/> elements,
        /// and pass it to <paramref name="action"/>.
        /// 
        /// This method is <strong>unsafe</strong>.
        /// You must be careful not to use the <see cref="Span{T}"/>
        /// after <paramref name="action"/> has returned.
        /// </summary>
        /// <param name="size">The size of the buffer</param>
        /// <param name="action">The action to run</param>
        public static void Do(int size, SpanAction<T> action)
        {
            if (size < 0 || size > 8192)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            switch (LeadingZeroCount.CountLeadingZeros(size - 1))
            {
                case 0:  // size == 0
                {
                    action(new Span<T>(null));
                    return;
                }
                case 32:  // size == 1
                {
                    static void Go(SpanAction<T> a)
                    {
                        T t = default;
                        a(MemoryMarshal.CreateSpan(ref t, 1));
                    }
                    Go(action);
                    return;
                }
                case 31:  // 1 < size <= 2
                {
                    static void Go(int size, SpanAction<T> a)
                    {
                        var buf = new FixedSizeBuffer2<T>();
                        a(buf.AsSpan().Slice(0, size));
                    }
                    Go(size, action);
                    return;
                }
                case 30:  // 2 < size <= 4
                {
                    static void Go(int size, SpanAction<T> a)
                    {
                        var buf = new FixedSizeBuffer4<T>();
                        a(buf.AsSpan().Slice(0, size));
                    }
                    Go(size, action);
                    return;
                }
                case 29:  // 4 < size <= 8
                {
                    static void Go(int size, SpanAction<T> a)
                    {
                        var buf = new FixedSizeBuffer8<T>();
                        a(buf.AsSpan().Slice(0, size));
                    }
                    Go(size, action);
                    return;
                }
                case 28:  // 8 < size <= 16
                {
                    static void Go(int size, SpanAction<T> a)
                    {
                        var buf = new FixedSizeBuffer16<T>();
                        a(buf.AsSpan().Slice(0, size));
                    }
                    Go(size, action);
                    return;
                }
                case 27:  // 16 < size <= 32
                {
                    static void Go(int size, SpanAction<T> a)
                    {
                        var buf = new FixedSizeBuffer32<T>();
                        a(buf.AsSpan().Slice(0, size));
                    }
                    Go(size, action);
                    return;
                }
                case 26:  // 32 < size <= 64
                {
                    static void Go(int size, SpanAction<T> a)
                    {
                        var buf = new FixedSizeBuffer64<T>();
                        a(buf.AsSpan().Slice(0, size));
                    }
                    Go(size, action);
                    return;
                }
                case 25:  // 64 < size <= 128
                {
                    static void Go(int size, SpanAction<T> a)
                    {
                        var buf = new FixedSizeBuffer128<T>();
                        a(buf.AsSpan().Slice(0, size));
                    }
                    Go(size, action);
                    return;
                }
                case 24:  // 128 < size <= 256
                {
                    static void Go(int size, SpanAction<T> a)
                    {
                        var buf = new FixedSizeBuffer256<T>();
                        a(buf.AsSpan().Slice(0, size));
                    }
                    Go(size, action);
                    return;
                }
                case 23:  // 256 < size <= 512
                {
                    static void Go(int size, SpanAction<T> a)
                    {
                        var buf = new FixedSizeBuffer512<T>();
                        a(buf.AsSpan().Slice(0, size));
                    }
                    Go(size, action);
                    return;
                }
                case 22:  // 512 < size <= 1024
                {
                    static void Go(int size, SpanAction<T> a)
                    {
                        var buf = new FixedSizeBuffer1024<T>();
                        a(buf.AsSpan().Slice(0, size));
                    }
                    Go(size, action);
                    return;
                }
                case 21:  // 1024 < size <= 2048
                {
                    static void Go(int size, SpanAction<T> a)
                    {
                        var buf = new FixedSizeBuffer2048<T>();
                        a(buf.AsSpan().Slice(0, size));
                    }
                    Go(size, action);
                    return;
                }
                case 20:  // 2048 < size <= 4096
                {
                    static void Go(int size, SpanAction<T> a)
                    {
                        var buf = new FixedSizeBuffer4096<T>();
                        a(buf.AsSpan().Slice(0, size));
                    }
                    Go(size, action);
                    return;
                }
                case 19:  // 4096 < size <= 8192
                {
                    static void Go(int size, SpanAction<T> a)
                    {
                        var buf = new FixedSizeBuffer8192<T>();
                        a(buf.AsSpan().Slice(0, size));
                    }
                    Go(size, action);
                    return;
                }
            }
        }

        /// <summary>
        /// Stack-allocate a buffer with <paramref name="size"/> elements,
        /// and pass it to <paramref name="action"/>.
        /// 
        /// This method is <strong>unsafe</strong>.
        /// You must be careful not to use the <see cref="Span{T}"/>
        /// after <paramref name="action"/> has returned.
        /// </summary>
        /// <param name="size">The size of the buffer</param>
        /// <param name="arg">A state object to pass to <paramref name="action"/></param>
        /// <param name="action">The action to run</param>
        public static void Do<U>(int size, U arg, SpanAction<T, U> action)
        {
            if (size < 0 || size > 8192)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            switch (LeadingZeroCount.CountLeadingZeros(size - 1))
            {
                case 0:  // size == 0
                {
                    action(new Span<T>(null), arg);
                    return;
                }
                case 32:  // size == 1
                {
                    static void Go(SpanAction<T, U> a, U z)
                    {
                        T t = default;
                        a(MemoryMarshal.CreateSpan(ref t, 1), z);
                    }
                    Go(action, arg);
                    return;
                }
                case 31:  // 1 < size <= 2
                {
                    static void Go(int size, SpanAction<T, U> a, U z)
                    {
                        var buf = new FixedSizeBuffer2<T>();
                        a(buf.AsSpan().Slice(0, size), z);
                    }
                    Go(size, action, arg);
                    return;
                }
                case 30:  // 2 < size <= 4
                {
                    static void Go(int size, SpanAction<T, U> a, U z)
                    {
                        var buf = new FixedSizeBuffer4<T>();
                        a(buf.AsSpan().Slice(0, size), z);
                    }
                    Go(size, action, arg);
                    return;
                }
                case 29:  // 4 < size <= 8
                {
                    static void Go(int size, SpanAction<T, U> a, U z)
                    {
                        var buf = new FixedSizeBuffer8<T>();
                        a(buf.AsSpan().Slice(0, size), z);
                    }
                    Go(size, action, arg);
                    return;
                }
                case 28:  // 8 < size <= 16
                {
                    static void Go(int size, SpanAction<T, U> a, U z)
                    {
                        var buf = new FixedSizeBuffer16<T>();
                        a(buf.AsSpan().Slice(0, size), z);
                    }
                    Go(size, action, arg);
                    return;
                }
                case 27:  // 16 < size <= 32
                {
                    static void Go(int size, SpanAction<T, U> a, U z)
                    {
                        var buf = new FixedSizeBuffer32<T>();
                        a(buf.AsSpan().Slice(0, size), z);
                    }
                    Go(size, action, arg);
                    return;
                }
                case 26:  // 32 < size <= 64
                {
                    static void Go(int size, SpanAction<T, U> a, U z)
                    {
                        var buf = new FixedSizeBuffer64<T>();
                        a(buf.AsSpan().Slice(0, size), z);
                    }
                    Go(size, action, arg);
                    return;
                }
                case 25:  // 64 < size <= 128
                {
                    static void Go(int size, SpanAction<T, U> a, U z)
                    {
                        var buf = new FixedSizeBuffer128<T>();
                        a(buf.AsSpan().Slice(0, size), z);
                    }
                    Go(size, action, arg);
                    return;
                }
                case 24:  // 128 < size <= 256
                {
                    static void Go(int size, SpanAction<T, U> a, U z)
                    {
                        var buf = new FixedSizeBuffer256<T>();
                        a(buf.AsSpan().Slice(0, size), z);
                    }
                    Go(size, action, arg);
                    return;
                }
                case 23:  // 256 < size <= 512
                {
                    static void Go(int size, SpanAction<T, U> a, U z)
                    {
                        var buf = new FixedSizeBuffer512<T>();
                        a(buf.AsSpan().Slice(0, size), z);
                    }
                    Go(size, action, arg);
                    return;
                }
                case 22:  // 512 < size <= 1024
                {
                    static void Go(int size, SpanAction<T, U> a, U z)
                    {
                        var buf = new FixedSizeBuffer1024<T>();
                        a(buf.AsSpan().Slice(0, size), z);
                    }
                    Go(size, action, arg);
                    return;
                }
                case 21:  // 1024 < size <= 2048
                {
                    static void Go(int size, SpanAction<T, U> a, U z)
                    {
                        var buf = new FixedSizeBuffer2048<T>();
                        a(buf.AsSpan().Slice(0, size), z);
                    }
                    Go(size, action, arg);
                    return;
                }
                case 20:  // 2048 < size <= 4096
                {
                    static void Go(int size, SpanAction<T, U> a, U z)
                    {
                        var buf = new FixedSizeBuffer4096<T>();
                        a(buf.AsSpan().Slice(0, size), z);
                    }
                    Go(size, action, arg);
                    return;
                }
                case 19:  // 4096 < size <= 8192
                {
                    static void Go(int size, SpanAction<T, U> a, U z)
                    {
                        var buf = new FixedSizeBuffer8192<T>();
                        a(buf.AsSpan().Slice(0, size), z);
                    }
                    Go(size, action, arg);
                    return;
                }
            }
        }

        /// <summary>
        /// Stack-allocate a buffer with <paramref name="size"/> elements,
        /// and pass it to <paramref name="func"/>.
        /// 
        /// This method is <strong>unsafe</strong>.
        /// You must be careful not to use the <see cref="Span{T}"/>
        /// after <paramref name="func"/> has returned.
        /// </summary>
        /// <param name="size">The size of the buffer</param>
        /// <param name="func">The action to run</param>
        public static R Do<R>(int size, SpanFunc<T, R> func)
        {
            if (size < 0 || size > 8192)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            switch (LeadingZeroCount.CountLeadingZeros(size - 1))
            {
                case 0:  // size == 0
                {
                    return func(new Span<T>(null));
                }
                case 32:  // size == 1
                {
                    static R Go(SpanFunc<T, R> f)
                    {
                        T t = default;
                        return f(MemoryMarshal.CreateSpan(ref t, 1));
                    }
                    return Go(func);
                }
                case 31:  // 1 < size <= 2
                {
                    static R Go(int size, SpanFunc<T, R> f)
                    {
                        var buf = new FixedSizeBuffer2<T>();
                        return f(buf.AsSpan().Slice(0, size));
                    }
                    return Go(size, func);
                }
                case 30:  // 2 < size <= 4
                {
                    static R Go(int size, SpanFunc<T, R> f)
                    {
                        var buf = new FixedSizeBuffer4<T>();
                        return f(buf.AsSpan().Slice(0, size));
                    }
                    return Go(size, func);
                }
                case 29:  // 4 < size <= 8
                {
                    static R Go(int size, SpanFunc<T, R> f)
                    {
                        var buf = new FixedSizeBuffer8<T>();
                        return f(buf.AsSpan().Slice(0, size));
                    }
                    return Go(size, func);
                }
                case 28:  // 8 < size <= 16
                {
                    static R Go(int size, SpanFunc<T, R> f)
                    {
                        var buf = new FixedSizeBuffer16<T>();
                        return f(buf.AsSpan().Slice(0, size));
                    }
                    return Go(size, func);
                }
                case 27:  // 16 < size <= 32
                {
                    static R Go(int size, SpanFunc<T, R> f)
                    {
                        var buf = new FixedSizeBuffer32<T>();
                        return f(buf.AsSpan().Slice(0, size));
                    }
                    return Go(size, func);
                }
                case 26:  // 32 < size <= 64
                {
                    static R Go(int size, SpanFunc<T, R> f)
                    {
                        var buf = new FixedSizeBuffer64<T>();
                        return f(buf.AsSpan().Slice(0, size));
                    }
                    return Go(size, func);
                }
                case 25:  // 64 < size <= 128
                {
                    static R Go(int size, SpanFunc<T, R> f)
                    {
                        var buf = new FixedSizeBuffer128<T>();
                        return f(buf.AsSpan().Slice(0, size));
                    }
                    return Go(size, func);
                }
                case 24:  // 128 < size <= 256
                {
                    static R Go(int size, SpanFunc<T, R> f)
                    {
                        var buf = new FixedSizeBuffer256<T>();
                        return f(buf.AsSpan().Slice(0, size));
                    }
                    return Go(size, func);
                }
                case 23:  // 256 < size <= 512
                {
                    static R Go(int size, SpanFunc<T, R> f)
                    {
                        var buf = new FixedSizeBuffer512<T>();
                        return f(buf.AsSpan().Slice(0, size));
                    }
                    return Go(size, func);
                }
                case 22:  // 512 < size <= 1024
                {
                    static R Go(int size, SpanFunc<T, R> f)
                    {
                        var buf = new FixedSizeBuffer1024<T>();
                        return f(buf.AsSpan().Slice(0, size));
                    }
                    return Go(size, func);
                }
                case 21:  // 1024 < size <= 2048
                {
                    static R Go(int size, SpanFunc<T, R> f)
                    {
                        var buf = new FixedSizeBuffer2048<T>();
                        return f(buf.AsSpan().Slice(0, size));
                    }
                    return Go(size, func);
                }
                case 20:  // 2048 < size <= 4096
                {
                    static R Go(int size, SpanFunc<T, R> f)
                    {
                        var buf = new FixedSizeBuffer4096<T>();
                        return f(buf.AsSpan().Slice(0, size));
                    }
                    return Go(size, func);
                }
                case 19:  // 4096 < size <= 8192
                {
                    static R Go(int size, SpanFunc<T, R> f)
                    {
                        var buf = new FixedSizeBuffer8192<T>();
                        return f(buf.AsSpan().Slice(0, size));
                    }
                    return Go(size, func);
                }
                default:  // unreachable
                {
                    throw new ArgumentOutOfRangeException(nameof(size));
                }
            }
        }

        /// <summary>
        /// Stack-allocate a buffer with <paramref name="size"/> elements,
        /// and pass it to <paramref name="func"/>.
        /// 
        /// This method is <strong>unsafe</strong>.
        /// You must be careful not to use the <see cref="Span{T}"/>
        /// after <paramref name="func"/> has returned.
        /// </summary>
        /// <param name="size">The size of the buffer</param>
        /// <param name="arg">A state object to pass to <paramref name="func"/></param>
        /// <param name="func">The action to run</param>
        public static R Do<U, R>(int size, U arg, SpanFunc<T, U, R> func)
        {
            if (size < 0 || size > 8192)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            switch (LeadingZeroCount.CountLeadingZeros(size - 1))
            {
                case 0:  // size == 0
                {
                    return func(new Span<T>(null), arg);
                }
                case 32:  // size == 1
                {
                    static R Go(SpanFunc<T, U, R> f, U z)
                    {
                        T t = default;
                        return f(MemoryMarshal.CreateSpan(ref t, 1), z);
                    }
                    return Go(func, arg);
                }
                case 31:  // 1 < size <= 2
                {
                    static R Go(int size, SpanFunc<T, U, R> f, U z)
                    {
                        var buf = new FixedSizeBuffer2<T>();
                        return f(buf.AsSpan().Slice(0, size), z);
                    }
                    return Go(size, func, arg);
                }
                case 30:  // 2 < size <= 4
                {
                    static R Go(int size, SpanFunc<T, U, R> f, U z)
                    {
                        var buf = new FixedSizeBuffer4<T>();
                        return f(buf.AsSpan().Slice(0, size), z);
                    }
                    return Go(size, func, arg);
                }
                case 29:  // 4 < size <= 8
                {
                    static R Go(int size, SpanFunc<T, U, R> f, U z)
                    {
                        var buf = new FixedSizeBuffer8<T>();
                        return f(buf.AsSpan().Slice(0, size), z);
                    }
                    return Go(size, func, arg);
                }
                case 28:  // 8 < size <= 16
                {
                    static R Go(int size, SpanFunc<T, U, R> f, U z)
                    {
                        var buf = new FixedSizeBuffer16<T>();
                        return f(buf.AsSpan().Slice(0, size), z);
                    }
                    return Go(size, func, arg);
                }
                case 27:  // 16 < size <= 32
                {
                    static R Go(int size, SpanFunc<T, U, R> f, U z)
                    {
                        var buf = new FixedSizeBuffer32<T>();
                        return f(buf.AsSpan().Slice(0, size), z);
                    }
                    return Go(size, func, arg);
                }
                case 26:  // 32 < size <= 64
                {
                    static R Go(int size, SpanFunc<T, U, R> f, U z)
                    {
                        var buf = new FixedSizeBuffer64<T>();
                        return f(buf.AsSpan().Slice(0, size), z);
                    }
                    return Go(size, func, arg);
                }
                case 25:  // 64 < size <= 128
                {
                    static R Go(int size, SpanFunc<T, U, R> f, U z)
                    {
                        var buf = new FixedSizeBuffer128<T>();
                        return f(buf.AsSpan().Slice(0, size), z);
                    }
                    return Go(size, func, arg);
                }
                case 24:  // 128 < size <= 256
                {
                    static R Go(int size, SpanFunc<T, U, R> f, U z)
                    {
                        var buf = new FixedSizeBuffer256<T>();
                        return f(buf.AsSpan().Slice(0, size), z);
                    }
                    return Go(size, func, arg);
                }
                case 23:  // 256 < size <= 512
                {
                    static R Go(int size, SpanFunc<T, U, R> f, U z)
                    {
                        var buf = new FixedSizeBuffer512<T>();
                        return f(buf.AsSpan().Slice(0, size), z);
                    }
                    return Go(size, func, arg);
                }
                case 22:  // 512 < size <= 1024
                {
                    static R Go(int size, SpanFunc<T, U, R> f, U z)
                    {
                        var buf = new FixedSizeBuffer1024<T>();
                        return f(buf.AsSpan().Slice(0, size), z);
                    }
                    return Go(size, func, arg);
                }
                case 21:  // 1024 < size <= 2048
                {
                    static R Go(int size, SpanFunc<T, U, R> f, U z)
                    {
                        var buf = new FixedSizeBuffer2048<T>();
                        return f(buf.AsSpan().Slice(0, size), z);
                    }
                    return Go(size, func, arg);
                }
                case 20:  // 2048 < size <= 4096
                {
                    static R Go(int size, SpanFunc<T, U, R> f, U z)
                    {
                        var buf = new FixedSizeBuffer4096<T>();
                        return f(buf.AsSpan().Slice(0, size), z);
                    }
                    return Go(size, func, arg);
                }
                case 19:  // 4096 < size <= 8192
                {
                    static R Go(int size, SpanFunc<T, U, R> f, U z)
                    {
                        var buf = new FixedSizeBuffer8192<T>();
                        return f(buf.AsSpan().Slice(0, size), z);
                    }
                    return Go(size, func, arg);
                }
                default:  // unreachable
                {
                    throw new ArgumentOutOfRangeException(nameof(size));
                }
            }
        }
    }
}
