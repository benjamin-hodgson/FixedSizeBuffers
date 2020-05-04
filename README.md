`FixedSizeBuffers`
==================

A collection of fixed-size structs which can be treated as `Span`s. A replacement for `stackalloc` (or `fixed`-size buffers), but these buffers can contain reference types (and can't be dynamically sized).

```csharp
void Copy(TextReader reader, TextWriter writer)
{
    // Allocate 2kB on the stack
    var buffer = new FixedSizeBuffer1024<char>();
    var span = buffer.AsSpan();

    var count = -1;
    while (count != 0)
    {
        count = reader.Read(span);
        writer.Write(span.Slice(count));
    }
    
    buffer.Dispose();
}
```

Buffers are provided in powers-of-two lengths, from 2 to 8192.

I hope you know what you're doing
---------------------------------

Use of this library is potentially **unsafe**. When calling `AsSpan` or `AsReadOnlySpan`, you must make sure the `Span` doesn't outlive the `FixedSizeBuffer`. Basically, don't return the `Span`.

```csharp
Span<int> Bad()
{
    var buffer = new FixedSizeBuffer4<int>();
    return buffer.AsSpan();  // Don't do this
}
```

You _can_ safely pass around spans into buffers which live on the heap — but if you're doing that you might as well use an array.
