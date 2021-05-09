using System;
using System.Linq;

namespace FixedSizeBuffers.CodeGen
{
    static class FixedSizeBufferExtensionsGenerator
    {
        public static string GenerateFixedSizeBufferExtensions()
            => $@"using System.Runtime.CompilerServices;

namespace FixedSizeBuffers
{{
    /// <summary>Extension methods for fixed size buffers.</summary>
    public static class FixedSizeBufferExtensions
    {{{string.Concat(Enumerable.Range(1, 13).Select(n => (int)Math.Pow(2, (double)n)).Select(GenerateItemRefMethod))}
    }}
}}
";

        private static string GenerateItemRefMethod(int length)
            => $@"
        /// <summary>Returns a reference to the item at offset <paramref name=""index""/>.</summary>
        /// <typeparam name=""T"">The type of the elements in the buffer</typeparam>
        /// <param name=""buffer"">The buffer</param>
        /// <param name=""index"">The index</param>
        /// <returns>A reference to the item at offset <paramref name=""index""/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T ItemRef<T>(this ref FixedSizeBuffer{length}<T> buffer, int index)
        {{
            if (index < 0 || index >= {length})
            {{
                ThrowHelper.ThrowArgumentOutOfRangeException_Index();
            }}
            return ref Unsafe.Add(ref buffer.Item1, index);
        }}
";
    }
}
