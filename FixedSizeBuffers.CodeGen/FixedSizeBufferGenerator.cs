using System;
using System.Linq;

namespace FixedSizeBuffers.CodeGen;

internal static class FixedSizeBufferGenerator
{
    public static string GenerateFixedSizeBuffers()
        => $@"using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace FixedSizeBuffers
{{{string.Concat(Enumerable.Range(1, 13).Select(n => (int)Math.Pow(2, n)).Select(GenerateFixedStruct))}
}}
";

    private static string GenerateFixedStruct(int length)
        => $@"
    /// <summary>
    /// A fixed size buffer of length {length}.
    /// </summary>
    /// <typeparam name=""T"">The type of the elements in the buffer</typeparam>
    [StructLayout(LayoutKind.Sequential)]
    public struct FixedSizeBuffer{length}<T> : IFixedSizeBuffer<T>
    {{{string.Concat(Enumerable.Range(1, length).Select(GenerateItemField))}

        /// <summary>
        /// Gets or sets the element at offset <paramref name=""index""/>.
        /// </summary>
        /// <param name=""index"">The index</param>
        /// <exception cref=""ArgumentOutOfRangeException"">The index was outside the bounds of the buffer</exception>
        /// <returns>The element at offset <paramref name=""index""/>.</returns>
        public T this[int index]
        {{
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {{
                if (index < 0 || index >= {length})
                {{
                    ThrowHelper.ThrowArgumentOutOfRangeException_Index();
                }}
                return Unsafe.Add(ref Item1, index);
            }}
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {{
                if (index < 0 || index >= {length})
                {{
                    ThrowHelper.ThrowArgumentOutOfRangeException_Index();
                }}
                Unsafe.Add(ref Item1, index) = value;
            }}
        }}

        /// <summary>
        /// Returns a <see cref=""Span{{T}}""/> representing the buffer.
        ///
        /// This method is <strong>unsafe</strong>.
        /// You must ensure the <see cref=""Span{{T}}""/> does not outlive the buffer itself.
        /// </summary>
        /// <returns>A <see cref=""Span{{T}}""/> representing the buffer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Span<T> AsSpan() => MemoryMarshal.CreateSpan(ref Item1, {length});

        /// <summary>
        /// Returns a <see cref=""ReadOnlySpan{{T}}""/> representing the buffer.
        ///
        /// This method is <strong>unsafe</strong>.
        /// You must ensure the <see cref=""ReadOnlySpan{{T}}""/> does not outlive the buffer itself.
        /// </summary>
        /// <returns>A <see cref=""ReadOnlySpan{{T}}""/> representing the buffer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<T> AsReadOnlySpan() => MemoryMarshal.CreateReadOnlySpan(ref Item1, {length});

        /// <summary>
        /// Call this method when you've finished using the buffer.
        ///
        /// Technically this method is a no-op, but calling it ensures that the
        /// buffer is not deallocated before you've finished working with it.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Dispose() {{ }}
    }}
";

    private static string GenerateItemField(int n)
    => $@"
        /// <summary>A slot in the buffer</summary>
        public T Item{n};
";
}
