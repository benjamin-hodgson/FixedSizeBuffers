using System.IO;

namespace FixedSizeBuffers.CodeGen
{
    class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllText(
                "FixedSizeBuffers/FixedSizeBuffer.Generated.cs",
                FixedSizeBufferGenerator.GenerateFixedSizeBuffers()
            );
            File.WriteAllText(
                "FixedSizeBuffers/FixedSizeBufferExtensions.Generated.cs",
                FixedSizeBufferExtensionsGenerator.GenerateFixedSizeBufferExtensions()
            );
            File.WriteAllText(
                "FixedSizeBuffers/WithFixedSizeBuffer.Generated.cs",
                WithFixedSizeBufferGenerator.GenerateWithFixedSizeBuffer()
            );
        }
    }
}
