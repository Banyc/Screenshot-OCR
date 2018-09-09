Imports System.ComponentModel

Public Class SettingsForm
    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Limit the Form size
        Me.MaximumSize = Me.Size

        'https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/constants-enums/how-to-iterate-through-an-enumeration
        Dim items As Array
        'Load Mode
        items = System.Enum.GetValues(GetType(Form1.Mode))
        Dim mode As Form1.Mode
        For Each mode In items
            cbbMode.Items.Add(mode.ToString)
        Next
        cbbMode.SelectedIndex = Form1.Settings.Mode

        'Load Language of A9T9
        items = System.Enum.GetValues(GetType(Form1.Language))
        Dim lang As Form1.Language
        For Each lang In items
            cbbLang.Items.Add(lang.ToString)
        Next
        cbbLang.SelectedIndex = Form1.Settings.A9T9.Lang
        'Load API Key of A9T9
        TxtApiKey.Text = Form1.Settings.A9T9.Apikey
        'Load Time_Out of A9T9
        txtTimeOut.Text = Form1.Settings.A9T9.TimeOut

        'Load Time_Out of Sogou
        txtTimeOut2.Text = Form1.Settings.Sogou.TimeOut
    End Sub

    'Private Sub cbbMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbMode.SelectedIndexChanged
    '    Form1.Settings.Mode = CType(cbbMode.SelectedIndex, Form1.Mode)
    'End Sub

    'Private Sub cbbLang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbLang.SelectedIndexChanged
    '    Form1.Settings.A9T9.Lang = CType(cbbLang.SelectedIndex, Form1.Language)
    'End Sub

    Private Sub SettingsForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        GC.Collect()
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Form1.Settings.Mode = CType(cbbMode.SelectedIndex, Form1.Mode)
        Form1.Settings.A9T9.Lang = CType(cbbLang.SelectedIndex, Form1.Language)
        Form1.Settings.A9T9.Apikey = TxtApiKey.Text
        Form1.Settings.A9T9.TimeOut = Int(txtTimeOut.Text)
        Form1.Settings.Sogou.TimeOut = Int(txtTimeOut2.Text)
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub cbbMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbMode.SelectedIndexChanged
        Select Case cbbMode.SelectedIndex
            Case Form1.Mode.A9T9
                gbA9T9.Enabled = True
                gbSogou.Enabled = False
            Case Form1.Mode.Sogou
                gbA9T9.Enabled = False
                gbSogou.Enabled = True
        End Select
    End Sub
End Class