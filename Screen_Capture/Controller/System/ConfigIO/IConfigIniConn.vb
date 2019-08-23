Namespace Controller

    Public Interface IConfigIniConn
        ReadOnly Property Path As String

        Function Load(key As String, defaultValue As String, Optional section As String = "default") As String
        Sub Save(key As String, value As String, Optional section As String = "default")

    End Interface
End Namespace
