Imports System.Windows.Input

Namespace Model
    Public Class HotkeyConfig
        ' ScreenCapture  'store hotkey activating screenshot mode
        Public Property ScreenCapture_KeyValue As Key
        Public Property ScreenCapture_KeyModifier As KeyModifier
        Public Property ScreenCapture_HotkeyId As HotkeyId  'This ID is used to distinguish different hotkey in Winsdows message context

        Public Shared Function Load(configConn As Controller.IConfigIniConn) As HotkeyConfig
            Dim config As New HotkeyConfig()
            config.ScreenCapture_KeyValue = CType(Int(configConn.Load(section:="HotKey", key:="ScreenCapture_KeyValue", defaultValue:=Key.F4)), Key)
            config.ScreenCapture_KeyModifier = CType(Int(configConn.Load(section:="HotKey", key:="ScreenCapture_KeyModifier", defaultValue:=KeyModifier.None)), KeyModifier)
            config.ScreenCapture_HotkeyId = HotkeyId.ScreenCapture

            Return config
        End Function

        Public Shared Sub Save(config As HotkeyConfig, configConn As Controller.IConfigIniConn)
            configConn.Save(section:="HotKey", key:="ScreenCapture_KeyValue", value:=config.ScreenCapture_KeyValue)
            configConn.Save(section:="HotKey", key:="ScreenCapture_KeyModifier", value:=config.ScreenCapture_KeyModifier)
        End Sub
        Private Sub New()

        End Sub
    End Class

    'This ID is used to distinguish different hotkey in Winsdows message context
    Public Enum HotkeyId
        ScreenCapture
    End Enum

    Public Enum KeyModifier
        None = 0
        Alt = &H1
        Control = &H2
        Shift = &H4
        Winkey = &H8
    End Enum 'This enum is just to make it easier to call the registerHotKey function: The modifier integer codes are replaced by a friendly "Alt","Shift" etc.
End Namespace
