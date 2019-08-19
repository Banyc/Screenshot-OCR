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

#Region "Mouse Left Button Events"
    Private Sub Card_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles Card.MouseLeftButtonDown
        Try
            If e.LeftButton = MouseButtonState.Pressed Then
                Me.DragMove()  ' BUG in some desktop
            End If
        Catch
        End Try
        'set the _content to clipboard once again
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

#Region "Window Fades Out" ' https://stackoverflow.com/questions/5958508/fading-out-a-window

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
#End Region
End Class
