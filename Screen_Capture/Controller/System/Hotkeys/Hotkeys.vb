Imports GlobalHotKey

Namespace Controller.Hotkeys
    Public Class Hotkeys
        Private Shared _hotkeyManager As HotKeyManager
        Private Shared Hotkey As System.Windows.Input.Key = Nothing
        Private Shared Modifier As Model.KeyModifier = Nothing

        Private Shared _controller As Controller

        Public Shared Sub Init(controller As Controller)
            _hotkeyManager = New HotKeyManager()
            AddHandler _hotkeyManager.KeyPressed, AddressOf HotKeyManagerPressed

            _controller = controller

            Load()
        End Sub

        Private Shared Sub Load()
            Dim keyConfig As Model.HotkeyConfig
            keyConfig = _controller.GetConfigCopy().Hotkeys

            Hotkey = keyConfig.ScreenCapture_KeyValue
            Modifier = keyConfig.ScreenCapture_KeyModifier

            _hotkeyManager.Register(keyConfig.ScreenCapture_KeyValue, keyConfig.ScreenCapture_KeyModifier)
            '_hotkeyManager.Register(Windows.Input.Key.F4, Windows.Input.ModifierKeys.None)
        End Sub

        Public Shared Sub ReFresh()
            If Hotkey <> Nothing Then
                _hotkeyManager.Unregister(Hotkey, Modifier)
                Load()
            End If
        End Sub

        Private Shared Sub HotKeyManagerPressed(sender As Object, e As KeyPressedEventArgs)
            If Hotkey = e.HotKey.Key Then
                _controller.KeyPressed()
            End If
        End Sub
    End Class
End Namespace
