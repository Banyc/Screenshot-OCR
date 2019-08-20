Imports System.ComponentModel

Public Class SettingsForm
    Private _controller As Controller.Controller
    Private _config As Model.Configuration
    Public Sub New(controller As Controller.Controller)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        _controller = controller
        _config = _controller.GetConfigCopy()
    End Sub
    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Limit the Form size
        Me.MaximumSize = Me.Size

        'https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/constants-enums/how-to-iterate-through-an-enumeration
        'Dim items As Array
        'Load Mode
        'items = System.Enum.GetValues(GetType(Model.Configuration.ProcessMode))
        'Dim mode As Model.Configuration.ProcessMode
        'For Each mode In items
        '    cbbMode.Items.Add(mode.ToString)
        'Next
        AddItemFromEnum(cbbMode.Items, GetType(Model.Configuration.ProcessMode))
        cbbMode.SelectedIndex = _config.Mode

        'Load Language of A9T9
        'items = System.Enum.GetValues(GetType(Model.Configuration.Language))
        'Dim lang As Model.Configuration.Language
        'For Each lang In items
        '    cbbLang.Items.Add(lang.ToString())
        'Next
        AddItemFromEnum(cbbLang.Items, GetType(Model.Configuration.Language))
        cbbLang.SelectedIndex = _config.A9T9_Lang
        'Load API Key of A9T9
        TxtApiKey.Text = _config.A9T9_Apikey
        'Load Time_Out of A9T9
        txtTimeOut.Text = _config.A9T9_TimeOut

        'Load Time_Out of Sogou
        txtTimeOut2.Text = _config.Sogou_TimeOut

        'Load Time_Out of SauceNAO
        txtTimeOut3.Text = _config.SauceNAO_Timeout

        'Load combol box of hotkey options
        'Dim keysArr As Array
        'keysArr = System.Enum.GetValues(GetType(System.Windows.Input.Key))
        'For Each key In keysArr
        '    'If Not System.Enum.IsDefined(GetType(Hotkey.KeyModifier), key) Then
        '    cbbHotkeyValue.Items.Add(key.ToString())
        '    'End If
        'Next
        AddItemFromEnum(cbbHotkeyValue.Items, GetType(System.Windows.Input.Key))
        cbbHotkeyValue.SelectedItem = _config.Hotkeys.ScreenCapture_KeyValue.ToString()
        'Load hotkey modifier
        'Dim modifiersArr As Array
        'modifiersArr = System.Enum.GetValues(GetType(Model.KeyModifier))
        'For Each modifier In modifiersArr
        '    cbbHotkeyModifier.Items.Add(modifier.ToString())
        'Next
        AddItemFromEnum(cbbHotkeyModifier.Items, GetType(Model.KeyModifier))
        cbbHotkeyModifier.SelectedItem = _config.Hotkeys.ScreenCapture_KeyModifier.ToString()

    End Sub

    Private Sub AddItemFromEnum(list As ComboBox.ObjectCollection, enumType As Type)
        Dim items As Array = System.Enum.GetValues(enumType)
        For Each item In items
            list.Add(item.ToString())
        Next
    End Sub

    Private Sub SettingsForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        GC.Collect()
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        _config.Mode = CType(cbbMode.SelectedIndex, Model.Configuration.ProcessMode)
        _config.A9T9_Lang = CType(cbbLang.SelectedIndex, Model.Configuration.Language)
        _config.A9T9_Apikey = TxtApiKey.Text
        _config.A9T9_TimeOut = Int(txtTimeOut.Text)
        _config.Sogou_TimeOut = Int(txtTimeOut2.Text)
        _config.SauceNAO_Timeout = Int(txtTimeOut3.Text)
        _config.Hotkeys.ScreenCapture_KeyValue = System.Enum.Parse(GetType(Windows.Input.Key), cbbHotkeyValue.SelectedItem)
        _config.Hotkeys.ScreenCapture_KeyModifier = System.Enum.Parse(GetType(Model.KeyModifier), cbbHotkeyModifier.SelectedItem)

        'Reflesh Key register state
        _controller.SaveConfig(_config)

        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    'When changing mode
    Private Sub cbbMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbbMode.SelectedIndexChanged
        Select Case cbbMode.SelectedIndex
            Case Model.Configuration.ProcessMode.A9T9
                gbA9T9.Enabled = True
                gbSogou.Enabled = False
                gbSauceNAO.Enabled = False
            Case Model.Configuration.ProcessMode.Sogou
                gbA9T9.Enabled = False
                gbSogou.Enabled = True
                gbSauceNAO.Enabled = False
            Case Model.Configuration.ProcessMode.SauceNAO
                gbA9T9.Enabled = False
                gbSogou.Enabled = False
                gbSauceNAO.Enabled = True
        End Select
    End Sub

    Private Sub SettingsForm_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.Close()
    End Sub
End Class
