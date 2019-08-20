Namespace Controller
    Public Class Controller
        Private _config As Model.Configuration

        Public Event ConfigChanged()
        Public Event Closing()
        Public Event KeyPressedForScreenCapture()

        Public Sub New()
            _config = Model.Configuration.Load()

        End Sub

        ' register hotkeys
        Public Sub Start()
            Reload()
            ' placeholder where register hotkeys
        End Sub

        Public Sub Close()
            RaiseEvent Closing()
            _config.Save()
            End  'Kills the application.
        End Sub

        ' called by hotkey event
        Public Sub KeyPressed()
            RaiseEvent KeyPressedForScreenCapture()
        End Sub

        ' called by viewController
        Public Function GetScreenshot() As Bitmap
            Dim screenShot As Bitmap = ScreenCap.CaptureScreen()
            'RaiseEvent GotScreenShot(screenShot)
            Return screenShot
        End Function

        Public Function GetConfigCopy()
            Return Model.Configuration.Load()
        End Function

        Public Sub SaveHotkey(key As System.Windows.Input.Key, modifier As System.Windows.Input.ModifierKeys)
            _config.Hotkeys.ScreenCapture_KeyValue = key
            _config.Hotkeys.ScreenCapture_KeyModifier = modifier
            SaveConfig(_config)
        End Sub

        Public Sub SaveConfig(config As Model.Configuration)
            config.Save()
            Reload()
            RaiseEvent ConfigChanged()
        End Sub

        Private Sub Reload()
            Hotkeys.Hotkeys.ReFresh()
            _config = Model.Configuration.Load()
        End Sub

    End Class
End Namespace
