'https://gist.github.com/kirsbo/3b01a1412311e7a1d565
Public Class Hotkey

#Region "Declarations - WinAPI, Hotkey constant and Modifier Enum"
    ''' <summary>
    ''' Declaration of winAPI function wrappers. The winAPI functions are used to register / unregister a hotkey
    ''' </summary>
    Private Declare Function RegisterHotKey Lib "user32" _
    (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer

    Private Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer

    Public Const WM_HOTKEY As Integer = &H312

    Enum KeyModifier
        None = 0
        Alt = &H1
        Control = &H2
        Shift = &H4
        Winkey = &H8
    End Enum 'This enum is just to make it easier to call the registerHotKey function: The modifier integer codes are replaced by a friendly "Alt","Shift" etc.

    Private Shared Property _registeredIdCount As New List(Of Integer)  'Store all registered ids
#End Region


#Region "Hotkey registration, unregistration and handling"
    'Register hotkey by (String)trigger key
    Public Shared Sub registerHotkey(ByRef sourceForm As Form, ByVal registeredId As Integer, ByVal modifier As KeyModifier, ByVal triggerKey As String)
        registerHotkey(sourceForm, registeredId, modifier, Asc(triggerKey.ToUpper))
    End Sub

    'Register hotkey by (int)trigger key
    Public Shared Sub registerHotkey(ByRef sourceForm As Form, ByVal registeredId As Integer, ByVal modifier As KeyModifier, ByVal triggerKey As Integer)
        _registeredIdCount.Add(registeredId)
        RegisterHotKey(sourceForm.Handle, registeredId, modifier, triggerKey)
    End Sub
    Public Shared Sub unregisterHotkeys(ByRef sourceForm As Form, ByVal registeredId As Integer)
        UnregisterHotKey(sourceForm.Handle, registeredId)  'Remember to call unregisterHotkeys() when closing your application.
    End Sub

    Public Shared Sub unregisterHotkeys(ByRef sourceForm As Form)  'Unregister all registered ids
        For Each registeredId In _registeredIdCount
            unregisterHotkeys(sourceForm, registeredId)
        Next
    End Sub
#End Region
End Class