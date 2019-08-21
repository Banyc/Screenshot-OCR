Imports System.Windows.Input

Namespace Model
    Public Class HotkeyConfig
        ' ScreenCapture  'store hotkey activating screenshot mode
        Public Property ScreenCapture_KeyValue As Key
        Public Property ScreenCapture_KeyModifier As KeyModifier
        Public Property ScreenCapture_HotkeyId As HotkeyId  'This ID is used to distinguish different hotkey in Winsdows message context

        Public Shared Function Load(iniFile As Controller.IniFile) As HotkeyConfig
            Dim config As New HotkeyConfig()
            config.ScreenCapture_KeyValue = CType(Int(iniFile.ReadIni(Section:="HotKey", Key:="ScreenCapture_KeyValue", DefaultValue:=Key.F4)), Key)
            config.ScreenCapture_KeyModifier = CType(Int(iniFile.ReadIni(Section:="HotKey", Key:="ScreenCapture_KeyModifier", DefaultValue:=KeyModifier.None)), KeyModifier)
            config.ScreenCapture_HotkeyId = HotkeyId.ScreenCapture

            Return config
        End Function
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
