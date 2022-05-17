using System;
using System.Linq;

namespace FixedSizeBuffers.CodeGen;

internal static class WithFixedSizeBufferGenerator
{
    public static string GenerateWithFixedSizeBuffer()
        => @$"using System;
using System.Runtime.InteropServices;

namespace FixedSizeBuffers
{{
    /// <summary>Methods for running a function in the context of a stack-allocated buffer</summary>
    /// <typeparam name=""T"">The type of elements of the fixed-size buffer</typeparam>
    public static class WithFixedSizeBuffer<T>
    {{
        /// <summary>
        /// Stack-allocate a buffer with <paramref name=""size""/> elements,
        /// and pass it to <paramref name=""action""/>.
        /// 
        /// This method is <strong>unsafe</strong>.
        /// You must be careful not to use the <see cref=""Span{{T}}""/>
        /// after <paramref name=""action""/> has returned.
        /// </summary>
        /// <param name=""size"">The size of the buffer</param>
        /// <param name=""action"">The action to run</param>
        public static void Do(int size, SpanAction<T> action)
        {{
            if (size < 0 || size > 8192)
            {{
                throw new ArgumentOutOfRangeException(nameof(size));
            }}
            if (action == null)
            {{
                throw new ArgumentNullException(nameof(action));
            }}
            switch (LeadingZeroCount.CountLeadingZeros(size - 1))
            {{
                case 0:  // size == 0
                {{
                    action(new Span<T>(null));
                    return;
                }}
                case 32:  // size == 1
                {{
                    static void Go(SpanAction<T> a)
                    {{
                        T t = default;
                        a(MemoryMarshal.CreateSpan(ref t, 1));
                    }}
                    Go(action);
                    return;
                }}{string.Concat(Enumerable.Range(1, 13).Select(GenerateSpanAction1Case))}
            }}
        }}

        /// <summary>
        /// Stack-allocate a buffer with <paramref name=""size""/> elements,
        /// and pass it to <paramref name=""action""/>.
        /// 
        /// This method is <strong>unsafe</strong>.
        /// You must be careful not to use the <see cref=""Span{{T}}""/>
        /// after <paramref name=""action""/> has returned.
        /// </summary>
        /// <param name=""size"">The size of the buffer</param>
        /// <param name=""arg"">A state object to pass to <paramref name=""action""/></param>
        /// <param name=""action"">The action to run</param>
        public static void Do<U>(int size, U arg, SpanAction<T, U> action)
        {{
            if (size < 0 || size > 8192)
            {{
                throw new ArgumentOutOfRangeException(nameof(size));
            }}
            if (action == null)
            {{
                throw new ArgumentNullException(nameof(action));
            }}
            switch (LeadingZeroCount.CountLeadingZeros(size - 1))
            {{
                case 0:  // size == 0
                {{
                    action(new Span<T>(null), arg);
                    return;
                }}
                case 32:  // size == 1
                {{
                    static void Go(SpanAction<T, U> a, U z)
                    {{
                        T t = default;
                        a(MemoryMarshal.CreateSpan(ref t, 1), z);
                    }}
                    Go(action, arg);
                    return;
                }}{string.Concat(Enumerable.Range(1, 13).Select(GenerateSpanAction2Case))}
            }}
        }}

        /// <summary>
        /// Stack-allocate a buffer with <paramref name=""size""/> elements,
        /// and pass it to <paramref name=""func""/>.
        /// 
        /// This method is <strong>unsafe</strong>.
        /// You must be careful not to use the <see cref=""Span{{T}}""/>
        /// after <paramref name=""func""/> has returned.
        /// </summary>
        /// <param name=""size"">The size of the buffer</param>
        /// <param name=""func"">The action to run</param>
        public static R Do<R>(int size, SpanFunc<T, R> func)
        {{
            if (size < 0 || size > 8192)
            {{
                throw new ArgumentOutOfRangeException(nameof(size));
            }}
            if (func == null)
            {{
                throw new ArgumentNullException(nameof(func));
            }}
            switch (LeadingZeroCount.CountLeadingZeros(size - 1))
            {{
                case 0:  // size == 0
                {{
                    return func(new Span<T>(null));
                }}
                case 32:  // size == 1
                {{
                    static R Go(SpanFunc<T, R> f)
                    {{
                        T t = default;
                        return f(MemoryMarshal.CreateSpan(ref t, 1));
                    }}
                    return Go(func);
                }}{string.Concat(Enumerable.Range(1, 13).Select(GenerateSpanFunc1Case))}
                default:  // unreachable
                {{
                    throw new ArgumentOutOfRangeException(nameof(size));
                }}
            }}
        }}

        /// <summary>
        /// Stack-allocate a buffer with <paramref name=""size""/> elements,
        /// and pass it to <paramref name=""func""/>.
        /// 
        /// This method is <strong>unsafe</strong>.
        /// You must be careful not to use the <see cref=""Span{{T}}""/>
        /// after <paramref name=""func""/> has returned.
        /// </summary>
        /// <param name=""size"">The size of the buffer</param>
        /// <param name=""arg"">A state object to pass to <paramref name=""func""/></param>
        /// <param name=""func"">The action to run</param>
        public static R Do<U, R>(int size, U arg, SpanFunc<T, U, R> func)
        {{
            if (size < 0 || size > 8192)
            {{
                throw new ArgumentOutOfRangeException(nameof(size));
            }}
            if (func == null)
            {{
                throw new ArgumentNullException(nameof(func));
            }}
            switch (LeadingZeroCount.CountLeadingZeros(size - 1))
            {{
                case 0:  // size == 0
                {{
                    return func(new Span<T>(null), arg);
                }}
                case 32:  // size == 1
                {{
                    static R Go(SpanFunc<T, U, R> f, U z)
                    {{
                        T t = default;
                        return f(MemoryMarshal.CreateSpan(ref t, 1), z);
                    }}
                    return Go(func, arg);
                }}{string.Concat(Enumerable.Range(1, 13).Select(GenerateSpanFunc2Case))}
                default:  // unreachable
                {{
                    throw new ArgumentOutOfRangeException(nameof(size));
                }}
            }}
        }}
    }}
}}
";

