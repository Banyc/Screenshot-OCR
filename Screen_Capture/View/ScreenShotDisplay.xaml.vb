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

        Public Sub New(controller As Controller.Controller)
            InitializeComponent()
            _controller = controller
            _config = _controller.GetConfigCopy()
            Me.WindowStyle = WindowStyle.None
            Me.AllowsTransparency = True
            Me.ResizeMode = ResizeMode.NoResize  ' <https://stackoverflow.com/a/28413414>
            'Me.WindowState = WindowState.Maximized
            ' alternative to `Me.WindowState = WindowState.Maximized`. But oversized
            Me.Left = 0
            Me.Top = 0
            Me.Width = Windows.Forms.Screen.PrimaryScreen.Bounds.Width
            Me.Height = Windows.Forms.Screen.PrimaryScreen.Bounds.Height

            Me.ShowInTaskbar = False
            Me.Focusable = False
            ' Me cannot be focused when `Show()` is called only
            Me.ShowActivated = False
            '#If Not DEBUG Then
            Me.Topmost = True
            '#End If
            _mRect = New System.Windows.Shapes.Rectangle()
            _mRect.Fill = Nothing
            _mRect.Stroke = Brushes.Red
            canvas_drawBoard.Children.Add(_mRect)
            Me.MyHide()
            Me.Show()
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

            'Me.canvas_screenShot.Children.Add(imageControl)
            Dim imageBruch As ImageBrush = New ImageBrush(imageSource)
            ' in case the maximized window is oversized
            imageBruch.Stretch = Stretch.None
            imageBruch.AlignmentX = AlignmentX.Left
            imageBruch.AlignmentY = AlignmentY.Top

            Me.Background = imageBruch

        End Sub

        Private Sub ScreenShotDisplay_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseDown
            If e.LeftButton = MouseButtonState.Pressed Then
                IsMouseDown = True
                If Not Keyboard.IsKeyDown(Key.LeftCtrl) Then
                    _startPoint = e.GetPosition(Me)
                End If
            End If
        End Sub
        Private Sub ScreenShotDisplay_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
            'https://stackoverflow.com/questions/17839869/run-when-when-you-ctrl-click-a-button-in-a-winform
            If e.LeftButton = MouseButtonState.Pressed And Not Keyboard.IsKeyDown(Key.LeftCtrl) Then  ' preserve the previous rectangle when ctrl is pressed
                Dim mousePos As Point = e.GetPosition(Me)
                _mRect.Width = Math.Max(_startPoint.X, mousePos.X) - Math.Min(_startPoint.X, mousePos.X) + 2
                _mRect.Height = Math.Max(_startPoint.Y, mousePos.Y) - Math.Min(_startPoint.Y, mousePos.Y) + 2
                System.Windows.Controls.Canvas.SetLeft(_mRect, Math.Min(_startPoint.X, mousePos.X) - 1)
                System.Windows.Controls.Canvas.SetTop(_mRect, Math.Min(_startPoint.Y, mousePos.Y) - 1)
            End If
        End Sub
        Private Sub ScreenShotDisplay_MouseUp(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseUp
            If IsMouseDown Then
                IsMouseDown = False
                Dim point1 As Point = New Point(Windows.Controls.Canvas.GetLeft(_mRect), Windows.Controls.Canvas.GetTop(_mRect))
                Dim point2 As Point = New Point(Windows.Controls.Canvas.GetLeft(_mRect) + _mRect.ActualWidth, Windows.Controls.Canvas.GetTop(_mRect) + _mRect.ActualHeight)
#If DEBUG Then
                Me.Hide()
#End If
                Dim partialCap As Bitmap = Controller.ScreenCap.CaptureScreen(point1, point2)
#If DEBUG Then
                Me.Show()
#End If
                Upload_Dispose(partialCap)
            End If
            HideWindow()
        End Sub

        Private Sub Upload_Dispose(image As Bitmap)
            If image IsNot Nothing Then
                image = ResizeImg(image)
                Controller.HttpRequests.AutoDirectOCR(image)
                image.Dispose()
            End If
        End Sub

        Private Sub HideWindow()
            If Me._fullScreenshot IsNot Nothing Then
                Me._fullScreenshot.Dispose()
            End If
            Me.Background = Nothing
            GC.Collect()
            Me.MyHide()
        End Sub

        Private Function ResizeImg(img As Bitmap) As Bitmap
            'For Sogou API only: either width or height must be larger than or equal to 50
            'https://stackoverflow.com/questions/2144592/resizing-images-in-vb-net
            If _config.Mode = Model.Configuration.ProcessMode.Sogou And (img.Width < 50 Or img.Height < 50) Then
                Dim newBitmap As New Bitmap(Math.Max(img.Width, 50), Math.Max(img.Height, 50))  'BUG:  from area too small still identify nothing
                Dim drawMannual As Graphics = Graphics.FromImage(newBitmap)
                drawMannual.Clear(System.Drawing.Color.White)
                drawMannual.DrawImage(img, 0, 0)
                img.Dispose()
                img = newBitmap
                drawMannual.Dispose()
            End If
            Return img
        End Function

        Public Sub MyShow()
            Me.Topmost = True
            Me.Opacity = 1
        End Sub

        Public Sub MyHide()
            ' prevent Me from getting focused. Worked with `Me.ShowActivated = False`
            Me.Hide()
            Me.Show()

            Me.Opacity = 0
        End Sub

    End Class
End Namespace
