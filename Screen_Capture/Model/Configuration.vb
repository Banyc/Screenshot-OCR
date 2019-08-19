Namespace Model
    Public Class Configuration
        Private Const iniPath As String = "./config.ini"

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
            Dim config As Configuration = New Configuration()

            Dim iniFile As New Controller.IniFile(iniPath)

            config.Mode = CType(Int(iniFile.ReadIni(Section:="Default", Key:="Mode", DefaultValue:="0")), ProcessMode)
            config.A9T9_Lang = CType(Int(iniFile.ReadIni(Section:="A9T9", Key:="Language", DefaultValue:=Str(Language.eng))), Language)
            config.A9T9_Apikey = iniFile.ReadIni(Section:="A9T9", Key:="API_Key", DefaultValue:="helloworld")
            config.A9T9_TimeOut = iniFile.ReadIni(Section:="A9T9", Key:="TimeOut", DefaultValue:="5")
            config.Sogou_TimeOut = iniFile.ReadIni(Section:="Sogou", Key:="TimeOut", DefaultValue:="5")
            config.SauceNAO_Timeout = iniFile.ReadIni(Section:="SauceNAO", Key:="Timeout", DefaultValue:="5")

            config.Hotkeys = HotkeyConfig.Load(iniFile)

            config.EraseAllNewlines = CType(iniFile.ReadIni(Section:="Advance", Key:="EraseAllNewlines", DefaultValue:="1"), Boolean)

            Return config
        End Function

        Private Sub New()

        End Sub

        Public Sub Save()
            Dim iniFile As New Controller.IniFile(iniPath)
            iniFile.WriteIni(Section:="Default", Key:="Mode", Value:=Mode)
            iniFile.WriteIni(Section:="A9T9", Key:="Language", Value:=A9T9_Lang)
            iniFile.WriteIni(Section:="A9T9", Key:="API_Key", Value:=A9T9_Apikey)
            iniFile.WriteIni(Section:="A9T9", Key:="TimeOut", Value:=A9T9_TimeOut)
            iniFile.WriteIni(Section:="Sogou", Key:="TimeOut", Value:=Sogou_TimeOut)
            iniFile.WriteIni(Section:="SauceNAO", Key:="Timeout", Value:=SauceNAO_Timeout)

            iniFile.WriteIni(Section:="HotKey", Key:="ScreenCapture_KeyValue", Value:=Hotkeys.ScreenCapture_KeyValue)
            iniFile.WriteIni(Section:="HotKey", Key:="ScreenCapture_KeyModifier", Value:=Hotkeys.ScreenCapture_KeyModifier)

            iniFile.WriteIni(Section:="Advance", Key:="EraseAllNewlines", Value:=EraseAllNewlines)
        End Sub


    End Class
End Namespace
