' Makes the window a click-through-able hover-window
Public Class ClickThruWindow
    'about syntax of hex expression https://codeday.me/bug/20180418/155059.html
    Private Const WS_EX_TRANSPARENT As Integer = &H20  ' i.e. 32

    Private Const WS_EX_LAYERED As Integer = &H80000

    Private Const GWL_EXSTYLE As Integer = -20

    Private Declare Function GetWindowLong Lib "user32.dll" Alias "GetWindowLongA" (ByVal hwnd As IntPtr, ByVal index As Integer) As Integer

    Private Declare Function SetWindowLong Lib "user32.dll" Alias "SetWindowLongA" (ByVal hwnd As IntPtr, ByVal index As Integer, ByVal newStyle As Integer) As Integer

    Public Shared Sub SetWindowExTransparent(ByVal hwnd As IntPtr)
        Dim extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE)
        SetWindowLong(hwnd, GWL_EXSTYLE, (extendedStyle Or WS_EX_TRANSPARENT Or WS_EX_LAYERED))
    End Sub

    Public Shared Sub SetWindowExReversed(ByVal hwnd As IntPtr)
        Dim extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE)
        SetWindowLong(hwnd, GWL_EXSTYLE, (extendedStyle And Not WS_EX_LAYERED And Not WS_EX_TRANSPARENT))
    End Sub
End Class
