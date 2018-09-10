<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class trayform
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.traymenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UploadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.A9T9ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.traymenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'traymenu
        '
        Me.traymenu.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.traymenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UploadToolStripMenuItem, Me.SettingsToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.traymenu.Name = "traymenu"
        Me.traymenu.Size = New System.Drawing.Size(139, 76)
        '
        'UploadToolStripMenuItem
        '
        Me.UploadToolStripMenuItem.Name = "UploadToolStripMenuItem"
        Me.UploadToolStripMenuItem.Size = New System.Drawing.Size(138, 24)
        Me.UploadToolStripMenuItem.Text = "Upload"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(138, 24)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(138, 24)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'A9T9ToolStripMenuItem
        '
        Me.A9T9ToolStripMenuItem.Name = "A9T9ToolStripMenuItem"
        Me.A9T9ToolStripMenuItem.Size = New System.Drawing.Size(122, 26)
        Me.A9T9ToolStripMenuItem.Text = "A9T9"
        '
        'ModeToolStripMenuItem
        '
        Me.ModeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.A9T9ToolStripMenuItem})
        Me.ModeToolStripMenuItem.Name = "ModeToolStripMenuItem"
        Me.ModeToolStripMenuItem.Size = New System.Drawing.Size(210, 24)
        Me.ModeToolStripMenuItem.Text = "Mode"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "jpeg"
        '
        'trayform
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "trayform"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "trayform"
        Me.TopMost = True
        Me.traymenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents traymenu As ContextMenuStrip
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents A9T9ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ModeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UploadToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
End Class
