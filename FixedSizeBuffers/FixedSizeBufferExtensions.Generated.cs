using System.Runtime.CompilerServices;

namespace FixedSizeBuffers
{
    /// <summary>Extension methods for fixed size buffers.</summary>
    public static class FixedSizeBufferExtensions
    {
        /// <summary>Returns a reference to the item at offset <paramref name="index"/>.</summary>
        /// <typeparam name="T">The type of the elements in the buffer</typeparam>
        /// <param name="buffer">The buffer</param>
        /// <param name="index">The index</param>
        /// <returns>A reference to the item at offset <paramref name="index"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T ItemRef<T>(this ref FixedSizeBuffer2<T> buffer, int index)
        {
            if (index < 0 || index >= 2)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException_Index();
            }
            return ref Unsafe.Add(ref buffer.Item1, index);
        }

        /// <summary>Returns a reference to the item at offset <paramref name="index"/>.</summary>
        /// <typeparam name="T">The type of the elements in the buffer</typeparam>
        /// <param name="buffer">The buffer</param>
        /// <param name="index">The index</param>
        /// <returns>A reference to the item at offset <paramref name="index"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T ItemRef<T>(this ref FixedSizeBuffer4<T> buffer, int index)
        {
            if (index < 0 || index >= 4)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException_Index();
            }
            return ref Unsafe.Add(ref buffer.Item1, index);
        }

        /// <summary>Returns a reference to the item at offset <paramref name="index"/>.</summary>
        /// <typeparam name="T">The type of the elements in the buffer</typeparam>
        /// <param name="buffer">The buffer</param>
        /// <param name="index">The index</param>
        /// <returns>A reference to the item at offset <paramref name="index"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T ItemRef<T>(this ref FixedSizeBuffer8<T> buffer, int index)
        {
            if (index < 0 || index >= 8)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException_Index();
            }
            return ref Unsafe.Add(ref buffer.Item1, index);
        }

        /// <summary>Returns a reference to the item at offset <paramref name="index"/>.</summary>
        /// <typeparam name="T">The type of the elements in the buffer</typeparam>
        /// <param name="buffer">The buffer</param>
        /// <param name="index">The index</param>
        /// <returns>A reference to the item at offset <paramref name="index"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T ItemRef<T>(this ref FixedSizeBuffer16<T> buffer, int index)
        {
            if (index < 0 || index >= 16)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException_Index();
            }
            return ref Unsafe.Add(ref buffer.Item1, index);
        }

        /// <summary>Returns a reference to the item at offset <paramref name="index"/>.</summary>
        /// <typeparam name="T">The type of the elements in the buffer</typeparam>
        /// <param name="buffer">The buffer</param>
        /// <param name="index">The index</param>
        /// <returns>A reference to the item at offset <paramref name="index"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T ItemRef<T>(this ref FixedSizeBuffer32<T> buffer, int index)
        {
            if (index < 0 || index >= 32)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException_Index();
            }
            return ref Unsafe.Add(ref buffer.Item1, index);
        }

        /// <summary>Returns a reference to the item at offset <paramref name="index"/>.</summary>
        /// <typeparam name="T">The type of the elements in the buffer</typeparam>
        /// <param name="buffer">The buffer</param>
        /// <param name="index">The index</param>
        /// <returns>A reference to the item at offset <paramref name="index"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T ItemRef<T>(this ref FixedSizeBuffer64<T> buffer, int index)
        {
            if (index < 0 || index >= 64)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException_Index();
            }
            return ref Unsafe.Add(ref buffer.Item1, index);
        }

        /// <summary>Returns a reference to the item at offset <paramref name="index"/>.</summary>
        /// <typeparam name="T">The type of the elements in the buffer</typeparam>
        /// <param name="buffer">The buffer</param>
        /// <param name="index">The index</param>
        /// <returns>A reference to the item at offset <paramref name="index"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T ItemRef<T>(this ref FixedSizeBuffer128<T> buffer, int index)
        {
            if (index < 0 || index >= 128)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException_Index();
            }
            return ref Unsafe.Add(ref buffer.Item1, index);
        }

        /// <summary>Returns a reference to the item at offset <paramref name="index"/>.</summary>
        /// <typeparam name="T">The type of the elements in the buffer</typeparam>
        /// <param name="buffer">The buffer</param>
        /// <param name="index">The index</param>
        /// <returns>A reference to the item at offset <paramref name="index"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T ItemRef<T>(this ref FixedSizeBuffer256<T> buffer, int index)
        {
            if (index < 0 || index >= 256)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException_Index();
            }
            return ref Unsafe.Add(ref buffer.Item1, index);
        }

        /// <summary>Returns a reference to the item at offset <paramref name="index"/>.</summary>
        /// <typeparam name="T">The type of the elements in the buffer</typeparam>
        /// <param name="buffer">The buffer</param>
        /// <param name="index">The index</param>
        /// <returns>A reference to the item at offset <paramref name="index"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T ItemRef<T>(this ref FixedSizeBuffer512<T> buffer, int index)
        {
            if (index < 0 || index >= 512)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException_Index();
            }
            return ref Unsafe.Add(ref buffer.Item1, index);
        }

        /// <summary>Returns a reference to the item at offset <paramref name="index"/>.</summary>
        /// <typeparam name="T">The type of the elements in the buffer</typeparam>
        /// <param name="buffer">The buffer</param>
        /// <param name="index">The index</param>
        /// <returns>A reference to the item at offset <paramref name="index"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T ItemRef<T>(this ref FixedSizeBuffer1024<T> buffer, int index)
        {
            if (index < 0 || index >= 1024)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException_Index();
            }
            return ref Unsafe.Add(ref buffer.Item1, index);
        }

        /// <summary>Returns a reference to the item at offset <paramref name="index"/>.</summary>
        /// <typeparam name="T">The type of the elements in the buffer</typeparam>
        /// <param name="buffer">The buffer</param>
        /// <param name="index">The index</param>
        /// <returns>A reference to the item at offset <paramref name="index"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T ItemRef<T>(this ref FixedSizeBuffer2048<T> buffer, int index)
        {
            if (index < 0 || index >= 2048)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException_Index();
            }
            return ref Unsafe.Add(ref buffer.Item1, index);
        }

        /// <summary>Returns a reference to the item at offset <paramref name="index"/>.</summary>
        /// <typeparam name="T">The type of the elements in the buffer</typeparam>
        /// <param name="buffer">The buffer</param>
        /// <param name="index">The index</param>
        /// <returns>A reference to the item at offset <paramref name="index"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T ItemRef<T>(this ref FixedSizeBuffer4096<T> buffer, int index)
        {
            if (index < 0 || index >= 4096)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException_Index();
            }
            return ref Unsafe.Add(ref buffer.Item1, index);
        }

        /// <summary>Returns a reference to the item at offset <paramref name="index"/>.</summary>
        /// <typeparam name="T">The type of the elements in the buffer</typeparam>
        /// <param name="buffer">The buffer</param>
        /// <param name="index">The index</param>
        /// <returns>A reference to the item at offset <paramref name="index"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T ItemRef<T>(this ref FixedSizeBuffer8192<T> buffer, int index)
        {
            if (index < 0 || index >= 8192)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException_Index();
            }
            return ref Unsafe.Add(ref buffer.Item1, index);
        }

    }
}
