using CommunityToolkit.HighPerformance.Buffers;
using MessageStudio.Formats.BinaryText;
using MsbtToYaml;
using Revrs.Extensions;
using System.Reflection;
using System.Text;

const ulong MSBT_MAGIC = 0x6E4264745367734DUL;

Console.WriteLine($"""
    MSBT to YAML [Version {Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "???"}]
    (c) 2024 ArchLeaders. MIT.

    """);

if (args.Length == 0 || args[0].ToLower() is "h" or "help" or "-h" or "--help") {
    Console.WriteLine("""
          Usage:
        <input> [-o|--output OUTPUT] ...

          Documentation:
        github.com/ArchLeaders/MsbtToYaml
        """);
}

for (int i = 0; i < args.Length; i++) {
    string input = args[i];

    using FileStream fs = File.OpenRead(input);
    int size = Convert.ToInt32(fs.Length);
    using SpanOwner<byte> buffer = SpanOwner<byte>.Allocate(size);
    fs.Read(buffer.Span);
    fs.Dispose();

    bool isMsbt = buffer.Span.Read<ulong>() == MSBT_MAGIC;

    if (!args.TryGetOutput(ref i, out string? output)) {
        output = Path.ChangeExtension(input, isMsbt switch {
            true => "yaml",
            false => "msbt"
        });
    }

    if (isMsbt) {
        Msbt msbt = Msbt.FromBinary(buffer.Span);
        string yaml = msbt.ToYaml();
        File.WriteAllText(output, yaml);
    }
    else {
        using SpanOwner<char> yaml = SpanOwner<char>.Allocate(size);
        Encoding.UTF8.GetChars(buffer.Span, yaml.Span);
        Msbt msbt = Msbt.FromYaml(yaml.Span);
        using FileStream outputFileStream = File.Create(output);
        msbt.WriteBinary(outputFileStream);
    }
}