namespace AngelLoaderReleasePackager;

sealed partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        ReleaseNotesRawTextBox = new System.Windows.Forms.TextBox();
        ReleaseNotesTTLGTextBox = new System.Windows.Forms.TextBox();
        ReleaseNotesRawLabel = new System.Windows.Forms.Label();
        ReleaseNotesTTLGLabel = new System.Windows.Forms.Label();
        ReleaseNotesServerTextBox = new System.Windows.Forms.TextBox();
        ReleaseNotesServerLabel = new System.Windows.Forms.Label();
        CreateReleaseButton = new System.Windows.Forms.Button();
        MarkdownHTMLWebView = new Microsoft.Web.WebView2.WinForms.WebView2();
        ReleaseNotesHTMLLabel = new System.Windows.Forms.Label();
        Found32BitLabel = new System.Windows.Forms.Label();
        Found64BitLabel = new System.Windows.Forms.Label();
        ReleaseNotesMarkdownTextBox = new System.Windows.Forms.TextBox();
        ReleaseNotesMarkdownLabel = new System.Windows.Forms.Label();
        PushUpdateToServerButton = new System.Windows.Forms.Button();
        ManualWorkLabel = new System.Windows.Forms.Label();
        Test1Button = new System.Windows.Forms.Button();
        ReleaseNotesHelpLabel = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)MarkdownHTMLWebView).BeginInit();
        SuspendLayout();
        // 
        // ReleaseNotesRawTextBox
        // 
        ReleaseNotesRawTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        ReleaseNotesRawTextBox.Location = new System.Drawing.Point(19, 33);
        ReleaseNotesRawTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ReleaseNotesRawTextBox.Multiline = true;
        ReleaseNotesRawTextBox.Name = "ReleaseNotesRawTextBox";
        ReleaseNotesRawTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        ReleaseNotesRawTextBox.Size = new System.Drawing.Size(621, 322);
        ReleaseNotesRawTextBox.TabIndex = 1;
        ReleaseNotesRawTextBox.TextChanged += ReleaseNotesRawTextBox_TextChanged;
        // 
        // ReleaseNotesTTLGTextBox
        // 
        ReleaseNotesTTLGTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        ReleaseNotesTTLGTextBox.Location = new System.Drawing.Point(656, 33);
        ReleaseNotesTTLGTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ReleaseNotesTTLGTextBox.Multiline = true;
        ReleaseNotesTTLGTextBox.Name = "ReleaseNotesTTLGTextBox";
        ReleaseNotesTTLGTextBox.ReadOnly = true;
        ReleaseNotesTTLGTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        ReleaseNotesTTLGTextBox.Size = new System.Drawing.Size(304, 322);
        ReleaseNotesTTLGTextBox.TabIndex = 5;
        // 
        // ReleaseNotesRawLabel
        // 
        ReleaseNotesRawLabel.AutoSize = true;
        ReleaseNotesRawLabel.Location = new System.Drawing.Point(19, 16);
        ReleaseNotesRawLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        ReleaseNotesRawLabel.Name = "ReleaseNotesRawLabel";
        ReleaseNotesRawLabel.Size = new System.Drawing.Size(286, 15);
        ReleaseNotesRawLabel.TabIndex = 0;
        ReleaseNotesRawLabel.Text = "Release notes (raw*, will be converted to Markdown):";
        // 
        // ReleaseNotesTTLGLabel
        // 
        ReleaseNotesTTLGLabel.AutoSize = true;
        ReleaseNotesTTLGLabel.Location = new System.Drawing.Point(656, 16);
        ReleaseNotesTTLGLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        ReleaseNotesTTLGLabel.Name = "ReleaseNotesTTLGLabel";
        ReleaseNotesTTLGLabel.Size = new System.Drawing.Size(120, 15);
        ReleaseNotesTTLGLabel.TabIndex = 4;
        ReleaseNotesTTLGLabel.Text = "Release notes (TTLG):";
        // 
        // ReleaseNotesServerTextBox
        // 
        ReleaseNotesServerTextBox.Font = new System.Drawing.Font("Consolas", 12F);
        ReleaseNotesServerTextBox.Location = new System.Drawing.Point(656, 384);
        ReleaseNotesServerTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ReleaseNotesServerTextBox.Multiline = true;
        ReleaseNotesServerTextBox.Name = "ReleaseNotesServerTextBox";
        ReleaseNotesServerTextBox.ReadOnly = true;
        ReleaseNotesServerTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        ReleaseNotesServerTextBox.Size = new System.Drawing.Size(616, 322);
        ReleaseNotesServerTextBox.TabIndex = 7;
        // 
        // ReleaseNotesServerLabel
        // 
        ReleaseNotesServerLabel.AutoSize = true;
        ReleaseNotesServerLabel.Location = new System.Drawing.Point(656, 368);
        ReleaseNotesServerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        ReleaseNotesServerLabel.Name = "ReleaseNotesServerLabel";
        ReleaseNotesServerLabel.Size = new System.Drawing.Size(164, 15);
        ReleaseNotesServerLabel.TabIndex = 6;
        ReleaseNotesServerLabel.Text = "Release notes (server version):";
        // 
        // CreateReleaseButton
        // 
        CreateReleaseButton.Location = new System.Drawing.Point(1280, 96);
        CreateReleaseButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        CreateReleaseButton.Name = "CreateReleaseButton";
        CreateReleaseButton.Size = new System.Drawing.Size(160, 27);
        CreateReleaseButton.TabIndex = 10;
        CreateReleaseButton.Text = "Prepare release";
        CreateReleaseButton.UseVisualStyleBackColor = true;
        CreateReleaseButton.Click += CreateReleaseButton_Click;
        // 
        // MarkdownHTMLWebView
        // 
        MarkdownHTMLWebView.AllowExternalDrop = true;
        MarkdownHTMLWebView.CreationProperties = null;
        MarkdownHTMLWebView.DefaultBackgroundColor = System.Drawing.Color.White;
        MarkdownHTMLWebView.Location = new System.Drawing.Point(19, 384);
        MarkdownHTMLWebView.Name = "MarkdownHTMLWebView";
        MarkdownHTMLWebView.Size = new System.Drawing.Size(621, 322);
        MarkdownHTMLWebView.TabIndex = 3;
        MarkdownHTMLWebView.ZoomFactor = 1D;
        // 
        // ReleaseNotesHTMLLabel
        // 
        ReleaseNotesHTMLLabel.AutoSize = true;
        ReleaseNotesHTMLLabel.Location = new System.Drawing.Point(16, 368);
        ReleaseNotesHTMLLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        ReleaseNotesHTMLLabel.Name = "ReleaseNotesHTMLLabel";
        ReleaseNotesHTMLLabel.Size = new System.Drawing.Size(203, 15);
        ReleaseNotesHTMLLabel.TabIndex = 2;
        ReleaseNotesHTMLLabel.Text = "Release notes (Markdown via HTML):";
        // 
        // Found32BitLabel
        // 
        Found32BitLabel.AutoSize = true;
        Found32BitLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        Found32BitLabel.Location = new System.Drawing.Point(1288, 40);
        Found32BitLabel.Name = "Found32BitLabel";
        Found32BitLabel.Size = new System.Drawing.Size(79, 15);
        Found32BitLabel.TabIndex = 8;
        Found32BitLabel.Text = "[Found32Bit]";
        // 
        // Found64BitLabel
        // 
        Found64BitLabel.AutoSize = true;
        Found64BitLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        Found64BitLabel.Location = new System.Drawing.Point(1288, 56);
        Found64BitLabel.Name = "Found64BitLabel";
        Found64BitLabel.Size = new System.Drawing.Size(79, 15);
        Found64BitLabel.TabIndex = 9;
        Found64BitLabel.Text = "[Found64Bit]";
        // 
        // ReleaseNotesMarkdownTextBox
        // 
        ReleaseNotesMarkdownTextBox.Font = new System.Drawing.Font("Consolas", 12F);
        ReleaseNotesMarkdownTextBox.Location = new System.Drawing.Point(968, 33);
        ReleaseNotesMarkdownTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        ReleaseNotesMarkdownTextBox.Multiline = true;
        ReleaseNotesMarkdownTextBox.Name = "ReleaseNotesMarkdownTextBox";
        ReleaseNotesMarkdownTextBox.ReadOnly = true;
        ReleaseNotesMarkdownTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        ReleaseNotesMarkdownTextBox.Size = new System.Drawing.Size(304, 322);
        ReleaseNotesMarkdownTextBox.TabIndex = 7;
        // 
        // ReleaseNotesMarkdownLabel
        // 
        ReleaseNotesMarkdownLabel.AutoSize = true;
        ReleaseNotesMarkdownLabel.Location = new System.Drawing.Point(968, 16);
        ReleaseNotesMarkdownLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        ReleaseNotesMarkdownLabel.Name = "ReleaseNotesMarkdownLabel";
        ReleaseNotesMarkdownLabel.Size = new System.Drawing.Size(240, 15);
        ReleaseNotesMarkdownLabel.TabIndex = 4;
        ReleaseNotesMarkdownLabel.Text = "Release notes (Markdown / GitHub Release):";
        // 
        // PushUpdateToServerButton
        // 
        PushUpdateToServerButton.Location = new System.Drawing.Point(1280, 192);
        PushUpdateToServerButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        PushUpdateToServerButton.Name = "PushUpdateToServerButton";
        PushUpdateToServerButton.Size = new System.Drawing.Size(160, 27);
        PushUpdateToServerButton.TabIndex = 10;
        PushUpdateToServerButton.Text = "Push update info to server";
        PushUpdateToServerButton.UseVisualStyleBackColor = true;
        PushUpdateToServerButton.Click += PushUpdateToServerButton_Click;
        // 
        // ManualWorkLabel
        // 
        ManualWorkLabel.AutoSize = true;
        ManualWorkLabel.Location = new System.Drawing.Point(1280, 128);
        ManualWorkLabel.Name = "ManualWorkLabel";
        ManualWorkLabel.Size = new System.Drawing.Size(232, 60);
        ManualWorkLabel.TabIndex = 11;
        ManualWorkLabel.Text = "Then do manual work:\r\nCreate GitHub release, update TTLG thread\r\n\r\nThen:";
        // 
        // Test1Button
        // 
        Test1Button.Location = new System.Drawing.Point(1584, 8);
        Test1Button.Name = "Test1Button";
        Test1Button.Size = new System.Drawing.Size(75, 23);
        Test1Button.TabIndex = 12;
        Test1Button.Text = "Test1";
        Test1Button.UseVisualStyleBackColor = true;
        Test1Button.Click += Test1Button_Click;
        // 
        // ReleaseNotesHelpLabel
        // 
        ReleaseNotesHelpLabel.Location = new System.Drawing.Point(1288, 368);
        ReleaseNotesHelpLabel.Name = "ReleaseNotesHelpLabel";
        ReleaseNotesHelpLabel.Size = new System.Drawing.Size(376, 336);
        ReleaseNotesHelpLabel.TabIndex = 13;
        ReleaseNotesHelpLabel.Text = resources.GetString("ReleaseNotesHelpLabel.Text");
        // 
        // MainForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(1670, 725);
        Controls.Add(ReleaseNotesHelpLabel);
        Controls.Add(Test1Button);
        Controls.Add(ManualWorkLabel);
        Controls.Add(Found64BitLabel);
        Controls.Add(Found32BitLabel);
        Controls.Add(MarkdownHTMLWebView);
        Controls.Add(PushUpdateToServerButton);
        Controls.Add(CreateReleaseButton);
        Controls.Add(ReleaseNotesHTMLLabel);
        Controls.Add(ReleaseNotesServerLabel);
        Controls.Add(ReleaseNotesMarkdownLabel);
        Controls.Add(ReleaseNotesTTLGLabel);
        Controls.Add(ReleaseNotesRawLabel);
        Controls.Add(ReleaseNotesMarkdownTextBox);
        Controls.Add(ReleaseNotesServerTextBox);
        Controls.Add(ReleaseNotesTTLGTextBox);
        Controls.Add(ReleaseNotesRawTextBox);
        Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        Name = "MainForm";
        Text = "AngelLoader Release Packager";
        ((System.ComponentModel.ISupportInitialize)MarkdownHTMLWebView).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.TextBox ReleaseNotesRawTextBox;
    private System.Windows.Forms.TextBox ReleaseNotesTTLGTextBox;
    private System.Windows.Forms.Label ReleaseNotesRawLabel;
    private System.Windows.Forms.Label ReleaseNotesTTLGLabel;
    private System.Windows.Forms.TextBox ReleaseNotesServerTextBox;
    private System.Windows.Forms.Label ReleaseNotesServerLabel;
    private System.Windows.Forms.Button CreateReleaseButton;
    private Microsoft.Web.WebView2.WinForms.WebView2 MarkdownHTMLWebView;
    private System.Windows.Forms.Label ReleaseNotesHTMLLabel;
    private System.Windows.Forms.Label Found32BitLabel;
    private System.Windows.Forms.Label Found64BitLabel;
    private System.Windows.Forms.TextBox ReleaseNotesMarkdownTextBox;
    private System.Windows.Forms.Label ReleaseNotesMarkdownLabel;
    private System.Windows.Forms.Button PushUpdateToServerButton;
    private System.Windows.Forms.Label ManualWorkLabel;
    private System.Windows.Forms.Button Test1Button;
    private System.Windows.Forms.Label ReleaseNotesHelpLabel;
}
