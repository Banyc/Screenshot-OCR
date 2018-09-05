'https://www.ctolib.com/topics-53086.html
Imports System.Runtime.InteropServices
Imports System.Text

Class IniFile
    Public iniPath As String
    <DllImport("kernel32")>
    Private Shared Function WritePrivateProfileString(ByVal section As String, ByVal key As String, ByVal val As String, ByVal filePath As String) As Long
    End Function
    <DllImport("kernel32")>
    Private Shared Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    End Function

    Public Sub New(ByVal Optional iniPath As String = "./default.ini")
        Me.iniPath = iniPath
    End Sub

    Public Sub WriteIni(ByVal Section As String, ByVal Key As String, ByVal Value As String)
        WritePrivateProfileString(Section, Key, Value, Me.iniPath)
    End Sub

    Public Sub WriteIni(ByVal Key As String, ByVal Value As String)
        WritePrivateProfileString("default", Key, Value, Me.iniPath)
    End Sub

    Public Function ReadIni(ByVal Section As String, ByVal Key As String, Optional DefaultValue As String = "") As String
        Dim temp As StringBuilder = New StringBuilder(256)
        Dim i As Integer = GetPrivateProfileString(Section, Key, DefaultValue, temp, 256, Me.iniPath)
        Return temp.ToString()
    End Function

    Public Function ReadIni(ByVal Key As String, Optional DefaultValue As String = "") As String
        Return ReadIni("default", Key, DefaultValue)
    End Function

    Public Function ExistINIFile() As Boolean
        Return System.IO.File.Exists(iniPath)
    End Function
End Class
