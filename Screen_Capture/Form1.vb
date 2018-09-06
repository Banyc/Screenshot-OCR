Imports System.Net.Http
Imports System.Text.RegularExpressions

Public Class Form1
    Private WithEvents kbHook As New KeyboardHook
    Private IsKeyUp As Boolean
#If DEBUG Then
    Private g As Graphics  ' pointer-like Graphics on Form1. for test only
#End If
    Private paintEvent_graphics As Graphics  ' reason unknown
    Private _startPoint As Point  ' regional capture rectangle's starting point
    Private IsMouseDown As Boolean = False
    Private Const iniPath As String = "./config.ini"
    Public _lang As String  ' detective language
    Public _apikey As String
    Private _timeCounter As Short  ' counts the time consumption of HTTP response

    '===== Reference:= https://social.msdn.microsoft.com/Forums/windows/en-US/5dc1b32b-7b7e-41fe-af87-d491d7021bd3/vbnet-smooth-rectangle-drawing-using-mousedrag?forum=winforms
    Dim mRect As Rectangle

    '--=====-- Overrides --=====--
    Public Sub New()
        InitializeComponent()
        Me.DoubleBuffered = True
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        IsMouseDown = True
        Label1.Text = "MouseDown"
        'mRect = New Rectangle(e.X, e.Y, 0, 0)
        _startPoint = New Size(e.X, e.Y)
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            'mRect = New Rectangle(_startPoint.X, _startPoint.Y, e.X - _startPoint.X, e.Y - _startPoint.Y)
            mRect = New Rectangle(Math.Min(_startPoint.X, e.X), Math.Min(_startPoint.Y, e.Y),
                                  Math.Max(_startPoint.X, e.X) - Math.Min(_startPoint.X, e.X),
                                  Math.Max(_startPoint.Y, e.Y) - Math.Min(_startPoint.Y, e.Y))
            Me.Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Using pen As New Pen(Color.Red, 3)
            e.Graphics.DrawRectangle(pen, mRect)
            paintEvent_graphics = e.Graphics
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



    ''=====
    'Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
    '    IsMouseDown = True
    '    Label1.Text = "Panel1MouseDown"
    '    fromPoint = e.Location
    'End Sub
    'Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
    '    If IsMouseDown Then
    '        g = Graphics.FromImage(Me.BackgroundImage)
    '        'Panel1.Visible = False
    '        'Panel1.Refresh()
    '        'Panel1.Visible = True
    '        'g.Clear(Color.FromArgb(0, 0, 0, 0))
    '        g.DrawRectangle(Pens.Red, fromPoint.X, fromPoint.Y, e.X - fromPoint.X, e.Y - fromPoint.Y)  ' BUG: cannot erase the previous rectangle

    '    End If
    'End Sub

    'Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
    '    If IsMouseDown = True Then
    '        IsMouseDown = False
    '        'MessageBox.Show("Checkpoint.1")
    '        Label1.Text = "MouseUp"
    '        Dim toPoint As Point = e.Location
    '        Dim capturedScreen As Bitmap = TakeRegionalScreenShot(fromPoint, toPoint)
    '        g.DrawImage(capturedScreen, 1, 1)
    '        GetContextFrom(capturedScreen)
    '    End If
    'End Sub
    ''=====
    '=====
    ''Aborted
    'Private Sub Frm_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
    '    IsMouseDown = True
    '    Label1.Text = "MouseDown"
    '    'fromPoint = e.Location  ‘ see Overrided OnMouseDown
    'End Sub

    'Private Sub Frm_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
    '    If IsMouseDown Then
    '        g.DrawRectangle(Pens.Red, fromPoint.X, fromPoint.Y, e.X - fromPoint.X, e.Y - fromPoint.Y)  ' BUG: cannot erase the previous rectangle
    '        'PictureBox1.
    '    End If
    'End Sub

    Private Sub Frm_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        If IsMouseDown = True Then
