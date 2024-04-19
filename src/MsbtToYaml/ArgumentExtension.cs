using System.Diagnostics.CodeAnalysis;

namespace MsbtToYaml;

public static class ArgumentExtension
{
    public static bool TryGetOutput(this string[] args, ref int index, [MaybeNullWhen(false)] out string output)
    {
        if (++index >= args.Length - 1) {
            output = default;
            return false;
        }

        output = args[index] switch {
            "-o" or "--output" => args[++index],
            _ => null
        };

        return output is not null;
    }
}
