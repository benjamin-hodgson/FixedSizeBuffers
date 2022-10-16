using System.Runtime.CompilerServices;
#if NETCOREAPP3_1_OR_GREATER
using System.Runtime.Intrinsics.X86;
#endif
#if NET5_0_OR_GREATER
using System.Runtime.Intrinsics.Arm;
#endif

namespace FixedSizeBuffers
{
    internal static class LeadingZeroCount
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CountLeadingZeros(int value)
        {
#if NETCOREAPP3_1_OR_GREATER
            if (Lzcnt.IsSupported)
            {
                return (int)Lzcnt.LeadingZeroCount((uint)value);
            }
#endif
#if NET5_0_OR_GREATER
            if (ArmBase.IsSupported)
            {
                return ArmBase.LeadingZeroCount(value);
            }
#endif
            return CountLeadingZeros_Fallback(value);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int CountLeadingZeros_Fallback(int value)
        {
            var count = 0;
            var mask = 1 << 31;
            while ((value & mask) == 0 && count < 32)
            {
                count++;
                mask >>= 1;
            }

            return count;
        }
    }
}
