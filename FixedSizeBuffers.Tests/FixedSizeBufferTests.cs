using System;

using Xunit;

namespace FixedSizeBuffers.Tests;

public class FixedSizeBufferTests
{
    [Fact]
    public void TestIndexer()
    {
        {
            var buffer = default(FixedSizeBuffer4<int>);

            buffer[0] = 123;
            Assert.Equal(123, buffer.Item1);

            buffer.Item4 = 456;
            Assert.Equal(456, buffer[3]);
        }

        {
            var buffer = default(FixedSizeBuffer16<int>);

            buffer[0] = 123;
            Assert.Equal(123, buffer.Item1);

            buffer.Item16 = 456;
            Assert.Equal(456, buffer[15]);
        }

        {
            var buffer = default(FixedSizeBuffer2<int>);
            Assert.Throws<ArgumentOutOfRangeException>(() => buffer[2]);
            Assert.Throws<ArgumentOutOfRangeException>(() => buffer[-1]);
        }

        {
            var buffer = default(FixedSizeBuffer8<int>);
            Assert.Throws<ArgumentOutOfRangeException>(() => buffer[8]);
            Assert.Throws<ArgumentOutOfRangeException>(() => buffer[-1]);
        }
    }

    [Fact]
    public void TestItemRef()
    {
        {
            var buffer = default(FixedSizeBuffer4<int>);

            buffer.ItemRef(0) = 123;
            Assert.Equal(123, buffer.Item1);

            buffer.Item4 = 456;
            Assert.Equal(456, buffer.ItemRef(3));
        }

        {
            var buffer = default(FixedSizeBuffer16<int>);

            buffer.ItemRef(0) = 123;
            Assert.Equal(123, buffer.Item1);

            buffer.Item16 = 456;
            Assert.Equal(456, buffer.ItemRef(15));
        }

        {
            var buffer = default(FixedSizeBuffer2<int>);
            Assert.Throws<ArgumentOutOfRangeException>(() => buffer.ItemRef(2));
            Assert.Throws<ArgumentOutOfRangeException>(() => buffer.ItemRef(-1));
        }

        {
            var buffer = default(FixedSizeBuffer8<int>);
            Assert.Throws<ArgumentOutOfRangeException>(() => buffer.ItemRef(8));
            Assert.Throws<ArgumentOutOfRangeException>(() => buffer.ItemRef(-1));
        }
    }

    [Fact]
    public void TestAsSpan()
    {
        {
            var buffer = default(FixedSizeBuffer4<int>);
            var span = buffer.AsSpan();

            Assert.Equal(4, span.Length);

            span[0] = 123;
            Assert.Equal(123, buffer.Item1);

            buffer.Item4 = 456;
            Assert.Equal(456, span[3]);
        }

        {
            var buffer = default(FixedSizeBuffer16<int>);
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
            var buffer = default(FixedSizeBuffer4<int>);
            var span = buffer.AsReadOnlySpan();

            Assert.Equal(4, span.Length);

            buffer.Item4 = 456;
            Assert.Equal(456, span[3]);
        }

        {
            var buffer = default(FixedSizeBuffer16<int>);
            var span = buffer.AsReadOnlySpan();

            Assert.Equal(16, span.Length);

            buffer.Item16 = 456;
            Assert.Equal(456, span[15]);
        }
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(16)]
    [InlineData(32)]
    [InlineData(8192)]
    public void TestWithFixedSizeBuffer_SpanAction1(int size)
    {
        var length = -1;
        WithFixedSizeBuffer<int>.Do(size, span => { length = span.Length; });
        Assert.Equal(size, length);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(16)]
    [InlineData(32)]
    [InlineData(8192)]
    public void TestWithFixedSizeBuffer_SpanAction2(int size)
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

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(16)]
    [InlineData(32)]
    [InlineData(8192)]
    public void TestWithFixedSizeBuffer_SpanFunc1(int size)
    {
        var length = WithFixedSizeBuffer<int>.Do(size, span => span.Length);
        Assert.Equal(size, length);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(16)]
    [InlineData(32)]
    [InlineData(8192)]
    public void TestWithFixedSizeBuffer_SpanFunc2(int size)
    {
        var (length, arg) = WithFixedSizeBuffer<int>.Do(size, 123, (span, x) => (span.Length, x));
        Assert.Equal(size, length);
        Assert.Equal(123, arg);
    }
}
