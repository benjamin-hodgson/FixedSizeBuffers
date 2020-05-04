using System;
using Xunit;

namespace FixedSizeBuffers.Tests
{
    public class FixedSizeBufferTests
    {
        [Fact]
        public void TestIndexer()
        {
            {
                var buffer = new FixedSizeBuffer4<int>();

                buffer[0] = 123;
                Assert.Equal(123, buffer.Item1);

                buffer.Item4 = 456;
                Assert.Equal(456, buffer[3]);
            }
            {
                var buffer = new FixedSizeBuffer16<int>();

                buffer[0] = 123;
                Assert.Equal(123, buffer.Item1);

                buffer.Item16 = 456;
                Assert.Equal(456, buffer[15]);
            }
            {
                var buffer = new FixedSizeBuffer2<int>();
                Assert.Throws<ArgumentOutOfRangeException>(() => buffer[2]);
                Assert.Throws<ArgumentOutOfRangeException>(() => buffer[-1]);
            }
            {
                var buffer = new FixedSizeBuffer8<int>();
                Assert.Throws<ArgumentOutOfRangeException>(() => buffer[8]);
                Assert.Throws<ArgumentOutOfRangeException>(() => buffer[-1]);
            }
        }

        [Fact]
        public void TestItemRef()
        {
            {
                var buffer = new FixedSizeBuffer4<int>();

                buffer.ItemRef(0) = 123;
                Assert.Equal(123, buffer.Item1);

                buffer.Item4 = 456;
                Assert.Equal(456, buffer.ItemRef(3));
            }
            {
                var buffer = new FixedSizeBuffer16<int>();

                buffer.ItemRef(0) = 123;
                Assert.Equal(123, buffer.Item1);

                buffer.Item16 = 456;
                Assert.Equal(456, buffer.ItemRef(15));
            }
            {
                var buffer = new FixedSizeBuffer2<int>();
                Assert.Throws<ArgumentOutOfRangeException>(() => buffer.ItemRef(2));
                Assert.Throws<ArgumentOutOfRangeException>(() => buffer.ItemRef(-1));
            }
            {
                var buffer = new FixedSizeBuffer8<int>();
                Assert.Throws<ArgumentOutOfRangeException>(() => buffer.ItemRef(8));
                Assert.Throws<ArgumentOutOfRangeException>(() => buffer.ItemRef(-1));
            }
        }

        [Fact]
        public void TestAsSpan()
        {
            {
                var buffer = new FixedSizeBuffer4<int>();
                var span = buffer.AsSpan();

                Assert.Equal(4, span.Length);

                span[0] = 123;
                Assert.Equal(123, buffer.Item1);

                buffer.Item4 = 456;
                Assert.Equal(456, span[3]);
            }
            {
                var buffer = new FixedSizeBuffer16<int>();
                var span = buffer.AsSpan();

                Assert.Equal(16, span.Length);

                span[0] = 123;
                Assert.Equal(123, buffer.Item1);

                buffer.Item16 = 456;
                Assert.Equal(456, span[15]);
            }
        }

        [Fact]
        public void TestAsReadOnlySpan()
        {
            {
                var buffer = new FixedSizeBuffer4<int>();
                var span = buffer.AsReadOnlySpan();

                Assert.Equal(4, span.Length);

                buffer.Item4 = 456;
                Assert.Equal(456, span[3]);
            }
            {
                var buffer = new FixedSizeBuffer16<int>();
                var span = buffer.AsReadOnlySpan();

                Assert.Equal(16, span.Length);

                buffer.Item16 = 456;
                Assert.Equal(456, span[15]);
            }
        }
    }
}
