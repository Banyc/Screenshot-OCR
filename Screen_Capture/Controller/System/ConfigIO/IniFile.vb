'https://www.ctolib.com/topics-53086.html
Imports System.Runtime.InteropServices
Imports System.Text

Namespace Controller
    Public Class IniFile : Implements IConfigIniConn

        Private iniPath As String

        Public ReadOnly Property Path As String Implements IConfigIniConn.Path
            Get
                Return iniPath
            End Get
        End Property

        <DllImport("kernel32")>
        Private Shared Function WritePrivateProfileString(ByVal section As String, ByVal key As String, ByVal val As String, ByVal filePath As String) As Long
        End Function
        <DllImport("kernel32")>
        Private Shared Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
        End Function

        Public Sub New(ByVal Optional iniPath As String = "./default.ini")
            Me.iniPath = iniPath
        End Sub

        Public Function ExistINIFile() As Boolean
            Return System.IO.File.Exists(iniPath)
        End Function

        Public Function Load(key As String, defaultValue As String, Optional section As String = "default") As String Implements IConfigIniConn.Load
            Dim temp As StringBuilder = New StringBuilder(256)
            Dim i As Integer = GetPrivateProfileString(section, key, defaultValue, temp, 256, Me.Path)
            Return temp.ToString()
        End Function

        Public Sub Save(key As String, value As String, Optional section As String = "default") Implements IConfigIniConn.Save
            WritePrivateProfileString(section, key, value, Me.Path)
        End Sub
    End Class
End Namespace