#If DEBUG Then
            Me.Hide()  ' for debug to see the output of debug message
#End If
            IsMouseDown = False
            Label1.Text = "MouseUp"
            'Dim toPoint As Point = e.Location
            If mRect <> Nothing And mRect.Size.Width <> 0 And mRect.Size.Height <> 0 Then
                Dim capturedScreen As Bitmap = TakeRegionalScreenShot(mRect)
#If DEBUG Then
                g.DrawImage(capturedScreen, 1, 1)
#End If

                GetContextFrom(capturedScreen)
            End If
#If Not DEBUG Then
            FinishingFrm()
#End If
        End If
    End Sub
    '=====

    Private Sub tray_MouseClick(sender As Object, e As MouseEventArgs) Handles tray.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then 'Checks if the pressed button is the Right Mouse
            trayform.Show() 'Shows the Form that is the parent of "traymenu"
            trayform.Activate() 'Set the Form to "Active", that means that that will be the "selected" window
            trayform.Width = 1 'Set the Form width to 1 pixel, that is needed because later we will set it behind the "traymenu"
            trayform.Height = 1 'Set the Form Height to 1 pixel, for the same reason as above
        End If
    End Sub

    ' counts time consumption of HTTP response
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        _timeCounter += 1
        tray.Text = "Processing by " & Str(_timeCounter) & "s"
    End Sub
    '--=====-- End Events --=====--

    '--=====-- Functions --=====--
    'When closing/hiding the Window
    Private Sub FinishingFrm()
        ' comment out the lines below for debug
        Me.Hide()
        Me.BackColor = Color.Gray  ' privacy protection
        ' erase the previous rectangle
        mRect = Nothing
        Me.Invalidate()
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

    '' Aborted
    'Private Function TakeRegionalScreenShot(fromPoint As Point, toPoint As Point) As Bitmap
    '    Dim captureSize As Size = New Size(Math.Abs(toPoint.X - fromPoint.X), Math.Abs(toPoint.Y - fromPoint.Y))
    '    Dim screenGrab As New Bitmap(Math.Abs(toPoint.X - fromPoint.X), Math.Abs(toPoint.Y - fromPoint.Y))

    '    Dim g1 As Graphics = Graphics.FromImage(screenGrab)
    '    g1.CopyFromScreen(fromPoint, New Point(0, 0), captureSize)
    '    Return screenGrab
    'End Function

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

        'Panel1.Left = 0
        'Panel1.Top = 0
        'Panel1.Size = Me.Size
        'Panel1.BackColor = Color.FromArgb(0, 0, 0, 0)
        'g = Panel1.CreateGraphics()
        'Panel1.Visible = False


        'PictureBox1.BackColor = Color.Gray
        'Panel1.BackColor = Color.FromArgb(200, 100, 100, 100)
        'TransparencyKey = PictureBox1.BackColor

        'https://stackoverflow.com/questions/21798859/make-the-forms-background-to-transparent
        'Me.TransparencyKey = Me.BackColor
        'Me.BackColor = Color.Black
        'Me.BackColor = Color.FromArgb(0, 0, 0, 0)
        'Me.Opacity = 0.25

    End Sub

    'Upload screenshot and receive OCR result
    Private Async Function GetContextFrom(image As Bitmap) As Threading.Tasks.Task
        _timeCounter = 0  ' reset
        Timer2.Enabled = True
        Try
            Dim httpClient As HttpClient = New HttpClient()
            httpClient.Timeout = New TimeSpan(1, 1, 1)
            Dim form As MultipartFormDataContent = New MultipartFormDataContent()
            form.Add(New StringContent(_apikey), "apikey")
            'Dim cmbLanguage As String = "chs"
            Dim cmbLanguage As String = _lang
            form.Add(New StringContent(cmbLanguage), "language")

            Dim imageData As Byte() = ConvertToByteArray(image)
            form.Add(New ByteArrayContent(imageData, 0, imageData.Length), "image", "image.jpg")

