
Namespace View
    Public Class ViewController

        Private WithEvents _controller As Controller.Controller
        Private WithEvents _tray As System.Windows.Forms.NotifyIcon
        'Private WithEvents _screenShotDisplay As ScreenShotDisplay
        'Private WithEvents _settingForm As SettingsForm
        'Private WithEvents _outputForm As OutputForm
        'Private WithEvents _loadingBox As LoadingBox

        Public Sub New(controller As Controller.Controller)
            _controller = controller
            ' Add event
            AddHandler _controller.GotScreenShot, AddressOf controller_GotScreenShot
            AddHandler _controller.ConfigChanged, AddressOf controller_ConfigChanged
            AddHandler _controller.Closing, AddressOf controller_Closing

            Dim components = New System.ComponentModel.Container()
            _tray = New System.Windows.Forms.NotifyIcon()

            _tray.Icon = Icon.ExtractAssociatedIcon("tray.ico")
            _tray.Text = "Screenshot OCR"
            _tray.Visible = True

            'Avoid running the same program twice
            If UBound(Process.GetProcessesByName(Process.GetCurrentProcess.ProcessName)) > 0 Then
                _tray.Visible = False 'Hides the tray icon. if we don't do this we can kill the app, but the icon will still be there
                End
            End If

            'Dim screenShotDisplay As ScreenShotDisplay
            '_screenShotDisplay = New ScreenShotDisplay(_controller)
        End Sub

        Private Sub controller_GotScreenShot(screenShot As Bitmap)
            Dim screenShotDisplay As ScreenShotDisplay
            screenShotDisplay = New ScreenShotDisplay(_controller)
            screenShotDisplay.SetDisplayingImage(screenShot)
            screenShotDisplay.Show()
        End Sub

        Private Sub controller_ConfigChanged()

        End Sub

        Private Sub controller_Closing()
            _tray.Visible = False 'Hides the tray icon. if we don't do this we can kill the app, but the icon will still be there
        End Sub

        Private Sub OnExit()
            _tray.Dispose()
            _controller.Close()
        End Sub

        Private Sub _tray_MouseClick(sender As Object, e As MouseEventArgs) Handles _tray.MouseClick
            Select Case e.Button
                Case Windows.Forms.MouseButtons.Right 'Checks if the pressed button is the Right Mouse
                    Dim trayform As New trayform(_controller)
                    trayform.Show() 'Shows the Form that is the parent of "traymenu"
                    trayform.Activate() 'Set the Form to "Active", that means that that will be the "selected" window
                    trayform.Width = 1 'Set the Form width to 1 pixel, that is needed because later we will set it behind the "traymenu"
                    trayform.Height = 1 'Set the Form Height to 1 pixel, for the same reason as above
                Case Windows.Forms.MouseButtons.Left
                    Dim settingsForm As New SettingsForm(_controller)
                    settingsForm.Show()
                    SettingsForm.Activate()
            End Select
        End Sub
    End Class

End Namespace
