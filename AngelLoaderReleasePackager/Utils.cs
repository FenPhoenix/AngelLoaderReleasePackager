using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace AngelLoaderReleasePackager;

internal static class Utils
{
    internal static string CombineAppendForwardSlash(params string[] urls)
    {
        string final = "";
        foreach (string url in urls)
        {
            Trace.Assert(!url.IsEmpty());
            final += url;
            if (url[^1] != '/')
            {
                final += "/";
            }
        }
        return final;
    }

    #region Empty / whitespace checks

    /// <summary>
    /// Returns true if <paramref name="value"/> is null or empty.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [ContractAnnotation("null => true")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmpty([NotNullWhen(false)] this string? value) => string.IsNullOrEmpty(value);

    /// <summary>
    /// Returns true if <paramref name="value"/> is null, empty, or whitespace.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [ContractAnnotation("null => true")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsWhiteSpace([NotNullWhen(false)] this string? value) => string.IsNullOrWhiteSpace(value);

    #endregion
}
