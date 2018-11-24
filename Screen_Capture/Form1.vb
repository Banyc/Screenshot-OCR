Imports System.Net.Http
Imports System.Text.RegularExpressions
Imports System.Diagnostics

Public Class Form1
    Private WithEvents kbHook As New KeyboardHook
    Private _paintEvent_graphics As Graphics  'For dispose a Graphics in other function
#If DEBUG Then
    Private g As Graphics  ' pointer-like Graphics on Form1. for test only
#End If
    Private _startPoint As Point  ' regional capture rectangle's starting point
    Private IsMouseDown As Boolean = False
    Private Const iniPath As String = "./config.ini"

    Public Enum Language  ' detective language
        ara = 0  'Arabic
        bul  'Bulgarian
        chs  'Chinese(Simplified)
        cht  'Chinese(Traditional)
        hrv  'Croatian
        cze  'Czech
        dan  'Danish
        dut  'Dutch
        eng  'English
        fin  'Finnish
        fre  'French
        ger  'German
        gre  'Greek
        hun  'Hungarian
        kor  'Korean
        ita  'Italian
        jpn  'Japanese
        nor  'Norwegian
        pol  'Polish
        por  'Portuguese
        rus  'Russian
        slv  'Slovenian
        spa  'Spanish
        swe  'Swedish
        tur  'Turkish
    End Enum

    Public Enum Mode
        A9T9 = 0
        Sogou
        SauceNAO
    End Enum

    Public Enum HotkeyId
        ScreenCapture
    End Enum

    Public Structure Settings
        Public Shared Property Mode As Mode
        Public Structure A9T9
            Public Shared Property Apikey As String
            Public Shared Property Lang As Language
            Public Shared Property TimeOut As Integer  'of Seconds
        End Structure
        Public Structure Sogou
            Public Shared Property TimeOut As Integer  'of Seconds
        End Structure
        Public Structure SauceNAO
            Public Shared Property Timeout As Integer  'of Seconds
        End Structure
        Public Structure Hotkeys
            Public Structure ScreenCapture  'store hotkey activating screenshot mode
                Public Shared Property KeyValue As Keys
                Public Shared Property KeyModifier As Hotkey.KeyModifier

                Public Shared Property HotkeyId As HotkeyId  'This ID is used to distinguish different hotkey in Winsdows message context
            End Structure
        End Structure
        Public Structure Advance
            Public Shared Property EraseAllNewlines As Boolean
        End Structure
    End Structure

    '===== Reference:= https://social.msdn.microsoft.com/Forums/windows/en-US/5dc1b32b-7b7e-41fe-af87-d491d7021bd3/vbnet-smooth-rectangle-drawing-using-mousedrag?forum=winforms
    Public _mRect As Rectangle  'TODO: remember to turn it back to private

    '--=====-- Overrides --=====--
    Public Sub New()
        InitializeComponent()
        Me.DoubleBuffered = True
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        If e.Button = MouseButtons.Right Then
            FinishingFrm()
        Else
            IsMouseDown = True
            'Label1.Text = "MouseDown"
            _startPoint = New Size(e.X, e.Y)
            Me.Invalidate()
        End If
    End Sub

    ' Draw a rectangle
    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        'https://stackoverflow.com/questions/17839869/run-when-when-you-ctrl-click-a-button-in-a-winform
        If e.Button = Windows.Forms.MouseButtons.Left And Not My.Computer.Keyboard.CtrlKeyDown Then  ' preserve the previous rectangle when ctrl is pressed
            _mRect = New Rectangle(Math.Min(_startPoint.X, e.X), Math.Min(_startPoint.Y, e.Y),
                                  Math.Max(_startPoint.X, e.X) - Math.Min(_startPoint.X, e.X),
                                  Math.Max(_startPoint.Y, e.Y) - Math.Min(_startPoint.Y, e.Y))
            Me.Invalidate()
        End If
    End Sub

    ' Graphics settings
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Using pen As New Pen(Color.Red, 3)
            e.Graphics.DrawRectangle(pen, _mRect.X - 2, _mRect.Y - 2, _mRect.Width + 3, _mRect.Height + 3)  'In order not to include the red edges when capturing _mRect area
            _paintEvent_graphics = e.Graphics  ' ready to dispose
        End Using
    End Sub
    '===== End Reference

    'For registered Hotkey hooking
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = Hotkey.WM_HOTKEY Then
            Debug.WriteLine("m.Msg: " & m.Msg.ToString)
            Select Case m.WParam.ToInt32  'm.WParam here is the self-identified registered Id of the specific hotkey
                Case Settings.Hotkeys.ScreenCapture.HotkeyId
                    Screenshot_keyPressed()
            End Select
            Debug.WriteLine("m.WParam: " & m.WParam.ToString)
        End If
        MyBase.WndProc(m)
    End Sub 'System wide hotkey event handling
    '--=====-- End Overrides --=====--

    '--=====-- Initiation --=====--
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'InitWindow()  ' Moved to tmrFrmLoad tick event
        tmrFrmLoad.Enabled = True
        'Avoid running the same program twice
        If UBound(Process.GetProcessesByName(Process.GetCurrentProcess.ProcessName)) > 0 Then
            tray.Visible = False 'Hides the tray icon. if we don't do this we can kill the app, but the icon will still be there
            End
        End If
    End Sub

    ''' <summary>
    ''' Hides Form1 at startup. The feature does NOT effect in Frm.Load Event.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tmrFrmLoad_Tick(sender As Object, e As EventArgs) Handles tmrFrmLoad.Tick
        tmrFrmLoad.Enabled = False
        Me.Hide()
        InitWindow()
        InitIniFile()
    End Sub
    '--=====-- End Initiation --=====--

    '--=====-- Finalization --=====--
    'This Sub might be skipped. Go to trayform.ExitToolStripMenuItem_Click() function
    Public Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        FinalizingIniFile()
    End Sub
    '--=====-- End Finalization --=====--

    '--=====-- Events --=====--
    Private Sub Screenshot_keyPressed()
        If Me.Visible = False Then
            Dim screenShot As Bitmap = TakeScreenShot()
