using System;
using System.Drawing;
using System.Windows.Forms;

namespace AngelLoaderReleasePackager;

public sealed partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();

        Found32BitLabel.Text = "";
        Found64BitLabel.Text = "";
    }

    protected override async void OnShown(EventArgs e)
    {
        base.OnShown(e);

        MarkdownHTMLWebView.Hide();

        await MarkdownHTMLWebView.EnsureCoreWebView2Async();

        MarkdownHTMLWebView.Show();

        Program.Init();
    }

    private void ReleaseNotesRawTextBox_TextChanged(object sender, EventArgs e) => Program.UpdateTexts();

    internal string GetRawReleaseNotes() => ReleaseNotesRawTextBox.Text;

    internal string GetServerReleaseNotes() => ReleaseNotesServerTextBox.Text;

    internal void SetServerReleaseNotes(string text) => ReleaseNotesServerTextBox.Text = text;

    internal void SetMarkdownWebViewHTML(string text) => MarkdownHTMLWebView.NavigateToString(text);

    internal string GetTTLGReleaseNotes() => ReleaseNotesTTLGTextBox.Text;

    internal void SetTTLGReleaseNotes(string text) => ReleaseNotesTTLGTextBox.Text = text;

    internal string GetMarkdownReleaseNotes() => ReleaseNotesMarkdownTextBox.Text;

    internal void SetMarkdownReleaseNotes(string text) => ReleaseNotesMarkdownTextBox.Text = text;

    internal void SetFound32BitLabel(bool found, Version? version)
    {
        if (found)
        {
            Found32BitLabel.ForeColor = Color.Green;
            Found32BitLabel.Text = "32-bit found";
        }
        else
        {
            Found32BitLabel.ForeColor = Color.Red;
            Found32BitLabel.Text = "32-bit not found";
        }

        Found32BitLabel.Text += " - " + (version?.ToString() ?? "");
    }

    internal void SetFound64BitLabel(bool found, Version? version)
    {
        if (found)
        {
            Found64BitLabel.ForeColor = Color.Green;
            Found64BitLabel.Text = "64-bit found";
        }
        else
        {
            Found64BitLabel.ForeColor = Color.Red;
            Found64BitLabel.Text = "64-bit not found";
        }

        Found64BitLabel.Text += " - " + (version?.ToString() ?? "");
    }
    private async void CreateReleaseButton_Click(object sender, EventArgs e) => await Program.CreateRelease();

    private void PushUpdateToServerButton_Click(object sender, EventArgs e) => Program.PushUpdateInfoToServer();

    private void Test1Button_Click(object sender, EventArgs e)
    {
    }
}
