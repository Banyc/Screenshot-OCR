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
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LanguageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EngToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.traymenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'traymenu
        '
        Me.traymenu.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.traymenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LanguageToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.traymenu.Name = "traymenu"
        Me.traymenu.Size = New System.Drawing.Size(211, 80)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(210, 24)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'LanguageToolStripMenuItem
        '
        Me.LanguageToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EngToolStripMenuItem, Me.ChsToolStripMenuItem})
        Me.LanguageToolStripMenuItem.Name = "LanguageToolStripMenuItem"
        Me.LanguageToolStripMenuItem.Size = New System.Drawing.Size(210, 24)
        Me.LanguageToolStripMenuItem.Text = "Language"
        '
        'EngToolStripMenuItem
        '
        Me.EngToolStripMenuItem.Name = "EngToolStripMenuItem"
        Me.EngToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.EngToolStripMenuItem.Text = "eng"
        '
        'ChsToolStripMenuItem
        '
        Me.ChsToolStripMenuItem.Name = "ChsToolStripMenuItem"
        Me.ChsToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.ChsToolStripMenuItem.Text = "chs"
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
    Friend WithEvents LanguageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EngToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ChsToolStripMenuItem As ToolStripMenuItem
End Class
