Imports System.Net.Http
Imports System.Text.RegularExpressions

Public Class Form1
    Private WithEvents kbHook As New KeyboardHook
    Private IsKeyUp As Boolean
    Private _paintEvent_graphics As Graphics
#If DEBUG Then
    Private g As Graphics  ' pointer-like Graphics on Form1. for test only
#End If
    Private _startPoint As Point  ' regional capture rectangle's starting point
    Private IsMouseDown As Boolean = False
    Private Const iniPath As String = "./config.ini"
    Public _lang As String  ' detective language
    Public _apikey As String
    Private _timeCounter As Short  ' counts the time consumption of HTTP response

    Private Enum Language
        ara = 0
        chs
        cht
        cze
        dan
        dut
        eng
        fin
        fre 
        ger
        gre
        hun
        jap
        kor
        nor
        pol
        por
        spa
        swe
        tur
    End Enum

    Private Enum Mode
        A9T9 = 0
        Sogou
    End Enum

    Private Structure Settings
        Property Mode As Mode
        Public Structure A9T9
            Property Apikey As String
            Property Lang As Language
        End Structure
    End Structure
    Private _settings As Settings

    '===== Reference:= https://social.msdn.microsoft.com/Forums/windows/en-US/5dc1b32b-7b7e-41fe-af87-d491d7021bd3/vbnet-smooth-rectangle-drawing-using-mousedrag?forum=winforms
    Dim _mRect As Rectangle

    '--=====-- Overrides --=====--
    Public Sub New()
        InitializeComponent()
        Me.DoubleBuffered = True
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        IsMouseDown = True
        Label1.Text = "MouseDown"
        '_mRect = New Rectangle(e.X, e.Y, 0, 0)
        _startPoint = New Size(e.X, e.Y)
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            '_mRect = New Rectangle(_startPoint.X, _startPoint.Y, e.X - _startPoint.X, e.Y - _startPoint.Y)
            _mRect = New Rectangle(Math.Min(_startPoint.X, e.X), Math.Min(_startPoint.Y, e.Y),
                                  Math.Max(_startPoint.X, e.X) - Math.Min(_startPoint.X, e.X),
                                  Math.Max(_startPoint.Y, e.Y) - Math.Min(_startPoint.Y, e.Y))
            Me.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Using pen As New Pen(Color.Red, 3)
            e.Graphics.DrawRectangle(pen, _mRect)
            _paintEvent_graphics = e.Graphics  ' ready to dispose
        End Using
    End Sub
    '===== End Reference
    '--=====-- End Overrides --=====--

    '--=====-- Initiation --=====--
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'InitWindow()  ' Moved to Timer1 tick event
        IsKeyUp = True
        Timer1.Enabled = True
    End Sub

    ''' <summary>
    ''' Hides Form1 at startup. The feature does NOT effect in Frm.Load Event.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Me.Hide()

        ''For test
        'TakeScreenShot()

        InitWindow()
        InitIniFile()

    End Sub
    '--=====-- End Initiation --=====--

    '--=====-- Finalization --=====--
    Public Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing  ' BUG: NOT WORKING
        'e.Cancel = True
        FinalizingIniFile()
    End Sub
    '--=====-- End Finalization --=====--

    '--=====-- Events --=====--
    Private Sub kbHook_KeyDown(ByVal Key As System.Windows.Forms.Keys) Handles kbHook.KeyDown
        Debug.WriteLine(Key.ToString)
        If Key = Keys.F4 And IsKeyUp Then
            IsKeyUp = False
            If Me.Visible = False Then
                Dim screenShot As Bitmap = TakeScreenShot()
                'InitWindow()  ' unnecessary
#If DEBUG Then
                'for debug
                g = Me.CreateGraphics()  ' moved to Me.BackgroundImage
#End If
                lbl_lang.Text = "Language: " & _lang
                Put_g_OnForm(screenShot)
                Me.Show()

            Else  ' stop grapping regional screenshot mannually
                FinishingFrm()
            End If
        End If
    End Sub

    Private Sub kbHook_KeyUp(ByVal Key As System.Windows.Forms.Keys) Handles kbHook.KeyUp
        Debug.WriteLine(Key)
        If Key = Keys.F4 Then
            IsKeyUp = True
        End If
    End Sub

    Private Sub Frm_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        If IsMouseDown = True Then
#If DEBUG Then
            Me.Hide()  ' for debug to see the output of debug message
#End If
            IsMouseDown = False
            Label1.Text = "MouseUp"
            'Dim toPoint As Point = e.Location
            If _mRect <> Nothing And _mRect.Size.Width <> 0 And _mRect.Size.Height <> 0 Then
                Dim capturedScreen As Bitmap = TakeRegionalScreenShot(_mRect)
#If DEBUG Then
                g.DrawImage(capturedScreen, 1, 1)
#End If
#If Not DEBUG Then
                HttpRequests.A9T9_OCR(capturedScreen, _apikey, _lang)
#End If
#If DEBUG Then
                HttpRequests.A9T9_OCR(capturedScreen, _apikey, _lang)
                'HttpRequests.Sogou_OCR(capturedScreen)
#End If
            End If
#If Not DEBUG Then
            FinishingFrm()
#End If
        End If
    End Sub

    Private Sub tray_MouseClick(sender As Object, e As MouseEventArgs) Handles tray.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then 'Checks if the pressed button is the Right Mouse
            trayform.Show() 'Shows the Form that is the parent of "traymenu"
            trayform.Activate() 'Set the Form to "Active", that means that that will be the "selected" window
            trayform.Width = 1 'Set the Form width to 1 pixel, that is needed because later we will set it behind the "traymenu"
            trayform.Height = 1 'Set the Form Height to 1 pixel, for the same reason as above
        End If
    End Sub

    '--=====-- End Events --=====--

    '--=====-- Functions --=====--
    'When closing/hiding the Window
    Private Sub FinishingFrm()
        Me.Hide()
        Me.BackgroundImage.Dispose()  ' privacy protection and ready to release RAM.
        _paintEvent_graphics.Dispose()

        ' erase the previous rectangle
        _mRect = Nothing
        Me.Invalidate()

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
        Me.BackgroundImage = screenShot
        'which similar to
        'g.DrawImage(screenShot, New Point(0, 0))


    End Sub

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
        _lang = iniFile.ReadIni(Section:="Basic config", Key:="Language", DefaultValue:="eng")
        _apikey = iniFile.ReadIni(Section:="Basic config", Key:="API_Key", DefaultValue:="helloworld")
    End Sub

    Public Sub FinalizingIniFile()
        Dim iniFile As New IniFile(iniPath)
        iniFile.WriteIni(Section:="Basic config", Key:="Language", Value:=_lang)
        iniFile.WriteIni(Section:="Basic config", Key:="API_Key", Value:=_apikey)
    End Sub
    '--=====-- End Functions --=====--
End Class
