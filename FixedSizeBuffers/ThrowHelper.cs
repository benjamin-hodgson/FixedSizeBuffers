using System;
using System.Runtime.CompilerServices;

namespace FixedSizeBuffers
{
    internal static class ThrowHelper
    {
        internal static void ThrowArgumentOutOfRangeException_Index()
        {
            throw CreateArgumentOutOfRangeException_Index();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static Exception CreateArgumentOutOfRangeException_Index()
        {
            return new ArgumentOutOfRangeException("index");
        }
    }
}
