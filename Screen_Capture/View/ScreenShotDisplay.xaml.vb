Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation

Namespace View
    Public Class ScreenShotDisplay
        Private IsMouseDown As Boolean
        Private _startPoint As Point
        Private _mRect As System.Windows.Shapes.Rectangle
        Private _controller As Controller.Controller
        Private _config As Model.Configuration

        Private _fullScreenshot As System.IO.MemoryStream

        Public Shared ReadOnly Property Num_Instance As Integer
            Get
                Return _instanceList.Count
            End Get
        End Property

        Private Shared _instanceList As List(Of ScreenShotDisplay) = New List(Of ScreenShotDisplay)

        Public Sub New(controller As Controller.Controller)
            _instanceList.Add(Me)

            ' 此调用是设计器所必需的。
            InitializeComponent()

            ' 在 InitializeComponent() 调用之后添加任何初始化。
            _controller = controller
            _config = _controller.GetConfigCopy()
            Me.WindowStyle = WindowStyle.None
            Me.WindowState = WindowState.Maximized
#If Not DEBUG Then
            Me.Topmost = True
#End If
            _mRect = New System.Windows.Shapes.Rectangle()
            _mRect.Fill = Nothing
            _mRect.Stroke = Brushes.Red
            canvas_drawBoard.Children.Add(_mRect)
        End Sub
        Public Sub SetDisplayingImage(img As Bitmap)
            Dim _fullScreenshot As System.IO.MemoryStream = New System.IO.MemoryStream()
            img.Save(_fullScreenshot, System.Drawing.Imaging.ImageFormat.Bmp)
            _fullScreenshot.Position = 0

            Dim imageControl As New System.Windows.Controls.Image()

            Dim imageSource As New System.Windows.Media.Imaging.BitmapImage()
            imageSource.BeginInit()
            imageSource.StreamSource = _fullScreenshot
            imageSource.EndInit()

            imageControl.Source = imageSource

            Me.canvas_screenShot.Children.Add(imageControl)
        End Sub

        Public Shared Sub Destroy()
            For Each window In _instanceList
                window.HideWindow()
            Next
            _instanceList.Clear()
        End Sub

        Private Sub ScreenShotDisplay_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseDown
            If e.LeftButton = MouseButtonState.Pressed Then
                IsMouseDown = True
                _startPoint = e.GetPosition(canvas_screenShot)
            End If
        End Sub
        Private Sub ScreenShotDisplay_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
            'https://stackoverflow.com/questions/17839869/run-when-when-you-ctrl-click-a-button-in-a-winform
            If e.LeftButton = MouseButtonState.Pressed And Not My.Computer.Keyboard.CtrlKeyDown Then  ' preserve the previous rectangle when ctrl is pressed
                Dim mousePos As Point = e.GetPosition(canvas_screenShot)
                _mRect.Width = Math.Max(_startPoint.X, mousePos.X) - Math.Min(_startPoint.X, mousePos.X) + 3
                _mRect.Height = Math.Max(_startPoint.Y, mousePos.Y) - Math.Min(_startPoint.Y, mousePos.Y) + 3
                System.Windows.Controls.Canvas.SetLeft(_mRect, Math.Min(_startPoint.X, mousePos.X) - 1)
                System.Windows.Controls.Canvas.SetTop(_mRect, Math.Min(_startPoint.Y, mousePos.Y) - 1)
            End If
        End Sub
        Private Sub ScreenShotDisplay_MouseUp(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseUp
            If IsMouseDown Then
                IsMouseDown = False
                Dim partialCap As Bitmap = Controller.ScreenCap.CaptureScreen(_startPoint, e.GetPosition(canvas_screenShot))
                Upload(partialCap)
            Else
                HideWindow()
                Exit Sub
            End If
        End Sub

        Private Sub Upload(image As Bitmap)
            If image IsNot Nothing Then
                image = ResizeImg(image)
                Controller.HttpRequests.AutoDirectOCR(image)

                HideWindow()
            End If
        End Sub

        Private Sub HideWindow()
            If Me._fullScreenshot IsNot Nothing Then
                Me._fullScreenshot.Dispose()
            End If
            Me.canvas_screenShot.Children.Clear()
            Me.Close()
            'Me.Hide()
        End Sub

        Private Function ResizeImg(img As Bitmap) As Bitmap
            'For Sogou API only: either width or height must be larger than or equal to 50
            'https://stackoverflow.com/questions/2144592/resizing-images-in-vb-net
            If _config.Mode = Model.Configuration.ProcessMode.Sogou And (_mRect.Width < 50 Or _mRect.Height < 50) Then
                Dim newBitmap As New Bitmap(CInt(Math.Max(_mRect.Width, 50)), CInt(Math.Max(_mRect.Height, 50)))  'BUG:  from area too small still identify nothing
                Dim drawMannual As Graphics = Graphics.FromImage(newBitmap)
                drawMannual.Clear(System.Drawing.Color.White)
                drawMannual.DrawImage(img, 0, 0)
                img.Dispose()
                img = newBitmap
                drawMannual.Dispose()
            End If
            Return img
        End Function
    End Class
End Namespace
