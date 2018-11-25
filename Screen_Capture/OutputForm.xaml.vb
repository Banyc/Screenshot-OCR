Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation

Public Class OutputForm
    Private _content As String

    Public Sub New(content As String)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        _content = content
        tbOutput.Text = _content
    End Sub

    Private Sub OutputForm_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ResizeWindow()
    End Sub

    'set the _content to clipboard once again
#Region "Mouse Left Button Events"
    Private Sub Card_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles Card.MouseLeftButtonDown
        MouseLeftButtinDownHandling(e)
    End Sub
    Private Sub tbOutput_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles tbOutput.MouseLeftButtonDown
        MouseLeftButtinDownHandling(e)
    End Sub

    Private Sub MouseLeftButtinDownHandling(e As MouseButtonEventArgs)
        Try
            If e.LeftButton = MouseButtonState.Pressed Then
                Me.DragMove()  ' BUG in some desktop
            End If
        Catch
        End Try
        If e.ClickCount = 2 Then  'https://social.msdn.microsoft.com/Forums/vstudio/en-US/83ac6fbd-af42-4b9c-897e-142abb0a8199/can-not-use-event-double-click-on-button?forum=vbgeneral
            Clipboard.Clear()
            Clipboard.SetText(_content)
            Me.Close()
        End If
        e.Handled = True
    End Sub
#End Region

    Private Sub OutputForm_MouseRightButtonUp(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseRightButtonUp
        Me.Close()
    End Sub

    ' resizes the dimension of MyBase and locates it to the bottom right
    Private Sub ResizeWindow()
        UpdateLayout()
        If tbOutput.ActualWidth > System.Windows.SystemParameters.WorkArea.Width / 3 Then
            tbOutput.Width = System.Windows.SystemParameters.WorkArea.Width / 3
            tbOutput.TextWrapping = TextWrapping.Wrap
        End If
        UpdateLayout()
        Me.Width = tbOutput.ActualWidth + tbOutput.Margin.Left + tbOutput.Margin.Right + gdMain.Margin.Left + gdMain.Margin.Right + 1
        Me.Height = tbOutput.ActualHeight + tbOutput.Margin.Top + tbOutput.Margin.Bottom + gdMain.Margin.Top + gdMain.Margin.Bottom + 1
        BottomRightForm()
    End Sub

    'Locate Window to the bottom right of the screen
    Private Sub BottomRightForm()
        UpdateLayout()
        Dim screenWidth As Double = System.Windows.SystemParameters.WorkArea.Width
        Dim screenHeight As Double = System.Windows.SystemParameters.WorkArea.Height
        Dim windowWidth As Double = Me.ActualWidth
        Dim windowHeight As Double = Me.ActualHeight
        Me.Left = screenWidth - windowWidth
        Me.Top = screenHeight - windowHeight
    End Sub

    'Change border color When mouse enters the form
    Private Sub Card_MouseEnter(sender As Object, e As MouseEventArgs) Handles Card.MouseEnter
        Card.Stroke = Media.Brushes.CornflowerBlue
        Card.StrokeThickness = 2
    End Sub
    Private Sub tbOutput_MouseEnter(sender As Object, e As MouseEventArgs) Handles tbOutput.MouseEnter
        Call Card_MouseEnter(sender, e)
    End Sub

    '' TODO: Fix BUGS
    '' change font size when scrolling in side Card
    '' https://stackoverflow.com/questions/2378296/mousewheel-determining-up-and-down-scrolling-events
    'Private Sub Card_MouseWheel(sender As Object, e As MouseWheelEventArgs) Handles Card.MouseWheel
    '    If e.Delta > 0 Then
    '        tbOutput.FontSize += 3
    '    Else
    '        tbOutput.FontSize -= 3
    '    End If
    'End Sub
    'Private Sub tbOutput_MouseWheel(sender As Object, e As MouseWheelEventArgs) Handles tbOutput.MouseWheel
    '    Call Card_MouseWheel(sender, e)
    'End Sub

    'Change border color when mouse leaves the form
    Private Sub Card_MouseLeave(sender As Object, e As MouseEventArgs) Handles Card.MouseLeave
        Card.StrokeThickness = 0
        e.Handled = True
    End Sub
    Private Sub tbOutput_MouseLeave(sender As Object, e As MouseEventArgs) Handles tbOutput.MouseLeave
        e.Handled = True
        Call Card_MouseLeave(sender, e)
    End Sub

    '=== https://stackoverflow.com/questions/5958508/fading-out-a-window

    Private AlreadyFaded As Boolean = False

    Private Sub window_Closing(ByVal sender As Object, ByVal e As CancelEventArgs) Handles Me.Closing
        If Not AlreadyFaded Then
            AlreadyFaded = True
            e.Cancel = True
            Dim anim = New DoubleAnimation(0, CType(TimeSpan.FromSeconds(0.2), Duration))
            AddHandler anim.Completed, New EventHandler(AddressOf anim_Completed)
            Me.BeginAnimation(UIElement.OpacityProperty, anim)
        End If
        Card = Nothing
        tbOutput = Nothing
        MyForm = Nothing
    End Sub

    Private Sub anim_Completed(ByVal sender As Object, ByVal e As EventArgs)
        Close()
    End Sub
    '=== end reference
End Class
