//#define TESTING

using System;
using System.IO;
using static AngelLoaderReleasePackager.Paths_UserDependent;
using static AngelLoaderReleasePackager.Program;

namespace AngelLoaderReleasePackager;

internal static class Paths
{
    internal const string AngelLoaderExe = "AngelLoader.exe";

    internal static readonly string X64ReleasePath = Path.Combine(ReleaseBasePath, "framework_x64");
    internal static readonly string X86ReleasePath = Path.Combine(ReleaseBasePath, "framework_x86");
    // If/when we release the .NET modern version, enable this
    //internal static readonly string X64NETReleasePath = Path.Combine(ReleaseBasePath, "netmodern_x64");
    internal static readonly string ReleaseNotesPath = Path.Combine(ReleaseBasePath, "ReleaseNotes");
    internal static readonly string RawReleaseNotesFile = Path.Combine(ReleaseNotesPath, "Raw_release_notes.txt");
    internal static readonly string MarkdownReleaseNotesFile = Path.Combine(ReleaseNotesPath, "Markdown_release_notes.txt");
    internal static readonly string TTLGReleaseNotesFile = Path.Combine(ReleaseNotesPath, "TTLG_release_notes.txt");

#if TESTING
    private const string _updatesRepoDir = "updates_testing";
#else
    private const string _updatesRepoDir = "updates";
#endif

    private const string _bitnessRepoDir64 = "framework_x64";
    private const string _bitnessRepoDir32 = "framework_x86";
    // If/when we release the .NET modern version, enable this
    //private const string _bitnessRepoDirNET32 = "netmodern_x64";
    internal static string GetBitnessRepoDir(Bitness bitness) => bitness == Bitness.X64 ? _bitnessRepoDir64 : _bitnessRepoDir32;

    private static readonly string _serverRepo64 = Utils.CombineAppendForwardSlash(_serverRepoBase, _updatesRepoDir, _bitnessRepoDir64);
    private static readonly string _serverRepo32 = Utils.CombineAppendForwardSlash(_serverRepoBase, _updatesRepoDir, _bitnessRepoDir32);
    internal static string GetServerRepoPath(Bitness bitness) => bitness == Bitness.X64 ? _serverRepo64 : _serverRepo32;

    private static readonly string _updateRepoPathLocal64 = Path.Combine(AngelLoaderUpdatesRepoPath, _updatesRepoDir, _bitnessRepoDir64);
    private static readonly string _updateRepoPathLocal32 = Path.Combine(AngelLoaderUpdatesRepoPath, _updatesRepoDir, _bitnessRepoDir32);
    internal static string GetUpdateRepoPathLocal(Bitness bitness) => bitness == Bitness.X64 ? _updateRepoPathLocal64 : _updateRepoPathLocal32;

    private static readonly string _latestVersionFileLocal64 = Path.Combine(_updateRepoPathLocal64, "latest_version.txt");
    private static readonly string _latestVersionFileLocal32 = Path.Combine(_updateRepoPathLocal32, "latest_version.txt");
    internal static string GetLatestVersionFileLocal(Bitness bitness) => bitness == Bitness.X64 ? _latestVersionFileLocal64 : _latestVersionFileLocal32;

    private static readonly string _versionsFileLocal64 = Path.Combine(_updateRepoPathLocal64, "versions.ini");
    private static readonly string _versionsFileLocal32 = Path.Combine(_updateRepoPathLocal32, "versions.ini");
    internal static string GetVersionsFileLocal(Bitness bitness) => bitness == Bitness.X64 ? _versionsFileLocal64 : _versionsFileLocal32;

    internal static string GetReleaseNotesFileLocal(Bitness bitness, Version version)
    {
        return Path.Combine(GetUpdateRepoPathLocal(bitness), "changelog-" + version + ".txt");
    }
}
