<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SettingsForm
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
        Me.lblMode = New System.Windows.Forms.Label()
        Me.cbbMode = New System.Windows.Forms.ComboBox()
        Me.gbA9T9 = New System.Windows.Forms.GroupBox()
        Me.TxtApiKey = New System.Windows.Forms.TextBox()
        Me.lblApiKey = New System.Windows.Forms.Label()
        Me.lblLang = New System.Windows.Forms.Label()
        Me.cbbLang = New System.Windows.Forms.ComboBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.gbA9T9.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblMode
        '
        Me.lblMode.AutoSize = True
        Me.lblMode.Location = New System.Drawing.Point(12, 9)
        Me.lblMode.Name = "lblMode"
        Me.lblMode.Size = New System.Drawing.Size(39, 15)
        Me.lblMode.TabIndex = 0
        Me.lblMode.Text = "Mode"
        '
        'cbbMode
        '
        Me.cbbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbbMode.FormattingEnabled = True
        Me.cbbMode.Location = New System.Drawing.Point(57, 6)
        Me.cbbMode.Name = "cbbMode"
        Me.cbbMode.Size = New System.Drawing.Size(121, 23)
        Me.cbbMode.TabIndex = 1
        '
        'gbA9T9
        '
        Me.gbA9T9.Controls.Add(Me.TxtApiKey)
        Me.gbA9T9.Controls.Add(Me.lblApiKey)
        Me.gbA9T9.Controls.Add(Me.lblLang)
        Me.gbA9T9.Controls.Add(Me.cbbLang)
        Me.gbA9T9.Controls.Add(Me.ComboBox1)
        Me.gbA9T9.Location = New System.Drawing.Point(15, 35)
        Me.gbA9T9.Name = "gbA9T9"
        Me.gbA9T9.Size = New System.Drawing.Size(217, 84)
        Me.gbA9T9.TabIndex = 2
        Me.gbA9T9.TabStop = False
        Me.gbA9T9.Text = "A9T9"
        '
        'TxtApiKey
        '
        Me.TxtApiKey.Location = New System.Drawing.Point(83, 50)
        Me.TxtApiKey.Name = "TxtApiKey"
        Me.TxtApiKey.Size = New System.Drawing.Size(121, 25)
        Me.TxtApiKey.TabIndex = 3
        '
        'lblApiKey
        '
        Me.lblApiKey.AutoSize = True
        Me.lblApiKey.Location = New System.Drawing.Point(6, 53)
        Me.lblApiKey.Name = "lblApiKey"
        Me.lblApiKey.Size = New System.Drawing.Size(63, 15)
        Me.lblApiKey.TabIndex = 2
        Me.lblApiKey.Text = "API Key"
        '
        'lblLang
        '
        Me.lblLang.AutoSize = True
        Me.lblLang.Location = New System.Drawing.Point(6, 21)
        Me.lblLang.Name = "lblLang"
        Me.lblLang.Size = New System.Drawing.Size(71, 15)
        Me.lblLang.TabIndex = 0
        Me.lblLang.Text = "Language"
        '
        'cbbLang
        '
        Me.cbbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbbLang.FormattingEnabled = True
        Me.cbbLang.Location = New System.Drawing.Point(83, 18)
        Me.cbbLang.Name = "cbbLang"
        Me.cbbLang.Size = New System.Drawing.Size(121, 23)
        Me.cbbLang.TabIndex = 1
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(83, 18)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 23)
        Me.ComboBox1.TabIndex = 1
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(161, 125)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(75, 23)
        Me.btnApply.TabIndex = 3
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'SettingsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(248, 159)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.gbA9T9)
        Me.Controls.Add(Me.cbbMode)
        Me.Controls.Add(Me.lblMode)
        Me.Name = "SettingsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settings"
        Me.gbA9T9.ResumeLayout(False)
        Me.gbA9T9.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblMode As Label
    Friend WithEvents cbbMode As ComboBox
    Friend WithEvents gbA9T9 As GroupBox
    Friend WithEvents lblLang As Label
    Friend WithEvents cbbLang As ComboBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents TxtApiKey As TextBox
    Friend WithEvents lblApiKey As Label
    Friend WithEvents btnApply As Button
End Class
