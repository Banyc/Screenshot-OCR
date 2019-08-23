Namespace Model
    Public Class Configuration
        Private Const IniPath As String = "./config.ini"

        Public Enum Language  ' detective language
            ara = 0  'Arabic
            bul  'Bulgarian
            chs  'Chinese(Simplified)
            cht  'Chinese(Traditional)
            hrv  'Croatian
            cze  'Czech
            dan  'Danish
            dut  'Dutch
            eng  'English
            fin  'Finnish
            fre  'French
            ger  'German
            gre  'Greek
            hun  'Hungarian
            kor  'Korean
            ita  'Italian
            jpn  'Japanese
            nor  'Norwegian
            pol  'Polish
            por  'Portuguese
            rus  'Russian
            slv  'Slovenian
            spa  'Spanish
            swe  'Swedish
            tur  'Turkish
        End Enum

        Public Enum ProcessMode
            A9T9 = 0
            Sogou
            SauceNAO
        End Enum

        Public Property Mode As ProcessMode
        Public Property A9T9_Apikey As String
        Public Property A9T9_Lang As Language
        Public Property A9T9_TimeOut As Integer  'of Seconds

        Public Property Sogou_TimeOut As Integer  'of Seconds

        Public Property SauceNAO_Timeout As Integer  'of Seconds

        ' Advance
        Public Property EraseAllNewlines As Boolean

        Public Hotkeys As Model.HotkeyConfig


        'initiate config through ".ini" file
        Public Shared Function Load() As Configuration
            Dim configConn As New Controller.IniFile(IniPath)
            Return Load(configConn)
        End Function
        'initiate config through ".ini" file
        Public Shared Function Load(configConn As Controller.IConfigIniConn) As Configuration
            Dim config As Configuration = New Configuration()

            config.Mode = CType(Int(configConn.Load(section:="Default", key:="Mode", defaultValue:="0")), ProcessMode)
            config.A9T9_Lang = CType(Int(configConn.Load(section:="A9T9", key:="Language", defaultValue:=Str(Language.eng))), Language)
            config.A9T9_Apikey = configConn.Load(section:="A9T9", key:="API_Key", defaultValue:="helloworld")
            config.A9T9_TimeOut = configConn.Load(section:="A9T9", key:="TimeOut", defaultValue:="5")
            config.Sogou_TimeOut = configConn.Load(section:="Sogou", key:="TimeOut", defaultValue:="5")
            config.SauceNAO_Timeout = configConn.Load(section:="SauceNAO", key:="Timeout", defaultValue:="5")

            config.Hotkeys = HotkeyConfig.Load(configConn)

            config.EraseAllNewlines = CType(configConn.Load(section:="Advance", key:="EraseAllNewlines", defaultValue:="1"), Boolean)

            Return config
        End Function

        Private Sub New()

        End Sub


        Public Sub Save()
            Dim configConn As New Controller.IniFile(IniPath)
            Me.Save(configConn)
        End Sub
        Public Sub Save(configConn As Controller.IConfigIniConn)
            configConn.Save(section:="Default", key:="Mode", value:=Mode)
            configConn.Save(section:="A9T9", key:="Language", value:=A9T9_Lang)
            configConn.Save(section:="A9T9", key:="API_Key", value:=A9T9_Apikey)
            configConn.Save(section:="A9T9", key:="TimeOut", value:=A9T9_TimeOut)
            configConn.Save(section:="Sogou", key:="TimeOut", value:=Sogou_TimeOut)
            configConn.Save(section:="SauceNAO", key:="Timeout", value:=SauceNAO_Timeout)

            HotkeyConfig.Save(Hotkeys, configConn)

            configConn.Save(section:="Advance", key:="EraseAllNewlines", value:=EraseAllNewlines)
        End Sub


    End Class
End Namespace
