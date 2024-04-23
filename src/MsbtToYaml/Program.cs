using CommunityToolkit.HighPerformance.Buffers;
using MessageStudio.Formats.BinaryText;
using MessageStudio.Formats.BinaryText.Components;
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
        [-f|--function-map FUNCTION_MAP] <input> [-o|--output OUTPUT] ...

          Documentation:
        github.com/ArchLeaders/MsbtToYaml
        """);
}

for (int i = 0; i < args.Length; i++) {
    string input = args[i];

    if (input.ToLower() is "-f" or "--function-map") {
        if (++i < args.Length) {
            LoadFunctionMap(args[i]);
            continue;
        }

        throw new ArgumentException("""
            Invalid function map (-f) flag. Please specify a function map (yaml) file.
            """);
    }

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

static void LoadFunctionMap(string functionMap)
{
    if (functionMap.EndsWith(".mfm")) {
        throw new ArgumentException($"""
            Invalid function map: '{functionMap}'. MFM files are not supported.
            Use MfmToYaml (github.com/ArchLeaders/MfmToYaml) to convert MFM files to YAML.
            """, nameof(functionMap));
    }

    if (File.Exists(functionMap)) {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"""
            Loading Function Map '{Path.GetFileNameWithoutExtension(functionMap)}'
            """);
        Console.ResetColor();

        FunctionMap.Current = FunctionMap.FromFile(functionMap);
        return;
    }

    if (functionMap.ToLower() is "null" or "none" or "default" or "~") {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"""
            Resetting Function Map
            """);
        Console.ResetColor();

        FunctionMap.Current = FunctionMap.Default;
        return;
    }

    throw new ArgumentException($"""
        Invalid function map: '{functionMap}'. Expected a YAML file path or 'None'.
        """, nameof(functionMap));
}