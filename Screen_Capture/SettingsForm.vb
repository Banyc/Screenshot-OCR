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

        'Load Time_Out of SauceNAO
        txtTimeOut3.Text = Form1.Settings.SauceNAO.Timeout

        'Load combol box of hotkey options
        Dim keysArr As Array
        keysArr = System.Enum.GetValues(GetType(Keys))
        For Each key In keysArr
            'If Not System.Enum.IsDefined(GetType(Hotkey.KeyModifier), key) Then
            cbbHotkeyValue.Items.Add(key.ToString())
            'End If
        Next
        cbbHotkeyValue.SelectedItem = Form1.Settings.Hotkeys.ScreenCapture.KeyValue.ToString()
        'Load hotkey modifier
        Dim modifiersArr As Array
        modifiersArr = System.Enum.GetValues(GetType(Hotkey.KeyModifier))
        For Each modifier In modifiersArr
            cbbHotkeyModifier.Items.Add(modifier.ToString())
        Next
        cbbHotkeyModifier.SelectedItem = Form1.Settings.Hotkeys.ScreenCapture.KeyModifier.ToString()

    End Sub

    Private Sub SettingsForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        GC.Collect()
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Form1.Settings.Mode = CType(cbbMode.SelectedIndex, Form1.Mode)
        Form1.Settings.A9T9.Lang = CType(cbbLang.SelectedIndex, Form1.Language)
        Form1.Settings.A9T9.Apikey = TxtApiKey.Text
        Form1.Settings.A9T9.TimeOut = Int(txtTimeOut.Text)
        Form1.Settings.Sogou.TimeOut = Int(txtTimeOut2.Text)
        Form1.Settings.SauceNAO.Timeout = Int(txtTimeOut3.Text)
        Form1.Settings.Hotkeys.ScreenCapture.KeyValue = System.Enum.Parse(GetType(Keys), cbbHotkeyValue.SelectedItem)
        Form1.Settings.Hotkeys.ScreenCapture.KeyModifier = System.Enum.Parse(GetType(Hotkey.KeyModifier), cbbHotkeyModifier.SelectedItem)
        'Reflesh Key register state
        Form1.FinalizingRegisterHotkey()
        Form1.InitRegisterHotkey()

        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    'When changing mode
    Private Sub cbbMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbMode.SelectedIndexChanged
        Select Case cbbMode.SelectedIndex
            Case Form1.Mode.A9T9
                gbA9T9.Enabled = True
                gbSogou.Enabled = False
                gbSauceNAO.Enabled = False
            Case Form1.Mode.Sogou
                gbA9T9.Enabled = False
                gbSogou.Enabled = True
                gbSauceNAO.Enabled = False
            Case Form1.Mode.SauceNAO
                gbA9T9.Enabled = False
                gbSogou.Enabled = False
                gbSauceNAO.Enabled = True
        End Select
    End Sub
End Class
