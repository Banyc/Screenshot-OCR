Imports System
Imports System.Drawing  ' using a win32 lib

Namespace Controller
    Public Class ScreenCap
        ' returns a full screen image
        Public Shared Function CaptureScreen() As Bitmap
            Return CaptureScreen(New Point(0, 0), New Point(System.Windows.SystemParameters.PrimaryScreenWidth, System.Windows.SystemParameters.PrimaryScreenHeight))
        End Function

        Public Shared Function CaptureScreen(point1 As System.Windows.Point, point2 As System.Windows.Point) As Bitmap
            Return CaptureScreen(New Point(point1.X, point1.Y), New Point(point2.X, point2.Y))
        End Function

        Public Shared Function CaptureScreen(point1 As Point, point2 As Point) As Bitmap
            Dim factor As Double = GetScalingFactor()

            Dim width As Integer = Math.Abs(point1.X - point2.X) * factor
            Dim height As Integer = Math.Abs(point1.Y - point2.Y) * factor
            Dim top As Integer = Math.Min(point1.Y, point2.Y) * factor
            Dim left As Integer = Math.Min(point1.X, point2.X) * factor

            If width * height <> 0 Then
                Dim captureSize As Size = New Size(width, height)
                Dim screenShot As New Bitmap(captureSize.Width, captureSize.Height)  ' pointer-like
                Dim g1 As Graphics = Graphics.FromImage(screenShot)
                g1.CopyFromScreen(New Point(left, top), New Point(0, 0), captureSize)
                g1.Dispose()
                Return screenShot
            Else
                Return Nothing
            End If
        End Function

        Private Shared Function GetScalingFactor() As Double
            Dim realSolution As Double = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width
            Dim fakeSolution = System.Windows.SystemParameters.PrimaryScreenWidth
            Return realSolution / fakeSolution
        End Function
    End Class

End Namespace
