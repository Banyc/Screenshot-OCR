Namespace Controller
    Public Class Controller
        Private _config As Model.Configuration

        Public Event GotScreenShot(screenShot As Bitmap)
        Public Event ConfigChanged()
        Public Event Closing()

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
            If View.ScreenShotDisplay.Num_Instance > 0 Then
                View.ScreenShotDisplay.Destroy()
            Else
                LaunchCap()
            End If
        End Sub

        ' called by hotkey event
        Private Sub LaunchCap()
            Dim screenShot As Bitmap = ScreenCap.CaptureScreen()
            RaiseEvent GotScreenShot(screenShot)
        End Sub

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
