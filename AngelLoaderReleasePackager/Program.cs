//#define TESTING

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Markdig;

namespace AngelLoaderReleasePackager;

internal static class Program
{
    private static MainForm View = null!;

    private static Version? Version64;
    private static Version? Version32;

    internal enum Bitness
    {
        X86,
        X64,
    }

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        View = new MainForm();
        Application.Run(View);
    }

    internal static void Init()
    {
        string alExe64 = Path.Combine(Paths.X64ReleasePath, Paths.AngelLoaderExe);
        string alExe32 = Path.Combine(Paths.X86ReleasePath, Paths.AngelLoaderExe);

        try
        {
            FileVersionInfo ver64 = FileVersionInfo.GetVersionInfo(alExe64);
            _ = Version.TryParse(ver64.ProductVersion, out Version64);
        }
        catch
        {
            Version64 = null;
        }

        try
        {
            FileVersionInfo ver32 = FileVersionInfo.GetVersionInfo(alExe32);
            _ = Version.TryParse(ver32.ProductVersion, out Version32);
        }
        catch
        {
            Version32 = null;
        }

        View.SetFound64BitLabel(Directory.Exists(Paths.X64ReleasePath), Version64);
        View.SetFound32BitLabel(Directory.Exists(Paths.X86ReleasePath), Version32);

        if (Version64 != null && Version32 != null && Version64 != Version32)
        {
            MessageBox.Show(
                View,
                "Versions don't match between 32- and 64-bit!",
                "Alert",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private static string UrlCombine(string url, string additional)
    {
        Trace.Assert(!url.IsEmpty());
        if (url[^1] != '/') url += "/";
        return url + additional;
    }

    private static string NormalizeToCRLF(this string text)
    {
        if (!text.Contains('\r'))
        {
            text = text.Replace("\n", "\r\n");
        }

        return text;
    }

    private static void ClearDir(string path)
    {
        try
        {
            foreach (string f in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
            {
                new FileInfo(f).IsReadOnly = false;
            }
        }
        catch (DirectoryNotFoundException)
        {
            return;
        }
        catch
        {
            // ignore
        }

        try
        {
            foreach (string f in Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly))
            {
                File.Delete(f);
            }
            foreach (string d in Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly))
            {
                Directory.Delete(d, recursive: true);
            }
        }
        catch
        {
            // ignore
        }
    }

    internal static void UpdateTexts()
    {
        string raw = View.GetRawReleaseNotes().NormalizeToCRLF();

        string markdown = Process(raw, server: false);
        string serverText = Process(raw, server: true);

        string rawHTML = Markdown.ToHtml(markdown);
        string bbCode = Regex.Replace(rawHTML
            .Replace("<h4>", "[B]")
            .Replace("</h4>", "[/B]")
            .Replace("<ul>", "[LIST]")
            .Replace("</ul>", "[/LIST]")
            .Replace("<li>", "[*]")
            .Replace("</li>", "")
            .NormalizeToCRLF(), @"(\r\n)+", "\r\n");
        bbCode = Regex.Replace(bbCode, @"\</?[^\>]+\>", "");

        if (Version64 != null)
        {
            string versionString = Version64.ToString();
            const Bitness bitness = Bitness.X64;

            bbCode =
                "[URL=\"" +
                Utils.CombineAppendForwardSlash(Paths_UserDependent.ReleaseDownloadUrlBase) + "v" + versionString +
                "/AngelLoader_v" + versionString + "_" + GetBitnessString(bitness) + ".zip\"][B]AngelLoader v" +
                versionString + "[/B][/URL] is out.\r\n\r\n" + bbCode;
        }

        View.SetTTLGReleaseNotes(bbCode.TrimEnd());
        View.SetServerReleaseNotes(serverText);
        View.SetMarkdownWebViewHTML(rawHTML);
        View.SetMarkdownReleaseNotes(markdown);

        return;

        static string Process(string raw, bool server)
        {
            int multiplier = server ? 1 : 2;
            string prefix = server ? " " : "";
            string[] rawLines = raw.Split("\r\n");
            for (int i = 0; i < rawLines.Length; i++)
            {
                string line = rawLines[i];
                if (line.TrimStart().StartsWith('-'))
                {
                    int hyphenIndex = line.IndexOf('-');
                    rawLines[i] = string.Concat(prefix, new string(' ', hyphenIndex * multiplier), "- ", line.AsSpan(hyphenIndex + 1));
                }
                else if (line.TrimEnd().EndsWith(':'))
                {
                    if (!server)
                    {
                        rawLines[i] = "#### " + line;
                    }
                }
            }

            return string.Join("\r\n", rawLines);
        }
    }

    /*
    The GitHub pages server takes a couple minutes after upload to have the new data appear in get requests, but
    this is fine since we'll be committing the whole thing (versions/changelog/latest version) in one go, so it
    will all become available at once.

    @Update: Updating the latest version file is the very last thing that should be done by the release packager
    We want everything in place when the app finds a new version defined there.
    */
    internal static async Task CreateRelease()
    {
        if (Version64 == null && Version32 == null) return;

        if (Version64 != null) Package(Bitness.X64, Version64);
        if (Version32 != null) Package(Bitness.X86, Version32);

        string versionString = Version64?.ToString() ?? Version32!.ToString();

        Directory.CreateDirectory(Paths.ReleaseNotesPath);
        ClearDir(Paths.ReleaseNotesPath);
        string rawReleaseNotes = View.GetRawReleaseNotes().NormalizeToCRLF();
        string serverReleaseNotes = View.GetServerReleaseNotes().NormalizeToCRLF();
        string ttlgReleaseNotes = View.GetTTLGReleaseNotes().NormalizeToCRLF();

        if (rawReleaseNotes.IsWhiteSpace() ||
            serverReleaseNotes.IsWhiteSpace() ||
            ttlgReleaseNotes.IsWhiteSpace())
        {
            MessageBox.Show("Blank release notes - aborting create.");
            return;
        }

        // Default is to write without BOM, but let's be absolutely strict and explicit for robustness.
        UTF8Encoding utf8NoBOM = new(false, true);

        // Maybe we just keep the app open and don't need to write these files...
        await File.WriteAllTextAsync(Paths.RawReleaseNotesFile, rawReleaseNotes, utf8NoBOM);
        await File.WriteAllTextAsync(Paths.TTLGReleaseNotesFile, ttlgReleaseNotes, utf8NoBOM);

        if (Version64 != null)
        {
            string latestVersionFileLocal = Paths.GetLatestVersionFileLocal(Bitness.X64);
            Directory.CreateDirectory(Path.GetDirectoryName(latestVersionFileLocal)!);
            await File.WriteAllTextAsync(latestVersionFileLocal, versionString, utf8NoBOM);
            await WriteVersionsFile(Bitness.X64, utf8NoBOM);
            await File.WriteAllTextAsync(Paths.GetReleaseNotesFileLocal(Bitness.X64, Version64), serverReleaseNotes, utf8NoBOM);
        }
        if (Version32 != null)
        {
            string latestVersionFileLocal = Paths.GetLatestVersionFileLocal(Bitness.X86);
            Directory.CreateDirectory(Path.GetDirectoryName(latestVersionFileLocal)!);
            await File.WriteAllTextAsync(latestVersionFileLocal, versionString, utf8NoBOM);
            await WriteVersionsFile(Bitness.X86, utf8NoBOM);
            await File.WriteAllTextAsync(Paths.GetReleaseNotesFileLocal(Bitness.X86, Version32), serverReleaseNotes, utf8NoBOM);
        }

        try
        {
            Directory.Delete(Paths.X64ReleasePath, recursive: true);
        }
        catch
        {
            // ignore
        }
        try
        {
            Directory.Delete(Paths.X86ReleasePath, recursive: true);
        }
        catch
        {
            // ignore
        }

        using (Process.Start(new ProcessStartInfo { FileName = Paths_UserDependent.ReleaseBasePath, UseShellExecute = true })) { }

        MessageBox.Show(View, "Release created!");

        return;

        #region Local functions

        async Task WriteVersionsFile(Bitness bitness, UTF8Encoding encoding)
        {
            string bitnessString = GetBitnessString(bitness);

            string versionsFileLocal = Paths.GetVersionsFileLocal(bitness);

            List<string> versionsIniLines =
                File.Exists(versionsFileLocal)
                    ? (await File.ReadAllLinesAsync(versionsFileLocal, encoding)).ToList()
                    : new List<string>();

            if (versionsIniLines.Count > 0)
            {
                for (int i = 0; i < versionsIniLines.Count; i++)
                {
                    if (versionsIniLines[i].IsWhiteSpace())
                    {
                        versionsIniLines.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        break;
                    }
                }

                if (versionsIniLines[0].Trim() == "[" + versionString + "]")
                {
                    versionsIniLines.RemoveAt(0);
                    for (int i = 0; i < versionsIniLines.Count; i++)
                    {
                        if (!versionsIniLines[i].Trim().StartsWith('['))
                        {
                            versionsIniLines.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            List<string> newVersionIniLines = new()
            {
                "[" + versionString + "]",
                "ChangelogUrl=" + UrlCombine(Paths.GetServerRepoPath(bitness), "changelog-" + versionString + ".txt"),
                "DownloadUrl=" + Utils.CombineAppendForwardSlash(Paths_UserDependent.ReleaseDownloadUrlBase) +
                "v" + versionString +
                "/AngelLoader_v" + versionString + "_" + bitnessString + ".zip",
            };
            if (versionsIniLines.Count > 0)
            {
                newVersionIniLines.Add("");
            }
            List<string> finalVersionIniLines = new(newVersionIniLines.Count + versionsIniLines.Count);
            finalVersionIniLines.AddRange(newVersionIniLines);
            finalVersionIniLines.AddRange(versionsIniLines);

            await File.WriteAllLinesAsync(Paths.GetVersionsFileLocal(bitness), finalVersionIniLines, encoding);
        }

        #endregion
    }

    #region Package

    private static string GetBitnessString(Bitness bitness) => bitness == Bitness.X64 ? "x64" : "x86";

    private static void Package(Bitness bitness, Version version)
    {
        string bitnessString = GetBitnessString(bitness);

        string releaseDir = bitness == Bitness.X64 ? Paths.X64ReleasePath : Paths.X86ReleasePath;

        string inputPath = Path.Combine(Paths_UserDependent.ReleaseBasePath, releaseDir);

        try
        {
            string[] files = Directory.GetFiles(inputPath, "*", SearchOption.AllDirectories);
            if (files.Length == 0)
            {
                MessageBox.Show(View, "No files in '" + inputPath + "'");
                return;
            }
        }
        catch (DirectoryNotFoundException ex)
        {
            MessageBox.Show(View,
                "Directory not found: '" + inputPath + "'.\r\n\r\n" +
                "Exception:\r\n\r\n" +
                ex);
            return;
        }
        catch (Exception ex)
        {
            MessageBox.Show(View,
                "Error while trying to get the list of files in '" + inputPath + "'.\r\n\r\n" +
                "Exception:\r\n\r\n" +
                ex);
            return;
        }

        string outputArchive = Path.Combine(Paths_UserDependent.ReleaseBasePath, "AngelLoader_v" + version + "_" + bitnessString + ".zip");
        using (var sevenZipProc = new Process())
        {
            sevenZipProc.StartInfo.FileName = Path.Combine(Application.StartupPath, "7z.exe");
            sevenZipProc.StartInfo.WorkingDirectory = Application.StartupPath;
            try
            {
                File.Delete(outputArchive);
            }
            catch
            {
                // ignore
            }

            sevenZipProc.StartInfo.Arguments =
                "a \"" + outputArchive + "\" \"" + Path.Combine(inputPath, "*.*") + "\" "
                // -r        = Recurse subdirectories
                // -y        = Say yes to all prompts automatically
                // -mx=9     = Compression level Ultra (maximum)
                // -mfb=257  = Max fast bytes (max compression)
                // -mpass=15 = Max passes (max compression)
                // -mcu=on   = Always use UTF-8 for non-ASCII file names
                + "-r -y -mx=9 -mfb=257 -mpass=15 -mcu=on";

            sevenZipProc.StartInfo.CreateNoWindow = false;

            RunProcess(sevenZipProc);
        }

        using (var zopfliProc = new Process())
        {
            zopfliProc.StartInfo.FileName = Path.Combine(Application.StartupPath, "RecompressZip-winx64", "RecompressZip.exe");
            zopfliProc.StartInfo.WorkingDirectory = Path.Combine(Application.StartupPath, "RecompressZip-winx64");
            zopfliProc.StartInfo.Arguments = "-n 12 \"" + outputArchive + "\"";
            zopfliProc.StartInfo.CreateNoWindow = false;

            RunProcess(zopfliProc);
        }
    }

    private static void RunProcess(Process p)
    {
        try
        {
            p.Start();
            p.WaitForExit();

            if (p.ExitCode != 0)
            {
                MessageBox.Show(View,
                    "Error exit code from " + Path.GetFileName(p.StartInfo.FileName) + ": " + p.ExitCode);
                return;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(View,
                "Exception while running " + Path.GetFileName(p.StartInfo.FileName) + "." +
                "Exception:\r\n\r\n" +
                ex);
            return;
        }
        finally
        {
            try
            {
                if (!p.HasExited)
                {
                    p.Kill();
                }
            }
            catch
            {
                // ignore
            }
            finally
            {
                try
                {
                    if (!p.HasExited)
                    {
                        p.WaitForExit();
                    }
                }
                catch
                {
                    // ignore...
                }

                p.Dispose();
            }
        }
    }

    #endregion

    internal static void PushUpdateInfoToServer()
    {
        if (Version64 == null || Version32 == null || Version64 != Version32) return;

        /*
        When we put anything in the local repo, these are the git commands we need to run for the upload to take:

        git add --all
        git commit -m <commit message>
        git pull
        git push

        This ensures all files are tracked, then it actually commits the changes, then it pushes.
        Need to pull and then push in order to properly have it work.
        */
        using (Process.Start(
                   new ProcessStartInfo
                   {
                       FileName = Paths_UserDependent.GitServerUpdateBatchFile,
                       WorkingDirectory = Path.GetDirectoryName(Paths_UserDependent.GitServerUpdateBatchFile),
                       Arguments = "\"" + Paths_UserDependent.AngelLoaderUpdatesRepoPath + "\"" + " " + Version64,
                       CreateNoWindow = false,
                   }))
        {
        }
    }
}