#If DEBUG Then
            'for debug
            g = Me.CreateGraphics()  ' moved to Me.BackgroundImage
#End If
            If Settings.Mode = Mode.A9T9 Then
                lbl_lang.Text = "Language: " & Settings.A9T9.Lang.ToString
            Else
                lbl_lang.Text = "Language: " & "Default"
            End If
            lblState.Text = "Mode: " & Settings.Mode.ToString()
            Put_g_OnForm(screenShot)
            Me.Show()

        Else  ' stop grapping regional screenshot mannually(exit grapping mode)
            FinishingFrm()
        End If
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown  ' not working until the first successful screenshot
        If e.KeyCode = Keys.Escape Then
            FinishingFrm()
        End If
    End Sub

    Private Sub Frm_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        If IsMouseDown = True Then

            IsMouseDown = False
            'Label1.Text = "MouseUp"

            If _mRect <> Nothing And _mRect.Size.Width <> 0 And _mRect.Size.Height <> 0 Then

                Dim capturedScreen As Bitmap = TakeRegionalScreenShot(_mRect)

                'For Sogou API only: either width or height must be larger than or equal to 50
                'https://stackoverflow.com/questions/2144592/resizing-images-in-vb-net
                If Settings.Mode = Mode.Sogou And (_mRect.Width < 50 Or _mRect.Height < 50) Then
                    Dim newBitmap As New Bitmap(Math.Max(_mRect.Width, 50), Math.Max(_mRect.Height, 50))  'BUG:  from area too small still identify nothing
                    Dim drawMannual As Graphics = Graphics.FromImage(newBitmap)
                    drawMannual.Clear(Color.White)
                    drawMannual.DrawImage(capturedScreen, 0, 0)
                    capturedScreen.Dispose()
                    capturedScreen = newBitmap
                End If

                '#If DEBUG Then
                '                g.DrawImage(capturedScreen, 1, 100)  'The last two args represent left top point from Me 
                '#End If
                HttpRequests.AutoDirectOCR(capturedScreen)  'Warning: <Awaitable!>
            End If
            '#If Not DEBUG Then  'Auto Exit screenshot mode
            FinishingFrm()
