using System.IO;

using FixedSizeBuffers.CodeGen;

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
