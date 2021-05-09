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

        [Fact]
        public void TestWithFixedSizeBuffer_SpanAction1()
        {
            void Test(int size)
            {
                var length = -1;
                WithFixedSizeBuffer<int>.Do(size, span => { length = span.Length; });
                Assert.Equal(size, length);
            }
            Test(0);
            Test(1);
            Test(2);
            Test(3);
            Test(4);
            Test(5);
            Test(6);
            Test(7);
            Test(8);
            Test(9);
            Test(16);
            Test(32);
            Test(8192);
        }

        [Fact]
        public void TestWithFixedSizeBuffer_SpanAction2()
        {
            void Test(int size)
            {
                var length = -1;
                var arg = -1;
                WithFixedSizeBuffer<int>.Do(
                    size,
                    123,
                    (span, x) =>
                    {
                        length = span.Length;
                        arg = x;
                    }
                );
                Assert.Equal(size, length);
                Assert.Equal(123, arg);
            }
            Test(0);
            Test(1);
            Test(2);
            Test(3);
            Test(4);
            Test(5);
            Test(6);
            Test(7);
            Test(8);
            Test(9);
            Test(16);
            Test(32);
            Test(8192);
        }

        [Fact]
        public void TestWithFixedSizeBuffer_SpanFunc1()
        {
            void Test(int size)
            {
                var length = WithFixedSizeBuffer<int>.Do(size, span => span.Length);
                Assert.Equal(size, length);
            }
            Test(0);
            Test(1);
            Test(2);
            Test(3);
            Test(4);
            Test(5);
            Test(6);
            Test(7);
            Test(8);
            Test(9);
            Test(16);
            Test(32);
            Test(8192);
        }

        [Fact]
        public void TestWithFixedSizeBuffer_SpanFunc2()
        {
            void Test(int size)
            {
                var (length, arg) = WithFixedSizeBuffer<int>.Do(size, 123, (span, x) => (span.Length, x));
                Assert.Equal(size, length);
                Assert.Equal(123, arg);
            }
            Test(0);
            Test(1);
            Test(2);
            Test(3);
            Test(4);
            Test(5);
            Test(6);
            Test(7);
            Test(8);
            Test(9);
            Test(16);
            Test(32);
            Test(8192);
        }
    }
}
