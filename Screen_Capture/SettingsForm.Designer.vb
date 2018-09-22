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
        Me.txtTimeOut = New System.Windows.Forms.TextBox()
        Me.TxtApiKey = New System.Windows.Forms.TextBox()
        Me.lblTimeOut = New System.Windows.Forms.Label()
        Me.lblApiKey = New System.Windows.Forms.Label()
        Me.lblLang = New System.Windows.Forms.Label()
        Me.cbbLang = New System.Windows.Forms.ComboBox()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.gbSogou = New System.Windows.Forms.GroupBox()
        Me.txtTimeOut2 = New System.Windows.Forms.TextBox()
        Me.lblTimeOut2 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.gbHotkey_Screenshot = New System.Windows.Forms.GroupBox()
        Me.lblKeyValue = New System.Windows.Forms.Label()
        Me.cbbHotkeyModifier = New System.Windows.Forms.ComboBox()
        Me.cbbHotkeyValue = New System.Windows.Forms.ComboBox()
        Me.lblKeyModifier = New System.Windows.Forms.Label()
        Me.gbA9T9.SuspendLayout()
        Me.gbSogou.SuspendLayout()
        Me.gbHotkey_Screenshot.SuspendLayout()
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
        Me.gbA9T9.Controls.Add(Me.txtTimeOut)
        Me.gbA9T9.Controls.Add(Me.TxtApiKey)
        Me.gbA9T9.Controls.Add(Me.lblTimeOut)
        Me.gbA9T9.Controls.Add(Me.lblApiKey)
        Me.gbA9T9.Controls.Add(Me.lblLang)
        Me.gbA9T9.Controls.Add(Me.cbbLang)
        Me.gbA9T9.Location = New System.Drawing.Point(15, 35)
        Me.gbA9T9.Name = "gbA9T9"
        Me.gbA9T9.Size = New System.Drawing.Size(217, 115)
        Me.gbA9T9.TabIndex = 2
        Me.gbA9T9.TabStop = False
        Me.gbA9T9.Text = "A9T9"
        '
        'txtTimeOut
        '
        Me.txtTimeOut.Location = New System.Drawing.Point(83, 81)
        Me.txtTimeOut.Name = "txtTimeOut"
        Me.txtTimeOut.Size = New System.Drawing.Size(121, 25)
        Me.txtTimeOut.TabIndex = 3
        '
        'TxtApiKey
        '
        Me.TxtApiKey.Location = New System.Drawing.Point(83, 50)
        Me.TxtApiKey.Name = "TxtApiKey"
        Me.TxtApiKey.Size = New System.Drawing.Size(121, 25)
        Me.TxtApiKey.TabIndex = 3
        '
        'lblTimeOut
        '
        Me.lblTimeOut.AutoSize = True
        Me.lblTimeOut.Location = New System.Drawing.Point(6, 84)
        Me.lblTimeOut.Name = "lblTimeOut"
        Me.lblTimeOut.Size = New System.Drawing.Size(71, 15)
        Me.lblTimeOut.TabIndex = 2
        Me.lblTimeOut.Text = "Time Out"
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
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(157, 297)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(75, 23)
        Me.btnApply.TabIndex = 3
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'gbSogou
        '
        Me.gbSogou.Controls.Add(Me.txtTimeOut2)
        Me.gbSogou.Controls.Add(Me.lblTimeOut2)
        Me.gbSogou.Location = New System.Drawing.Point(15, 156)
        Me.gbSogou.Name = "gbSogou"
        Me.gbSogou.Size = New System.Drawing.Size(217, 51)
        Me.gbSogou.TabIndex = 2
        Me.gbSogou.TabStop = False
        Me.gbSogou.Text = "Sogou"
        '
        'txtTimeOut2
        '
        Me.txtTimeOut2.Location = New System.Drawing.Point(83, 17)
        Me.txtTimeOut2.Name = "txtTimeOut2"
        Me.txtTimeOut2.Size = New System.Drawing.Size(121, 25)
        Me.txtTimeOut2.TabIndex = 3
        '
        'lblTimeOut2
        '
        Me.lblTimeOut2.AutoSize = True
        Me.lblTimeOut2.Location = New System.Drawing.Point(6, 20)
        Me.lblTimeOut2.Name = "lblTimeOut2"
        Me.lblTimeOut2.Size = New System.Drawing.Size(71, 15)
        Me.lblTimeOut2.TabIndex = 2
        Me.lblTimeOut2.Text = "Time Out"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(15, 297)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'gbHotkey_Screenshot
        '
        Me.gbHotkey_Screenshot.Controls.Add(Me.lblKeyModifier)
        Me.gbHotkey_Screenshot.Controls.Add(Me.lblKeyValue)
        Me.gbHotkey_Screenshot.Controls.Add(Me.cbbHotkeyModifier)
        Me.gbHotkey_Screenshot.Controls.Add(Me.cbbHotkeyValue)
        Me.gbHotkey_Screenshot.Location = New System.Drawing.Point(15, 213)
        Me.gbHotkey_Screenshot.Name = "gbHotkey_Screenshot"
        Me.gbHotkey_Screenshot.Size = New System.Drawing.Size(217, 78)
        Me.gbHotkey_Screenshot.TabIndex = 2
        Me.gbHotkey_Screenshot.TabStop = False
        Me.gbHotkey_Screenshot.Text = "Hotkey For Screenshot"
        '
        'lblKeyValue
        '
        Me.lblKeyValue.AutoSize = True
        Me.lblKeyValue.Location = New System.Drawing.Point(6, 20)
        Me.lblKeyValue.Name = "lblKeyValue"
        Me.lblKeyValue.Size = New System.Drawing.Size(79, 15)
        Me.lblKeyValue.TabIndex = 2
        Me.lblKeyValue.Text = "Key Value"
        '
        'cbbHotkeyModifier
        '
        Me.cbbHotkeyModifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbbHotkeyModifier.FormattingEnabled = True
        Me.cbbHotkeyModifier.Location = New System.Drawing.Point(83, 46)
        Me.cbbHotkeyModifier.Name = "cbbHotkeyModifier"
        Me.cbbHotkeyModifier.Size = New System.Drawing.Size(121, 23)
        Me.cbbHotkeyModifier.TabIndex = 1
        '
        'cbbHotkeyValue
        '
        Me.cbbHotkeyValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbbHotkeyValue.FormattingEnabled = True
        Me.cbbHotkeyValue.Location = New System.Drawing.Point(83, 17)
        Me.cbbHotkeyValue.Name = "cbbHotkeyValue"
        Me.cbbHotkeyValue.Size = New System.Drawing.Size(121, 23)
        Me.cbbHotkeyValue.TabIndex = 1
        '
        'lblKeyModifier
        '
        Me.lblKeyModifier.AutoSize = True
        Me.lblKeyModifier.Location = New System.Drawing.Point(6, 49)
        Me.lblKeyModifier.Name = "lblKeyModifier"
        Me.lblKeyModifier.Size = New System.Drawing.Size(71, 15)
        Me.lblKeyModifier.TabIndex = 2
        Me.lblKeyModifier.Text = "Modifier"
        '
        'SettingsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(248, 328)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.gbHotkey_Screenshot)
        Me.Controls.Add(Me.gbSogou)
        Me.Controls.Add(Me.gbA9T9)
        Me.Controls.Add(Me.cbbMode)
        Me.Controls.Add(Me.lblMode)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "SettingsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settings"
        Me.gbA9T9.ResumeLayout(False)
        Me.gbA9T9.PerformLayout()
        Me.gbSogou.ResumeLayout(False)
        Me.gbSogou.PerformLayout()
        Me.gbHotkey_Screenshot.ResumeLayout(False)
        Me.gbHotkey_Screenshot.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblMode As Label
    Friend WithEvents cbbMode As ComboBox
    Friend WithEvents gbA9T9 As GroupBox
    Friend WithEvents lblLang As Label
    Friend WithEvents cbbLang As ComboBox
    Friend WithEvents TxtApiKey As TextBox
    Friend WithEvents lblApiKey As Label
    Friend WithEvents btnApply As Button
    Friend WithEvents txtTimeOut As TextBox
    Friend WithEvents lblTimeOut As Label
    Friend WithEvents gbSogou As GroupBox
    Friend WithEvents txtTimeOut2 As TextBox
    Friend WithEvents lblTimeOut2 As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents gbHotkey_Screenshot As GroupBox
    Friend WithEvents lblKeyValue As Label
    Friend WithEvents cbbHotkeyModifier As ComboBox
    Friend WithEvents cbbHotkeyValue As ComboBox
    Friend WithEvents lblKeyModifier As Label
End Class