    private static string GenerateSpanAction1Case(int number)
    {
        var lastPow = (int)Math.Pow(2, number - 1);
        var pow = (int)Math.Pow(2, number);
        return @$"
                case {32 - number}:  // {lastPow} < size <= {pow}
                {{
                    static void Go(int size, SpanAction<T> a)
                    {{
                        var buf = new FixedSizeBuffer{pow}<T>();
                        a(buf.AsSpan().Slice(0, size));
                    }}
                    Go(size, action);
                    return;
                }}";
    }
    private static string GenerateSpanAction2Case(int number)
    {
        var lastPow = (int)Math.Pow(2, number - 1);
        var pow = (int)Math.Pow(2, number);
        return @$"
                case {32 - number}:  // {lastPow} < size <= {pow}
                {{
                    static void Go(int size, SpanAction<T, U> a, U z)
                    {{
                        var buf = new FixedSizeBuffer{pow}<T>();
                        a(buf.AsSpan().Slice(0, size), z);
                    }}
                    Go(size, action, arg);
                    return;
                }}";
    }
    private static string GenerateSpanFunc1Case(int number)
    {
        var lastPow = (int)Math.Pow(2, number - 1);
        var pow = (int)Math.Pow(2, number);
        return @$"
                case {32 - number}:  // {lastPow} < size <= {pow}
                {{
                    static R Go(int size, SpanFunc<T, R> f)
                    {{
                        var buf = new FixedSizeBuffer{pow}<T>();
                        return f(buf.AsSpan().Slice(0, size));
                    }}
                    return Go(size, func);
                }}";
    }
    private static string GenerateSpanFunc2Case(int number)
    {
        var lastPow = (int)Math.Pow(2, number - 1);
        var pow = (int)Math.Pow(2, number);
        return @$"
                case {32 - number}:  // {lastPow} < size <= {pow}
                {{
                    static R Go(int size, SpanFunc<T, U, R> f, U z)
                    {{
                        var buf = new FixedSizeBuffer{pow}<T>();
                        return f(buf.AsSpan().Slice(0, size), z);
                    }}
                    return Go(size, func, arg);
                }}";
    }
}