'#End If

        End If
    End Sub

    Private Sub tray_MouseClick(sender As Object, e As MouseEventArgs) Handles tray.MouseClick
        Select Case e.Button
            Case Windows.Forms.MouseButtons.Right 'Checks if the pressed button is the Right Mouse
                trayform.Show() 'Shows the Form that is the parent of "traymenu"
                trayform.Activate() 'Set the Form to "Active", that means that that will be the "selected" window
                trayform.Width = 1 'Set the Form width to 1 pixel, that is needed because later we will set it behind the "traymenu"
                trayform.Height = 1 'Set the Form Height to 1 pixel, for the same reason as above
            Case Windows.Forms.MouseButtons.Left
                If SettingsForm.Visible Then
                    SettingsForm.Close()
                Else
                    SettingsForm.Show()
                    SettingsForm.Activate()
                End If
        End Select
    End Sub
    '--=====-- End Events --=====--

    '--=====-- Functions --=====--
    'When closing/hiding the Window
    Private Sub FinishingFrm()
        Me.Hide()
        Me.BackgroundImage.Dispose()  ' privacy protection and ready to release RAM.
        _paintEvent_graphics.Dispose()

        '' erase the previous rectangle  'Now reserve the previous drawn rectangle
        '_mRect = Nothing
        ''Me.Invalidate()  'Since Me is hidden, no need for Me's re-painting

        _startPoint = Nothing

        GC.Collect()  ' RAM releases
    End Sub

    ''' <summary>
    ''' [url](https://stackoverflow.com/questions/10930569/high-quality-full-screenshots-vb-net)
    ''' </summary>
    ''' <returns></returns>
    Private Function TakeScreenShot() As Bitmap
        Dim screenSize As Size = New Size(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)
        Dim screenGrab As New Bitmap(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)  ' pointer-like
        Dim g1 As Graphics = Graphics.FromImage(screenGrab)
        g1.CopyFromScreen(New Point(0, 0), New Point(0, 0), screenSize)
        Return screenGrab
    End Function

    Private Function TakeRegionalScreenShot(rect As Rectangle) As Bitmap
        Dim screenGrab As New Bitmap(rect.Width, rect.Height)

        Dim g1 As Graphics = Graphics.FromImage(screenGrab)
        g1.CopyFromScreen(rect.Location, New Point(0, 0), rect.Size)
        Return screenGrab
    End Function

    ''' <summary>
    ''' Display screenShot on Form
    ''' </summary>
    ''' <param name="screenShot"></param>
    Private Sub Put_g_OnForm(ByVal screenShot As Image)
        Me.BackgroundImage = screenShot  ' which similar to _
        'g.DrawImage(screenShot, New Point(0, 0))

    End Sub

    'initiate Form1's properties
    Private Sub InitWindow()
        'https://stackoverflow.com/questions/14554186/run-in-full-screen-with-no-start-menu
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.Location = New Point(0, 0)
        Me.Size = SystemInformation.PrimaryMonitorSize
        Me.TopMost = True
        Me.Opacity = 1
        Me.BackColor = Color.Gray
    End Sub

    'initiate config through ".ini" file
    Private Sub InitIniFile()
        Dim iniFile As New IniFile(iniPath)
        Settings.Mode = CType(Int(iniFile.ReadIni(Section:="Default", Key:="Mode", DefaultValue:="0")), Mode)
        Settings.A9T9.Lang = CType(Int(iniFile.ReadIni(Section:="A9T9", Key:="Language", DefaultValue:=Str(Language.eng))), Language)
        Settings.A9T9.Apikey = iniFile.ReadIni(Section:="A9T9", Key:="API_Key", DefaultValue:="helloworld")
        Settings.A9T9.TimeOut = iniFile.ReadIni(Section:="A9T9", Key:="TimeOut", DefaultValue:="5")
        Settings.Sogou.TimeOut = iniFile.ReadIni(Section:="Sogou", Key:="TimeOut", DefaultValue:="5")
        Settings.SauceNAO.Timeout = iniFile.ReadIni(Section:="SauceNAO", Key:="Timeout", DefaultValue:="5")

        Settings.Hotkeys.ScreenCapture.KeyValue = CType(Int(iniFile.ReadIni(Section:="HotKey", Key:="ScreenCapture_KeyValue", DefaultValue:=Keys.F4)), Keys)
        Settings.Hotkeys.ScreenCapture.KeyModifier = CType(Int(iniFile.ReadIni(Section:="HotKey", Key:="ScreenCapture_KeyModifier", DefaultValue:=Hotkey.KeyModifier.None)), Hotkey.KeyModifier)
        InitRegisterHotkey()

        Settings.Advance.EraseAllNewlines = CType(iniFile.ReadIni(Section:="Advance", Key:="EraseAllNewlines", DefaultValue:="1"), Boolean)
    End Sub

    Public Sub FinalizingIniFile()
        Dim iniFile As New IniFile(iniPath)
        iniFile.WriteIni(Section:="Default", Key:="Mode", Value:=Settings.Mode)
        iniFile.WriteIni(Section:="A9T9", Key:="Language", Value:=Settings.A9T9.Lang)
        iniFile.WriteIni(Section:="A9T9", Key:="API_Key", Value:=Settings.A9T9.Apikey)
        iniFile.WriteIni(Section:="A9T9", Key:="TimeOut", Value:=Settings.A9T9.TimeOut)
        iniFile.WriteIni(Section:="Sogou", Key:="TimeOut", Value:=Settings.Sogou.TimeOut)
        iniFile.WriteIni(Section:="SauceNAO", Key:="Timeout", Value:=Settings.SauceNAO.Timeout)

        iniFile.WriteIni(Section:="HotKey", Key:="ScreenCapture_KeyValue", Value:=Settings.Hotkeys.ScreenCapture.KeyValue)
        iniFile.WriteIni(Section:="HotKey", Key:="ScreenCapture_KeyModifier", Value:=Settings.Hotkeys.ScreenCapture.KeyModifier)

        iniFile.WriteIni(Section:="Advance", Key:="EraseAllNewlines", Value:=Settings.Advance.EraseAllNewlines)
    End Sub

    Public Sub InitRegisterHotkey()
        Hotkey.registerHotkey(Me, Settings.Hotkeys.ScreenCapture.HotkeyId, Settings.Hotkeys.ScreenCapture.KeyModifier, Settings.Hotkeys.ScreenCapture.KeyValue)
    End Sub

    Public Sub FinalizingRegisterHotkey()
        Hotkey.unregisterHotkeys(Me)
    End Sub
    '--=====-- End Functions --=====--
End Class