#If DEBUG Then
            Label1.Text = "Processing"  ' BUG: occasionally stuck here
#End If
            'tray.Text = "Processing"
            Dim response As HttpResponseMessage = Await httpClient.PostAsync("https://api.ocr.space/Parse/Image", form)
#If DEBUG Then
            Label1.Text = "Response Received"
#End If
            tray.Text = "Response Received"
            Dim strContent As String = Await response.Content.ReadAsStringAsync()
#If DEBUG Then
            Label1.Text = "Finished"
#End If
            Timer2.Enabled = False

            'Dim ocrResult As Rootobject = JsonConvert.DeserializeObject(Of Rootobject)(strContent)

            'If ocrResult.OCRExitCode = 1 Then

            '    For i As Integer = 0 To ocrResult.ParsedResults.Count() - 1
            '        txtResult.Text = txtResult.Text + ocrResult.ParsedResults(i).ParsedText
            '    Next
            'Else
            '    MessageBox.Show("ERROR: " & strContent)
            'End If

            'TextBox1.Text = strContent  ' for debug
            Debug.WriteLine(strContent)

            Dim parsedText As String = GetParsedText(strContent)
            Clipboard.Clear()
            Clipboard.SetText(parsedText)
            If MessageBox.Show(parsedText, "", MessageBoxButtons.OKCancel) = DialogResult.OK Then Clipboard.SetText(parsedText)  ' BUG: does not pop up. fix: wait. Cause: bad response

        Catch exception As Exception
            'Me.Hide()  ' for debug
            MessageBox.Show("Ooops" & vbCrLf & exception.Message)
        End Try
        Timer2.Enabled = False
        tray.Text = "Screenshot OCR"
    End Function

    ''' <summary>
    ''' convert Bitmap to Byte()
    ''' https://dotnettips.wordpress.com/2007/12/16/convert-bitmap-to-byte-array/
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Shared Function ConvertToByteArray(ByVal value As Bitmap) As Byte()
        Dim bitmapBytes As Byte()
        Using stream As New System.IO.MemoryStream()
            'value.Save(stream, value.RawFormat)
            value.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg)
            bitmapBytes = stream.ToArray()
        End Using
        Return bitmapBytes
    End Function

    'analyses response documents and returns useful text
    Private Function GetParsedText(ByVal content As String) As String
        'Dim parsedText As String
        'Dim startIndex As Short = content.IndexOf("ParsedText") + "ParsedText".Length + 3  ' ":"
        'Dim endIndex As Short = content.IndexOf(Chr(34), startIndex)
        'parsedText = content.Substring(startIndex, endIndex - startIndex).Replace("\r\n", vbCrLf)
        'Return parsedText

        ' (?<="ParsedText"\s*:\s*").+?(?=(?<!\\)")
        Dim pattern As String = "(?<=" & Chr(34) & "ParsedText" & Chr(34) & "\s*:\s*" & Chr(34) & ").+?(?=(?<!\\)" & Chr(34) & ")"
        Dim match = Regex.Match(content, pattern)
        If match.Success Then
            Return match.Value.Replace("\r\n", vbCrLf).Replace("\" & Chr(34), Chr(34))
        Else
            ' https://stackoverflow.com/questions/13151322/how-to-raise-an-exception-in-vb-net
            Throw New System.Exception("Did not match")
        End If
    End Function

    'initiate config through ".ini" file
    Private Sub InitIniFile()
        Dim iniFile As New IniFile(iniPath)
        'If Not iniFile.ExistINIFile() Then
        '    'Dim fileWriter = System.IO.File.CreateText(iniPath)
        '    iniFile.WriteIni(Section:="Basic config", Key:="Language", Value:="eng")
        '    iniFile.WriteIni(Section:="Basic config", Key:="API_Key", Value:="helloworld")
        'End If
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
